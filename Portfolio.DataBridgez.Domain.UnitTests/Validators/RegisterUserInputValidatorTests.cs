using Portfolio.Databridgez.Domain.Dtos.Post;
using Portfolio.Databridgez.Domain.Validators;

namespace Portfolio.DataBridgez.Domain.UnitTests.Validators;

/// <summary>
///     <see cref="RegisterInputValidator" /> test library.
/// </summary>
public sealed class RegisterUserInputValidatorTests
{
    [Fact]
    public void Validate_ValidInput_ReturnTrue()
    {
        // Arrange
        var validator = Activator.CreateInstance<RegisterInputValidator>();
        var userToRegister = Activator.CreateInstance<RegisterInput>();
        userToRegister.UserName = "TestUser";
        userToRegister.Email = "test.user@prov.dev";
        userToRegister.Password = userToRegister.ConfirmPassword = "Password123$";

        // Act
        var validationResult = validator.Validate(userToRegister);

        // Assert
        validationResult.Should().NotBeNull();
        validationResult.IsValid.Should().BeTrue();
    }

    [Fact]
    public void Validate_TooShortPassword_ReturnFalse()
    {
        // Arrange
        var validator = Activator.CreateInstance<RegisterInputValidator>();
        var userToRegister = Activator.CreateInstance<RegisterInput>();
        userToRegister.UserName = "TestUser";
        userToRegister.Email = "test.user@prov.dev";
        userToRegister.Password = userToRegister.ConfirmPassword = "Pass1$";

        // Act
        var validationResult = validator.Validate(userToRegister);

        // Assert
        validationResult.Should().NotBeNull();
        validationResult.IsValid.Should().BeFalse();
        validationResult.Errors.Should().Contain(failure =>
            failure.ErrorMessage.Equals("Password too short", StringComparison.InvariantCultureIgnoreCase));
    }

    [Fact]
    public void Validate_TooLongPassword_ReturnFalse()
    {
        // Arrange
        var validator = Activator.CreateInstance<RegisterInputValidator>();
        var userToRegister = Activator.CreateInstance<RegisterInput>();
        userToRegister.UserName = "TestUser";
        userToRegister.Email = "test.user@prov.dev";
        userToRegister.Password = userToRegister.ConfirmPassword =
            string.Join(string.Empty, Enumerable.Repeat("Pass123$", 100));

        // Act
        var validationResult = validator.Validate(userToRegister);

        // Assert
        validationResult.Should().NotBeNull();
        validationResult.IsValid.Should().BeFalse();
        validationResult.Errors.Should().Contain(failure =>
            failure.ErrorMessage.Equals("Password too long", StringComparison.InvariantCultureIgnoreCase));
    }
    
    [Fact]
    public void Validate_ConfirmationPassword_DoesNotMatch_ReturnFalse()
    {
        // Arrange
        var validator = Activator.CreateInstance<RegisterInputValidator>();
        var userToRegister = Activator.CreateInstance<RegisterInput>();
        userToRegister.UserName = "TestUser";
        userToRegister.Email = "test.user@prov.dev";
        userToRegister.Password = "FirstPassword123$";
        userToRegister.ConfirmPassword = "SecondPassword123$";

        // Act
        var validationResult = validator.Validate(userToRegister);

        // Assert
        validationResult.Should().NotBeNull();
        validationResult.IsValid.Should().BeFalse();
        validationResult.Errors.Should().Contain(failure =>
            failure.ErrorMessage.Equals("Password does not match", StringComparison.InvariantCultureIgnoreCase));
    }
    
    [Fact]
    public void Validate_InvalidFormatEmail_ReturnFalse()
    {
        // Arrange
        var validator = Activator.CreateInstance<RegisterInputValidator>();
        var userToRegister = Activator.CreateInstance<RegisterInput>();
        userToRegister.UserName = "TestUser";
        userToRegister.Email = "test.user.prov.dev";
        userToRegister.Password = userToRegister.ConfirmPassword = "Password123$";

        // Act
        var validationResult = validator.Validate(userToRegister);

        // Assert
        validationResult.Should().NotBeNull();
        validationResult.IsValid.Should().BeFalse();
        validationResult.Errors.Should().Contain(failure =>
            failure.ErrorMessage.Equals("Invalid email format", StringComparison.InvariantCultureIgnoreCase));
    }
    
    [Fact]
    public void Validate_TooLongEmail_ReturnFalse()
    {
        // Arrange
        var validator = Activator.CreateInstance<RegisterInputValidator>();
        var userToRegister = Activator.CreateInstance<RegisterInput>();
        userToRegister.UserName = "TestUser";
        userToRegister.Email = string.Format("{0}@prov.dev",
            string.Join(string.Empty, Enumerable.Repeat("test.user", 100)));
        userToRegister.Password = userToRegister.ConfirmPassword = "Password123$";

        // Act
        var validationResult = validator.Validate(userToRegister);

        // Assert
        validationResult.Should().NotBeNull();
        validationResult.IsValid.Should().BeFalse();
        validationResult.Errors.Should().Contain(failure =>
            failure.ErrorMessage.Equals("Email too long", StringComparison.InvariantCultureIgnoreCase));
    }
    
    [Fact]
    public void Validate_TooShortUserName_ReturnFalse()
    {
        // Arrange
        var validator = Activator.CreateInstance<RegisterInputValidator>();
        var userToRegister = Activator.CreateInstance<RegisterInput>();
        userToRegister.UserName = "u";
        userToRegister.Email = "test.user@prov.dev";
        userToRegister.Password = userToRegister.ConfirmPassword = "Password123$";

        // Act
        var validationResult = validator.Validate(userToRegister);

        // Assert
        validationResult.Should().NotBeNull();
        validationResult.IsValid.Should().BeFalse();
        validationResult.Errors.Should().Contain(failure =>
            failure.ErrorMessage.Equals("User name too short", StringComparison.InvariantCultureIgnoreCase));
    }
    
    [Fact]
    public void Validate_TooLongUserName_ReturnFalse()
    {
        // Arrange
        var validator = Activator.CreateInstance<RegisterInputValidator>();
        var userToRegister = Activator.CreateInstance<RegisterInput>();
        userToRegister.UserName =  string.Join(string.Empty, Enumerable.Repeat("TestUser", 100));
        userToRegister.Email = "test.user@prov.dev";
        userToRegister.Password = userToRegister.ConfirmPassword = "Password123$";

        // Act
        var validationResult = validator.Validate(userToRegister);

        // Assert
        validationResult.Should().NotBeNull();
        validationResult.IsValid.Should().BeFalse();
        validationResult.Errors.Should().Contain(failure =>
            failure.ErrorMessage.Equals("User name too long", StringComparison.InvariantCultureIgnoreCase));
    }
    
    [Fact]
    public void Validate_UserName_ContainsInvalidCharacter_ReturnFalse()
    {
        // Arrange
        var validator = Activator.CreateInstance<RegisterInputValidator>();
        var userToRegister = Activator.CreateInstance<RegisterInput>();
        userToRegister.UserName = "TestUser123*&@";
        userToRegister.Email = "test.user@prov.dev";
        userToRegister.Password = userToRegister.ConfirmPassword = "Password123$";

        // Act
        var validationResult = validator.Validate(userToRegister);

        // Assert
        validationResult.Should().NotBeNull();
        validationResult.IsValid.Should().BeFalse();
        validationResult.Errors.Should().Contain(failure =>
            failure.ErrorMessage.Equals("User name contains invalid character", StringComparison.InvariantCultureIgnoreCase));
    }
    
    [Fact]
    public void Validate_EmptyUserName_ReturnFalse()
    {
        // Arrange
        var validator = Activator.CreateInstance<RegisterInputValidator>();
        var userToRegister = Activator.CreateInstance<RegisterInput>();
        userToRegister.UserName = string.Empty;
        userToRegister.Email = "test.user@prov.dev";
        userToRegister.Password = userToRegister.ConfirmPassword = "Password123$";

        // Act
        var validationResult = validator.Validate(userToRegister);

        // Assert
        validationResult.Should().NotBeNull();
        validationResult.IsValid.Should().BeFalse();
        validationResult.Errors.Should().Contain(failure =>
            failure.ErrorMessage.Equals("User name is required", StringComparison.InvariantCultureIgnoreCase));
    }
    
    [Fact]
    public void Validate_NullReferenceUserName_ReturnFalse()
    {
        // Arrange
        var validator = Activator.CreateInstance<RegisterInputValidator>();
        var userToRegister = Activator.CreateInstance<RegisterInput>();
        userToRegister.UserName = null;
        userToRegister.Email = "test.user@prov.dev";
        userToRegister.Password = userToRegister.ConfirmPassword = "Password123$";

        // Act
        var validationResult = validator.Validate(userToRegister);

        // Assert
        validationResult.Should().NotBeNull();
        validationResult.IsValid.Should().BeFalse();
        validationResult.Errors.Should().Contain(failure =>
            failure.ErrorMessage.Equals("User name is required", StringComparison.InvariantCultureIgnoreCase));
    }
    
    [Fact]
    public void Validate_EmptyEmail_ReturnFalse()
    {
        // Arrange
        var validator = Activator.CreateInstance<RegisterInputValidator>();
        var userToRegister = Activator.CreateInstance<RegisterInput>();
        userToRegister.UserName = "TestUser";
        userToRegister.Email = string.Empty;
        userToRegister.Password = userToRegister.ConfirmPassword = "Password123$";

        // Act
        var validationResult = validator.Validate(userToRegister);

        // Assert
        validationResult.Should().NotBeNull();
        validationResult.IsValid.Should().BeFalse();
        validationResult.Errors.Should().Contain(failure =>
            failure.ErrorMessage.Equals("Email is required", StringComparison.InvariantCultureIgnoreCase));
    }
    
    [Fact]
    public void Validate_NullReferenceEmail_ReturnFalse()
    {
        // Arrange
        var validator = Activator.CreateInstance<RegisterInputValidator>();
        var userToRegister = Activator.CreateInstance<RegisterInput>();
        userToRegister.UserName = "TestUser";
        userToRegister.Email = null;
        userToRegister.Password = userToRegister.ConfirmPassword = "Password123$";

        // Act
        var validationResult = validator.Validate(userToRegister);

        // Assert
        validationResult.Should().NotBeNull();
        validationResult.IsValid.Should().BeFalse();
        validationResult.Errors.Should().Contain(failure =>
            failure.ErrorMessage.Equals("Email is required", StringComparison.InvariantCultureIgnoreCase));
    }
    
    [Fact]
    public void Validate_EmptyPassword_ReturnFalse()
    {
        // Arrange
        var validator = Activator.CreateInstance<RegisterInputValidator>();
        var userToRegister = Activator.CreateInstance<RegisterInput>();
        userToRegister.UserName = "TestUser";
        userToRegister.Email = "test.user@prov.dev";
        userToRegister.Password = string.Empty;
        userToRegister.ConfirmPassword = "Password123$";

        // Act
        var validationResult = validator.Validate(userToRegister);

        // Assert
        validationResult.Should().NotBeNull();
        validationResult.IsValid.Should().BeFalse();
        validationResult.Errors.Should().Contain(failure =>
            failure.ErrorMessage.Equals("Password is required", StringComparison.InvariantCultureIgnoreCase));
    }
    
    [Fact]
    public void Validate_NullReferencePassword_ReturnFalse()
    {
        // Arrange
        var validator = Activator.CreateInstance<RegisterInputValidator>();
        var userToRegister = Activator.CreateInstance<RegisterInput>();
        userToRegister.UserName = "TestUser";
        userToRegister.Email = "test.user@prov.dev";
        userToRegister.Password = null;
        userToRegister.ConfirmPassword = "Password123$";

        // Act
        var validationResult = validator.Validate(userToRegister);

        // Assert
        validationResult.Should().NotBeNull();
        validationResult.IsValid.Should().BeFalse();
        validationResult.Errors.Should().Contain(failure =>
            failure.ErrorMessage.Equals("Password is required", StringComparison.InvariantCultureIgnoreCase));
    }
    
    [Fact]
    public void Validate_EmptyConfirmationPassword_ReturnFalse()
    {
        // Arrange
        var validator = Activator.CreateInstance<RegisterInputValidator>();
        var userToRegister = Activator.CreateInstance<RegisterInput>();
        userToRegister.UserName = "TestUser";
        userToRegister.Email = "test.user@prov.dev";
        userToRegister.Password = "Password123$";
        userToRegister.ConfirmPassword = string.Empty;

        // Act
        var validationResult = validator.Validate(userToRegister);

        // Assert
        validationResult.Should().NotBeNull();
        validationResult.IsValid.Should().BeFalse();
        validationResult.Errors.Should().Contain(failure =>
            failure.ErrorMessage.Equals("Password confirmation is required", StringComparison.InvariantCultureIgnoreCase));
    }
    
    [Fact]
    public void Validate_NullReferenceConfirmationPassword_ReturnFalse()
    {
        // Arrange
        var validator = Activator.CreateInstance<RegisterInputValidator>();
        var userToRegister = Activator.CreateInstance<RegisterInput>();
        userToRegister.UserName = "TestUser";
        userToRegister.Email = "test.user@prov.dev";
        userToRegister.Password = "Password123$";
        userToRegister.ConfirmPassword = null;

        // Act
        var validationResult = validator.Validate(userToRegister);

        // Assert
        validationResult.Should().NotBeNull();
        validationResult.IsValid.Should().BeFalse();
        validationResult.Errors.Should().Contain(failure =>
            failure.ErrorMessage.Equals("Password confirmation is required", StringComparison.InvariantCultureIgnoreCase));
    }
}