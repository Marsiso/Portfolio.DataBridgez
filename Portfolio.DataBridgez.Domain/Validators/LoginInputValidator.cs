using FluentValidation;
using Portfolio.Databridgez.Domain.Dtos.Post;

namespace Portfolio.Databridgez.Domain.Validators;

public sealed class LoginInputValidator : AbstractValidator<LoginInput>
{
    public LoginInputValidator()
    {
    }
}