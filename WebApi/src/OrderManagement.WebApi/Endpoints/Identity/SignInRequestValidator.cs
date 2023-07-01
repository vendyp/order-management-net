using FluentValidation;
using OrderManagement.WebApi.Validators;

namespace OrderManagement.WebApi.Endpoints.Identity;

public class SignInRequestValidator : AbstractValidator<SignInRequest>
{
    public SignInRequestValidator()
    {
        RuleFor(e => e.Username).NotNull()
            .NotEmpty()
            .MaximumLength(256)
            .SetValidator(new NonUnicodeOnlyValidator())
            .EmailAddress();

        RuleFor(e => e.Password).NotNull()
            .NotEmpty();
    }
}