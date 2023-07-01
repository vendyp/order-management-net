using Microsoft.AspNetCore.Mvc;
using OrderManagement.WebApi.Endpoints.Identity;
using Shouldly;

namespace OrderManagement.UnitTests.Endpoints.Identity;

public class SignInTests
{
    public static IEnumerable<object[]> GetInvalidRequests()
    {
        yield return new object[]
        {
            new SignInRequest
            {
                Username = null,
                Password = null,
            }
        };

        yield return new object[]
        {
            new SignInRequest
            {
                Username = string.Empty,
                Password = string.Empty,
            }
        };

        yield return new object[]
        {
            new SignInRequest
            {
                Username = "abcde",
                Password = string.Empty,
            }
        };

        yield return new object[]
        {
            new SignInRequest
            {
                Username = "私の名前はウェンディです",
                Password = "Qwerty@1234",
            }
        };

        yield return new object[]
        {
            new SignInRequest
            {
                Username = new string('a', 300),
                Password = "Qwerty@1234",
            }
        };

        yield return new object[]
        {
            new SignInRequest
            {
                Username = "dwivendypratama",
                Password = "Qwerty@1234",
            }
        };

        yield return new object[]
        {
            new SignInRequest
            {
                Username = "اسمي ويندي",
                Password = "Qwerty@1234",
            }
        };

        yield return new object[]
        {
            new SignInRequest
            {
                Username = "我的名字是温迪",
                Password = "Qwerty@1234",
            }
        };
    }

    [Theory]
    [MemberData(nameof(GetInvalidRequests))]
    public async Task SignIn_Given_InvalidRequest_ShouldReturn_BadRequest(SignInRequest request)
    {
        var signIn = new SignIn();

        var result = await signIn.HandleAsync(request, CancellationToken.None);

        // Assert the expected results
        result.ShouldNotBeNull();
        result.Result.ShouldNotBeNull();
        result.Result.ShouldBeOfType(typeof(BadRequestObjectResult));
        var actual = result.Result as BadRequestObjectResult;
        actual.ShouldNotBeNull();
        actual.Value.ShouldBeOfType<Error>();
    }
}