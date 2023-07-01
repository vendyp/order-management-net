using Microsoft.AspNetCore.Mvc;
using OrderManagement.Core.Abstractions;
using OrderManagement.Core.Entities;
using OrderManagement.WebApi.Dto;
using Swashbuckle.AspNetCore.Annotations;

namespace OrderManagement.WebApi.Endpoints.Identity;

public class SignIn : BaseEndpoint<SignInRequest, SignInDto>
{
    private readonly IUserService _userService;
    private readonly IDbContext _dbContext;

    public SignIn(IUserService userService, IDbContext dbContext)
    {
        _userService = userService;
        _dbContext = dbContext;
    }

    [HttpPost("sign-in")]
    [SwaggerOperation(
            Summary = "Sign in",
            Description = "",
            OperationId = "Identity.SignIn",
            Tags = new[] { "Identity" }),
    ]
    [ProducesResponseType(typeof(SignInDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
    public override async Task<ActionResult<SignInDto>> HandleAsync(SignInRequest request,
        CancellationToken cancellationToken = new())
    {
        var validator = new SignInRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
            return BadRequest(Error.Create("Invalid parameter", validationResult.Construct()));

        var user = await _userService.GetUserByUsernameAsync(request.Username!, cancellationToken);
        if (user is null)
            return BadRequest(Error.Create("Invalid username or password"));

        if (!_userService.VerifyPassword(request.Password!, user.Salt!, user.HashedPassword!))
            return BadRequest(Error.Create("Invalid username or password"));

        _dbContext.AttachEntity(user);

        var userToken = new UserToken
        {
            RefreshToken = Guid.NewGuid().ToString("N")
        };
        user.UserTokens.Add(userToken);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return new SignInDto();
    }
}