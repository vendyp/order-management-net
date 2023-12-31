﻿using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Moq;
using OrderManagement.Core.Abstractions;
using OrderManagement.Core.Entities;
using OrderManagement.WebApi;
using OrderManagement.WebApi.Endpoints.Identity;
using Shouldly;

namespace OrderManagement.UnitTests.Endpoints.Identity;

public class SignInTests
{
    

    

    [Fact]
    public async Task SignIn_Given_CorrectRequest_With_InvalidEmail_ShouldReturn_BadRequest()
    {
        var accessTokenBuilderMock = new Mock<IAccessTokenBuilder>();
        var userServiceMock = new Mock<IUserService>();
        userServiceMock.Setup(e =>
                e.GetUserByUsernameAsync(
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
            .ReturnsAsync((User?)null);

        var signIn = new SignIn(
            userServiceMock.Object,
            new Mock<IDbContext>().Object,
            accessTokenBuilderMock.Object);

        var request = new SignInRequest
        {
            Username = "test@test.com",
            Password = "Qwerty@1234"
        };

        var result = await signIn.HandleAsync(request, CancellationToken.None);

        // Assert the expected results
        result.ShouldNotBeNull();
        result.Result.ShouldNotBeNull();
        result.Result.ShouldBeOfType(typeof(BadRequestObjectResult));
        var actual = result.Result as BadRequestObjectResult;
        actual.ShouldNotBeNull();
        actual.StatusCode.ShouldBe(400);
        actual.Value.ShouldBeOfType<Error>();
        var err = actual.Value as Error;
        err.ShouldNotBeNull();
        err.Message.ShouldBe("Invalid username or password");
    }

    [Fact]
    public async Task SignIn_Given_CorrectRequest_With_InvalidPassword_ShouldReturn_BadRequest()
    {
        var request = new SignInRequest
        {
            Username = "test@test.com",
            Password = "Qwerty@1234"
        };

        var userServiceMock = new Mock<IUserService>();
        var dummyUser = new User
        {
            Username = "test@test.com",
            HashedPassword = "bbb",
            Salt = "aaa"
        };

        //get username return data
        userServiceMock.Setup(e =>
                e.GetUserByUsernameAsync(
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
            .ReturnsAsync(new User());

        //when verify password called return false
        userServiceMock.Setup(e => e.VerifyPassword(request.Password, dummyUser.Salt, dummyUser.HashedPassword))
            .Returns(false);

        var signIn = new SignIn(
            userServiceMock.Object,
            new Mock<IDbContext>().Object,
            new Mock<IAccessTokenBuilder>().Object);

        var result = await signIn.HandleAsync(request, CancellationToken.None);

        // Assert the expected results
        result.ShouldNotBeNull();
        result.Result.ShouldNotBeNull();
        result.Result.ShouldBeOfType(typeof(BadRequestObjectResult));
        var actual = result.Result as BadRequestObjectResult;
        actual.ShouldNotBeNull();
        actual.StatusCode.ShouldBe(400);
        actual.Value.ShouldBeOfType<Error>();
        var err = actual.Value as Error;
        err.ShouldNotBeNull();
        err.Message.ShouldBe("Invalid username or password");
    }

    [Fact]
    public async Task SignIn_Given_CorrectRequest_Should_Do_As_Expected()
    {
        var request = new SignInRequest
        {
            Username = "test@test.com",
            Password = "Qwerty@1234"
        };

        var userServiceMock = new Mock<IUserService>();

        var dummyUser = new User
        {
            Username = request.Username,
            HashedPassword = "aaa"
        };

        User? user = null;

        var dbContextMock = new Mock<IDbContext>();
        dbContextMock.Setup(e => e.AttachEntity(It.IsAny<User>()))
            .Callback<User>((entity) =>
            {
                entity.UserId.ShouldBe(dummyUser.UserId);
                entity.Username.ShouldBe(dummyUser.Username);
                entity.HashedPassword.ShouldBe(dummyUser.HashedPassword);

                user = entity;
            });

        //get username return data
        userServiceMock.Setup(e =>
                e.GetUserByUsernameAsync(
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
            .ReturnsAsync(dummyUser);

        //when verify password called return false
        userServiceMock.Setup(e => e.VerifyPassword(request.Password, It.IsAny<string>(), It.IsAny<string>()))
            .Returns(true);

        var accessTokenBuilderMock = new Mock<IAccessTokenBuilder>();
        accessTokenBuilderMock.Setup(e => e.CreateToken(It.IsAny<List<Claim>>()))
            .Returns(new JsonWebToken());

        var signIn = new SignIn(
            userServiceMock.Object,
            dbContextMock.Object,
            accessTokenBuilderMock.Object);

        user.ShouldBeNull();

        _ = await signIn.HandleAsync(request, CancellationToken.None);

        user.ShouldNotBeNull();
        user.UserTokens.Count.ShouldBeGreaterThan(0);
        var userToken = user.UserTokens.First();
        userToken.RefreshToken.ShouldNotBeEmpty();
        userToken.IsUsed.ShouldBeFalse();
        userToken.UsedAt.ShouldBeNull();

        dbContextMock.Verify(e => e.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }
}