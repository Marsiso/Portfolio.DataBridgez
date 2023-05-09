using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Portfolio.Databridgez.Application.Mappings;
using Portfolio.DataBridgez.Domain.Dtos.Get;
using Portfolio.Databridgez.Domain.Dtos.Post;
using Portfolio.Databridgez.Domain.Validators;
using Portfolio.DataBridgez.IdentityProvider.Controllers.Base;
using Portfolio.Databridgez.Infrastructure.Identity;
using static System.String;

namespace Portfolio.DataBridgez.IdentityProvider.Controllers.Authentication;

[AllowAnonymous]
public sealed class LoginController : ApiControllerBase
{
    public LoginController(ILogger logger) : base(logger)
    {
    }

    [HttpPost("~/api/user/log-in")]
    [Produces(MediaTypeNames.Application.Json, MediaTypeNames.Application.Xml)]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status415UnsupportedMediaType)]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<LoginResponse>> Login(
        [FromBody] LoginInput? loginInput,
        [FromServices] IAccessTokenProvider accessTokenProvider,
        [FromServices] ILookupNormalizer lookupNormalizer,
        [FromServices] IPasswordHasher<AppUser> passwordHasher,
        CancellationToken cancellationToken)
    {
        // Null object
        if (loginInput == null)
        {
            return StatusCode(StatusCodes.Status400BadRequest, new LoginResponse
            {
                StatusCode = StatusCodes.Status400BadRequest,
                Message = Format("Object sent from the client is null. Controller: {0}, action: {1}",
                    nameof(LoginController), nameof(Login))
            });
        }

        // Validate model
        var validator = new LoginInputValidator();
        var validationResult = await validator.ValidateAsync(loginInput, cancellationToken);
        if (!validationResult.IsValid)
        {
            return StatusCode(StatusCodes.Status422UnprocessableEntity, new LoginResponse
            {
                StatusCode = StatusCodes.Status422UnprocessableEntity,
                Message = Format("Object model sent from the client is invalid. Controller: {0}, action: {1}",
                    nameof(LoginController), nameof(Login)),
                ValidationFailures = validationResult.Errors.MapSourceToDestination(new List<ValidationFailureResponse>())
            });
        }

        // Get user
        using var identityManager = HttpContext.RequestServices.GetService<IdentityStore>();
        ArgumentNullException.ThrowIfNull(identityManager);

        var normalizedUserName = lookupNormalizer.NormalizeEmail(loginInput.UserName);
        var userEntity = await identityManager.Users.SingleOrDefaultAsync(u => 
            u.NormalizedEmail == normalizedUserName
            || u.NormalizedUserName == normalizedUserName, cancellationToken: cancellationToken);

        if (userEntity == null)
        {
            return StatusCode(StatusCodes.Status404NotFound, new LoginResponse
            {
                StatusCode = StatusCodes.Status404NotFound,
                Message = Format("Invalid user name credential. Controller: {0}, action: {1}",
                    nameof(LoginController), nameof(Login))
            });
        }
        
        // Compare password hashes
        var passwordVerificationResult = passwordHasher.VerifyHashedPassword(
            userEntity, userEntity.PasswordHash, loginInput.Password);

        if (passwordVerificationResult == PasswordVerificationResult.Failed)
        {
            return StatusCode(StatusCodes.Status400BadRequest, new LoginResponse
            {
                StatusCode = StatusCodes.Status400BadRequest,
                Message = Format("Invalid password credential. Controller: {0}, action: {1}",
                    nameof(LoginController), nameof(Login))
            });
        }

        var accessToken = await accessTokenProvider.GenerateTokenAsync(userEntity);
        return StatusCode(StatusCodes.Status201Created, new LoginResponse
        {
            StatusCode = StatusCodes.Status201Created,
            Message = Format("Log in success. Controller: {0}, action: {1}",
                nameof(LoginController), nameof(Login)),
            Token = accessToken
        });
    }
}