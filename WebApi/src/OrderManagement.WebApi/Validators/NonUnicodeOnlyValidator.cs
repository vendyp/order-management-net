using FluentValidation;

namespace OrderManagement.WebApi.Validators;

public class NonUnicodeOnlyValidator : AbstractValidator<string>
{
    public NonUnicodeOnlyValidator()
    {
        RuleFor(x => x)
            .Must(BeNonUnicodeOnly)
            .WithMessage("The string must contain only non-unicode characters.");
    }

    private static bool BeNonUnicodeOnly(string value)
    {
        foreach (char c in value)
        {
            if (c < 0 || c > 127)
                return false;
        }

        return true;
    }
}