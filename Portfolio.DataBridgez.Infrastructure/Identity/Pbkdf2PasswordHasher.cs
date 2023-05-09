using System.Diagnostics.CodeAnalysis;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace Portfolio.Databridgez.Infrastructure.Identity;

public sealed class Pbkdf2PasswordHasher : IPasswordHasher<AppUser>
{
    private const int KeySize = 32;
    private const int SaltSize = 16;
    private const int Cycles = 1_572_864;
    private const char Delimiter = ';';
    
    public string HashPassword(AppUser appUser, string password)
    {
        ArgumentNullException.ThrowIfNull(appUser);
        ArgumentException.ThrowIfNullOrEmpty(password);
        
        // Generate 128 bit salt
        Span<byte> salt = stackalloc byte[SaltSize];
        RandomNumberGenerator.Fill(salt);

        // Encode password
        Span<byte> encodedPassword = stackalloc byte[password.Length];
        Encoding.UTF8.GetBytes(password.AsSpan(), encodedPassword);
        
        // Derive key
        Span<byte> key = stackalloc byte[KeySize];
        Rfc2898DeriveBytes.Pbkdf2(encodedPassword, salt, key, Cycles, HashAlgorithmName.SHA512);
        
        // Format password hash
        var passwordHashBase64 = string.Format("{0}{1}{2}",
            Convert.ToBase64String(key),
            Delimiter,
            Convert.ToBase64String(salt));

        appUser.PasswordHash = passwordHashBase64;
        return passwordHashBase64;
    }

    public PasswordVerificationResult VerifyHashedPassword(AppUser appUser, string hashedPassword, string providedPassword)
    {
        ArgumentNullException.ThrowIfNull(appUser);
        ArgumentException.ThrowIfNullOrEmpty(hashedPassword);
        ArgumentException.ThrowIfNullOrEmpty(providedPassword);

        // Password hash -> key + delimiter + salt
        if (!TrySplitPasswordHash(
                hashedPassword,
                out var passwordHashKey,
                out var passwordHashSalt))
        {
            throw new FormatException();
        }
        
        // Encode password
        Span<byte> encodedPassword = stackalloc byte[providedPassword.Length];
        Encoding.UTF8.GetBytes(providedPassword.AsSpan(), encodedPassword);
        
        // Derive key
        Span<byte> key = stackalloc byte[KeySize];
        Rfc2898DeriveBytes.Pbkdf2(encodedPassword, passwordHashSalt, key, Cycles, HashAlgorithmName.SHA512);
        
        // Key comparison + protection against Timing Attacks
        var comparisonResult = CryptographicOperations.FixedTimeEquals(key, passwordHashKey);
        return comparisonResult ? PasswordVerificationResult.Success : PasswordVerificationResult.Failed;
    }

    public static bool TrySplitPasswordHash(
        ReadOnlySpan<char> passwordHash,
        [NotNullWhen(true)] out byte[]? key,
        [NotNullWhen(true)] out byte[]? salt)
    {
        key = salt = null;
        try
        {
            // Retrieve delimiter index
            var delimiterIndex = passwordHash.IndexOf(Delimiter);
            if (delimiterIndex == -1) return false;

            // Password hash -> key + salt
            var keyBase64 = passwordHash[..delimiterIndex++];
            var saltBase64 = passwordHash[delimiterIndex..];
            if (keyBase64.Length == 0 || saltBase64.Length == 0) return false;

            // Decode Base64 encoded string
            key = Convert.FromBase64String(keyBase64.ToString());
            salt = Convert.FromBase64String(saltBase64.ToString());
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
}