using FluentValidation;
using Portfolio.DataBridgez.Domain.Constants;
using Portfolio.Databridgez.Domain.Dtos.Post;
using static Portfolio.DataBridgez.Domain.Constants.AppDbDefaults.Tables.AppUsers.Column;
using static Portfolio.DataBridgez.Domain.Constants.IdentityDefaults;
using static Portfolio.DataBridgez.Domain.Constants.IdentityDefaults.Password;

namespace Portfolio.Databridgez.Domain.Validators;

/// <summary>
///     Collection of validation rules for the <see cref="RegisterInput"/> data transfer object.
/// </summary>
public sealed class RegisterInputValidator : AbstractValidator<RegisterInput>
{
    public RegisterInputValidator()
    {
        ClassLevelCascadeMode = CascadeMode.Continue;
        
        RuleFor(u => u.Email).NotEmpty()
            .WithMessage("Email is required")
            .EmailAddress()
            .WithMessage("Invalid email format")
            .MaximumLength(MaximumEmailLengthConstraint)
            .WithMessage("Email too long");

        RuleFor(u => u.Password).NotEmpty()
            .WithMessage("Password is required")
            .MinimumLength(RequiredLength)
            .WithMessage("Password too short")
            .MaximumLength(FullLength)
            .WithMessage("Password too long");

        RuleFor(u => u.ConfirmPassword).NotEmpty()
            .WithMessage("Password confirmation is required")
            .Equal(u => u.Password)
            .WithMessage("Password does not match");
        
        RuleFor(u => u.UserName).NotEmpty()
            .WithMessage("User name is required")
            .MinimumLength(MinimumUserNameLengthConstraint)
            .WithMessage("User name too short")
            .MaximumLength(MaximumUserNameLengthConstraint)
            .WithMessage("User name too long")
            .Must(ValidUserNameCharacter)
            .WithMessage("User name contains invalid character");
    }

    private static bool ValidUserNameCharacter(string? s)
    {
        return s != null && s.All(c => User.AllowedUserNameCharacters.IndexOf(c, StringComparison.Ordinal) != -1);
    }
}