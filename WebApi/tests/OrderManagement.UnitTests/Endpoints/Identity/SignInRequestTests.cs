using OrderManagement.WebApi.Endpoints.Identity;
using Shouldly;

namespace OrderManagement.UnitTests.Endpoints.Identity;

public class SignInRequestTests
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
        var validator = new SignInRequestValidator();
        var validationResult = await validator.ValidateAsync(request, CancellationToken.None);
        validationResult.IsValid.ShouldBeFalse();
    }
}