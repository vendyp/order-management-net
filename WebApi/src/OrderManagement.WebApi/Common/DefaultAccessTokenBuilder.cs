using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using OrderManagement.Core.Abstractions;

namespace OrderManagement.WebApi.Common;

public class DefaultAccessTokenBuilder : IAccessTokenBuilder
{
    private readonly IClock _clock;
    private readonly IConfiguration _configuration;

    public DefaultAccessTokenBuilder(IClock clock, IConfiguration configuration)
    {
        _clock = clock;
        _configuration = configuration;
    }

    public JsonWebToken CreateToken(List<Claim> claims)
    {
        if (!claims.Any())
            throw new InvalidOperationException("Claims can not be empty");

        var expires = _clock.CurrentDate().Add(TimeSpan.FromDays(7));

        var jwt = new JwtSecurityToken(
            "_app",
            claims: claims,
            notBefore: _clock.CurrentDate(),
            expires: expires,
            signingCredentials: new SigningCredentials(
                new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(_configuration.GetValue<string>("Authorization:SigningKey")!)),
                SecurityAlgorithms.HmacSha256)
        );

        var token = new JwtSecurityTokenHandler().WriteToken(jwt);

        var jsonWebToken = new JsonWebToken
        {
            AccessToken = token,
            Expiry = new DateTimeOffset(expires).ToUnixTimeMilliseconds(),
        };

        return jsonWebToken;
    }
}