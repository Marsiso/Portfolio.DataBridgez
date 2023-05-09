using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Portfolio.Databridgez.Application.Mappings;
using Portfolio.Databridgez.Domain.Dtos.Post;
using Portfolio.DataBridgez.IdentityProvider.Controllers.Base;
using Portfolio.DataBridgez.IdentityProvider.Filters;
using Portfolio.Databridgez.Infrastructure.Identity;

namespace Portfolio.DataBridgez.IdentityProvider.Controllers.Authentication;

public sealed class RegisterController : ApiControllerBase
{
    public RegisterController(ILogger logger) : base(logger)
    {
    }

    [HttpPost("~/api/user/sign-in", Name = nameof(Signin))]
    [ServiceFilter(typeof(RegisterInputValidationFilter), Order = 1)]
    [ServiceFilter(typeof(RegisterInputCredentialsTakenFilter), Order = 2)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Signin(
        [FromBody] RegisterInput registerInput,
        [FromServices] IPasswordHasher<AppUser> passwordHasher,
        CancellationToken cancellationToken)
    {
        // Request user manager
        using var identityStore = HttpContext.RequestServices.GetService<IdentityStore>();
        ArgumentNullException.ThrowIfNull(identityStore);

        // Map input data to entity
        var userToCreate = new AppUser();
        registerInput.MapRegisterUserInputToExistingUser(userToCreate);

        // Security
        userToCreate.NormalizedUserName = (string)HttpContext.Items[nameof(AppUser.NormalizedUserName)]!;
        userToCreate.NormalizedEmail = (string)HttpContext.Items[nameof(AppUser.NormalizedEmail)]!;
        passwordHasher.HashPassword(userToCreate, registerInput.Password!);
        userToCreate.SecurityStamp = Guid.NewGuid().ToString("D");

        // Create user
        var identityResult = await identityStore.CreateAsync(userToCreate, cancellationToken);
        if (identityResult.Succeeded) return StatusCode(StatusCodes.Status201Created);

        return StatusCode(StatusCodes.Status500InternalServerError,
            "User could not be created in persistence store");
    }
}