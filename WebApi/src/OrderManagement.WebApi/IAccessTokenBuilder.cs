using System.Security.Claims;

namespace OrderManagement.WebApi;

public interface IAccessTokenBuilder
{
    JsonWebToken CreateToken(List<Claim> claims);
}