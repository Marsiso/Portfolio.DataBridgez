using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Portfolio.DataBridgez.Domain.Entities;
using Portfolio.Databridgez.Infrastructure.Identity;

namespace Portfolio.DataBridgez.Infrastructure.UnitTests.Identity;

/// <summary>
///     <see cref="Pbkdf2PasswordHasher"/> test library.
/// </summary>
public sealed class Pbkdf2PasswordHasherTests
{
    [Fact]
    public void HashPassword_NullReferencePassword_ThrowException()
    {
        // Arrange
        var user = Activator.CreateInstance<User>();
        IPasswordHasher<User> passwordHasher = Activator.CreateInstance<Pbkdf2PasswordHasher>();
        string Action() => passwordHasher.HashPassword(user, null!);

        // Act
        var exception = Record.Exception(Action);

        // Assert
        exception.Should().NotBeNull();
    }
    
    [Fact]
    public void HashPassword_EmptyStringAsPassword_ThrowException()
    {
        // Arrange
        var user = Activator.CreateInstance<User>();
        IPasswordHasher<User> passwordHasher = Activator.CreateInstance<Pbkdf2PasswordHasher>();
        string Action() => passwordHasher.HashPassword(user, string.Empty);

        // Act
        var exception = Record.Exception(Action);

        // Assert
        exception.Should().NotBeNull();
    }
    
    [Fact]
    public void HashPassword_Password_ReturnHash()
    {
        // Arrange
        var user = Activator.CreateInstance<User>();
        IPasswordHasher<User> passwordHasher = Activator.CreateInstance<Pbkdf2PasswordHasher>();

        // Act
        var passwordHash = passwordHasher.HashPassword(user, "PasswordSample123$");
        
        // Assert
        passwordHash.Should().NotBeNullOrEmpty();
        user.Should().NotBeNull();
        user.PasswordHash.Should().NotBeNullOrEmpty();
    }
    
    
    [Fact]
    public void VerifyPassword_NullReferenceProvidedPassword_ThrowException()
    {
        // Arrange
        var user = Activator.CreateInstance<User>();
        IPasswordHasher<User> passwordHasher = Activator.CreateInstance<Pbkdf2PasswordHasher>();
        void Action() =>
            passwordHasher.VerifyHashedPassword(user, "KeyAsBase64;SaltAsBase64", null!);

        // Act
        var exception = Record.Exception(Action);

        // Assert
        exception.Should().NotBeNull();
    }
    
    [Fact]
    public void VerifyPassword_EmptyStringProvidedPassword_ThrowException()
    {
        // Arrange
        var user = Activator.CreateInstance<User>();
        IPasswordHasher<User> passwordHasher = Activator.CreateInstance<Pbkdf2PasswordHasher>();
        void Action() =>
            passwordHasher.VerifyHashedPassword(user, "KeyAsBase64;SaltAsBase64", string.Empty);

        // Act
        var exception = Record.Exception(Action);

        // Assert
        exception.Should().NotBeNull();
    }
    
    [Fact]
    public void VerifyPassword_NullReferenceHashedPassword_ThrowException()
    {
        // Arrange
        var user = Activator.CreateInstance<User>();
        IPasswordHasher<User> passwordHasher = Activator.CreateInstance<Pbkdf2PasswordHasher>();
        void Action() =>
            passwordHasher.VerifyHashedPassword(user, null!, "PasswordSample123$");

        // Act
        var exception = Record.Exception(Action);

        // Assert
        exception.Should().NotBeNull();
    }
    
    [Fact]
    public void VerifyPassword_EmptyStringHashedPassword_ThrowException()
    {
        // Arrange
        var user = Activator.CreateInstance<User>();
        IPasswordHasher<User> passwordHasher = Activator.CreateInstance<Pbkdf2PasswordHasher>();
        void Action() =>
            passwordHasher.VerifyHashedPassword(user, string.Empty, "PasswordSample123$");

        // Act
        var exception = Record.Exception(Action);

        // Assert
        exception.Should().NotBeNull();
    }
    
    [Fact]
    public void AssignableTo_IPasswordHasherTUser_Abstraction()
    {
        // Arrange
        var passwordHasher = Activator.CreateInstance<Pbkdf2PasswordHasher>();
        
        // Assert
        passwordHasher.Should().BeAssignableTo<IPasswordHasher<User>>();
    }
    
    [Fact]
    public void HashPassword_WithRandomSalt_ReturnUniquePasswordHash()
    {
        // Arrange
        var user = Activator.CreateInstance<User>();
        IPasswordHasher<User> passwordHasher = Activator.CreateInstance<Pbkdf2PasswordHasher>();
        var passwordHashesLength = 5;
        var passwordHashes = new string[passwordHashesLength];

        // Act
        for (var index = 0; index < passwordHashesLength; index++)
        {
            passwordHashes[index] = passwordHasher.HashPassword(user,"PasswordSample123$");
        }

        // Assert
        passwordHashes.Should().NotBeNullOrEmpty();
        passwordHashes.Should().OnlyContain(ph => !string.IsNullOrEmpty(ph));
        passwordHashes.Should().OnlyHaveUniqueItems();
    }

    [Fact]
    public void HashPassword_ThanVerify_ReturnSuccessVerificationResult()
    {
        // Arrange
        var user = Activator.CreateInstance<User>();
        IPasswordHasher<User> passwordHasher = Activator.CreateInstance<Pbkdf2PasswordHasher>();
        const string password = "SamplePassword123$";
        
        // Act
        var passwordHash = passwordHasher.HashPassword(user, password);
        var verificationResult = passwordHasher.VerifyHashedPassword(user, passwordHash, password);
        
        // Assert
        verificationResult.Should().Be(PasswordVerificationResult.Success);
    }
    
    [Fact]
    public void HashPassword_ThanVerify_ReturnFailedVerificationResult()
    {
        // Arrange
        var user = Activator.CreateInstance<User>();
        IPasswordHasher<User> passwordHasher = Activator.CreateInstance<Pbkdf2PasswordHasher>();
        const string password = "SamplePassword123$";
        
        // Act
        var passwordHash = passwordHasher.HashPassword(user, password);
        var verificationResult = passwordHasher.VerifyHashedPassword(user, passwordHash, string.Format("{0}{0}", password));
        
        // Assert
        verificationResult.Should().Be(PasswordVerificationResult.Failed);
    }

    [Fact]
    public void SplitPasswordHash_ReturnPasswordHashSaltAndKey()
    {
        // Arrange
        var samplePasswordHash = string.Format("{0}{1}{2}",
            Convert.ToBase64String("Key"u8.ToArray()),
            ";",
            Convert.ToBase64String("Salt"u8.ToArray()));
        
        // Act
        var result = Pbkdf2PasswordHasher.TrySplitPasswordHash(samplePasswordHash,
            out var key, out var salt);
        
        // Assert
        result.Should().Be(true);
        key.Should().NotBeNullOrEmpty();
        salt.Should().NotBeNullOrEmpty();
    }
    
    [Fact]
    public void SplitPasswordHash_EmptyStringKey_ReturnFalse()
    {
        // Arrange
        var samplePasswordHash = string.Format("{0}{1}{2}",
            string.Empty,
            ";",
            Convert.ToBase64String("Salt"u8.ToArray()));
        
        // Act
        var result = Pbkdf2PasswordHasher.TrySplitPasswordHash(samplePasswordHash,
            out var key, out var salt);
        
        // Assert
        result.Should().Be(false);
        key.Should().BeNull();
        salt.Should().BeNull();
    }
    
    [Fact]
    public void SplitPasswordHash_EmptyStringSalt_ReturnFalse()
    {
        // Arrange
        var samplePasswordHash = string.Format("{0}{1}{2}",
            Convert.ToBase64String("Key"u8.ToArray()),
            ";",
            string.Empty);
        
        // Act
        var result = Pbkdf2PasswordHasher.TrySplitPasswordHash(samplePasswordHash,
            out var key, out var salt);
        
        // Assert
        result.Should().Be(false);
        key.Should().BeNull();
        salt.Should().BeNull();
    }
    
    [Fact]
    public void SplitPasswordHash_NoDelimiter_ReturnFalse()
    {
        // Arrange
        var samplePasswordHash = string.Format("{0}{1}{2}",
            Convert.ToBase64String("Key"u8.ToArray()),
            string.Empty,
            Convert.ToBase64String("Salt"u8.ToArray()));
        
        // Act
        var result = Pbkdf2PasswordHasher.TrySplitPasswordHash(samplePasswordHash,
            out var key, out var salt);
        
        // Assert
        result.Should().Be(false);
        key.Should().BeNull();
        salt.Should().BeNull();
    }
}