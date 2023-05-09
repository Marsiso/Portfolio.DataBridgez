using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Portfolio.Databridgez.Application.Mappings;
using Portfolio.DataBridgez.Domain.Dtos.Get;
using Portfolio.Databridgez.Domain.Dtos.Post;
using Portfolio.DataBridgez.IdentityProvider.Controllers.Base;
using Portfolio.DataBridgez.IdentityProvider.Filters;
using Portfolio.Databridgez.Infrastructure.Identity;

namespace Portfolio.DataBridgez.IdentityProvider.Controllers.Authentication;

/// <summary>
///     Endpoints related to the user sign in tasks.
/// </summary>
public sealed class RegisterController : ApiControllerBase
{
    /// <summary>
    ///     The controller constructor.
    /// </summary>
    /// <param name="logger">The abstraction for logging.</param>
    public RegisterController(ILogger logger) : base(logger)
    {
    }

    /// <summary>
    ///     The user registration endpoint that handles the user registration and related tasks.
    /// </summary>
    /// <param name="registerInput">The user for creation data transfer object.</param>
    /// <param name="passwordHasher">The abstraction for hashing and verifying passwords.</param>
    /// <param name="cancellationToken">The cancellation token to prematurely cancel the user registration related tasks.</param>
    /// <returns>
    ///     The registered user identifier with the message and status code on succeed 
    ///     otherwise model validation errors, status code and error description as message.
    /// </returns>
    [HttpPost("~/api/user/sign-in", Name = nameof(Signin))]
    [ServiceFilter(typeof(RegisterInputValidationFilter), Order = 1)]
    [ServiceFilter(typeof(RegisterInputCredentialsTakenFilter), Order = 2)]
    [Produces(MediaTypeNames.Application.Json, MediaTypeNames.Application.Xml)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<RegisterResponse>> Signin(
        [FromBody] RegisterInput registerInput,
        [FromServices] IPasswordHasher<AppUser> passwordHasher,
        CancellationToken cancellationToken)
    {
        // Request the identity manager from the DI container
        using var identityStore = HttpContext.RequestServices.GetService<IdentityStore>();
        ArgumentNullException.ThrowIfNull(identityStore);

        // Map the input data to the user model for creation 
        var userToCreate = new AppUser();
        registerInput.MapRegisterUserInputToExistingUser(userToCreate);

        // Generate the security stamp, password hash from the password and retrieve normalized user name and email
        // than update the user model for creation
        userToCreate.NormalizedUserName = (string)HttpContext.Items[nameof(AppUser.NormalizedUserName)]!;
        userToCreate.NormalizedEmail = (string)HttpContext.Items[nameof(AppUser.NormalizedEmail)]!;
        passwordHasher.HashPassword(userToCreate, registerInput.Password!);
        userToCreate.SecurityStamp = Guid.NewGuid().ToString("D");

        // Create the user in the persistence store otherwise return the internal server error as response
        var identityResult = await identityStore.CreateAsync(userToCreate, cancellationToken);
        if (identityResult.Succeeded) return StatusCode(StatusCodes.Status201Created, new RegisterResponse
        {
            StatusCode = StatusCodes.Status201Created,
            Message = "User has been successfully registered",
        });

        return StatusCode(StatusCodes.Status500InternalServerError, new RegisterResponse
        {
            StatusCode = StatusCodes.Status500InternalServerError,
            Message = "Registration failed due to the internal server error",
        });
    }
}