using Microsoft.AspNetCore.Mvc;
using OrderManagement.WebApi.Dto;
using Swashbuckle.AspNetCore.Annotations;

namespace OrderManagement.WebApi.Endpoints.Identity;

public class SignIn : BaseEndpoint<SignInRequest, SignInDto>
{
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

        throw new NotImplementedException();
    }
}