using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Portfolio.DataBridgez.Domain.Dtos.Get;
using Portfolio.Databridgez.Domain.Dtos.Post;
using Portfolio.Databridgez.Infrastructure.Identity;
using static System.String;

namespace Portfolio.DataBridgez.IdentityProvider.Filters;

public sealed class RegisterInputCredentialsTakenFilter : IAsyncActionFilter
{
    private readonly ILookupNormalizer _lookupNormalizer;
    
    public RegisterInputCredentialsTakenFilter(ILookupNormalizer lookupNormalizer)
    {
        _lookupNormalizer = lookupNormalizer;
    }

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        // Get necessary route data
        var cancellationToken = (CancellationToken)context.ActionArguments
            .Single(keyValuePair => keyValuePair.Value?.GetType() == typeof(CancellationToken))
            .Value!;
        
        ArgumentNullException.ThrowIfNull(cancellationToken);
        cancellationToken.ThrowIfCancellationRequested();
        
        var registerUserInput = (RegisterInput)context.ActionArguments
            .Single(keyValuePair => keyValuePair.Value?.GetType() == typeof(RegisterInput))
            .Value!;
        
        ArgumentNullException.ThrowIfNull(registerUserInput);

        // Normalize user name and email
        var normalizeUserName = _lookupNormalizer.NormalizeName(registerUserInput.UserName)!;
        var normalizedEmail = _lookupNormalizer.NormalizeEmail(registerUserInput.Email)!;
        
        using var identityStore = context.HttpContext.RequestServices.GetService<IdentityStore>();
        ArgumentNullException.ThrowIfNull(identityStore);
        
        // If user name or email already exist than return
        var userNameExist = await  identityStore.Users.AnyAsync(u =>
            u.NormalizedUserName == normalizeUserName, cancellationToken: cancellationToken);

        var emailExist = await identityStore.Users.AnyAsync(u =>
            u.NormalizedEmail == normalizedEmail, cancellationToken: cancellationToken);

        if (userNameExist || emailExist)
        {
            context.HttpContext.Response.ContentType = MediaTypeNames.Application.Json;
            context.Result = new UnprocessableEntityObjectResult(
                CreateCredentialsTakenResponse(registerUserInput, userNameExist, emailExist));
            return;
        }

        // Add normalized user name and email to action arguments
        context.HttpContext.Items.Add(nameof(AppUser.NormalizedUserName), normalizeUserName);
        context.HttpContext.Items.Add(nameof(AppUser.NormalizedEmail), normalizedEmail);
        await next();
    }

    private static RegisterResponse CreateCredentialsTakenResponse(RegisterInput registerUserInput, bool userNameExist,
        bool emailExist)
    {
        return (userNameExist, emailExist) switch
        {
            (true, true) => new RegisterResponse
            {
                StatusCode = StatusCodes.Status400BadRequest,
                Message = Format("User with user name {0} and email {1} already exists", registerUserInput.UserName,
                    registerUserInput.Email),
            },
            (true, false) => new RegisterResponse
            {
                StatusCode = StatusCodes.Status400BadRequest,
                Message = Format("User with user name {0} already exists", registerUserInput.UserName),
            },
            (false, true) => new RegisterResponse
            {
                StatusCode = StatusCodes.Status400BadRequest,
                Message = Format("User with email {0} already exists", registerUserInput.Email),
            },
            _ => throw new ArgumentOutOfRangeException()
        };
    }
}