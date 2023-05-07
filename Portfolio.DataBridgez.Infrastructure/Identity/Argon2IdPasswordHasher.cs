using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Portfolio.DataBridgez.Domain.Entities;

namespace Portfolio.Databridgez.Infrastructure.Identity;

/// <summary>
///     Argon2id password hashing provider.
/// </summary>
public sealed class Argon2IdPasswordHasher : IPasswordHasher<User>
{
    /// <summary>
    ///     API name that provides unmanaged methods.
    /// </summary>
    private const string DllName = "libsodium";

    /// <summary>
    ///     API Argon2id algorithm identifier.
    /// </summary>
    private const int AlgorithmIdentifier = 2;

    /// <summary>
    ///     Derived key size in bytes used.
    /// </summary>
    private const int KeySize = 32;

    /// <summary>
    ///     Salt size in bytes used.
    /// </summary>
    private const int SaltSize = 16;

    /// <summary>
    ///     Reserved memory size in bits.
    /// </summary>
    private const int MemoryHardness = 262_144;

    /// <summary>
    ///     Number of cycles performed.
    /// </summary>
    private const long Cycles = 16;

    /// <summary>
    ///     Delimiter that separates salt and key.
    /// </summary>
    private const char Delimiter = ';';

    /// <summary>
    ///     Initializes the static class members.
    /// </summary>
    static Argon2IdPasswordHasher()
    {
        sodium_init();
    }
    
    /// <summary>
    ///     Computes password hash.
    /// </summary>
    /// <param name="user"></param>
    /// <param name="password">Password to be hashed.</param>
    /// <returns>Password hash that consist of key and salt encoded as Base64 strings separated by delimiter.</returns>
    public string HashPassword(User user, string? password)
    {
        ArgumentNullException.ThrowIfNull(user);
        ArgumentException.ThrowIfNullOrEmpty(password);

        // Generate salt
        var salt = new byte[SaltSize];
        randombytes_buf(salt, SaltSize);
        
        // Encode password
        var encodedPassword = new byte[password.Length];
        Encoding.UTF8.GetBytes(password, encodedPassword);

        // Derive key
        var key = new byte[KeySize];
        if (crypto_pwhash(
                key,
                KeySize, 
                encodedPassword, 
                encodedPassword.Length,
                salt,
                Cycles,
                MemoryHardness,
                AlgorithmIdentifier) != 0)
        {
            throw new OutOfMemoryException("Sodium library out of memory exception");
        }
        
        // Format password hash
        var passwordHash = string.Format("{0}{1}{2}",
            Convert.ToBase64String(key),
            Delimiter,
            Convert.ToBase64String(salt));

        user.PasswordHash = passwordHash;
        return passwordHash;
    }

    /// <summary>
    ///     Compares password against its password hash.
    /// </summary>
    /// <param name="user"></param>
    /// <param name="hashedPassword">Password to be compared.</param>
    /// <param name="providedPassword">Password hash that consists of salt and key encoded as Base64 strings and delimiter.</param>
    /// <exception cref="ArgumentNullException">Thrown when either password or its hash is null reference object or empty string</exception>
    /// <returns>True - Password matches, False - Otherwise.</returns>
    public PasswordVerificationResult VerifyHashedPassword(User user, string hashedPassword, string providedPassword)
    {
        ArgumentNullException.ThrowIfNull(user);
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
        var encodedPassword = new byte[providedPassword.Length];
        Encoding.UTF8.GetBytes(providedPassword, encodedPassword);

        // Derive key
        var key = new byte[KeySize];
        if (crypto_pwhash(
                key,
                KeySize, 
                encodedPassword, 
                encodedPassword.Length,
                passwordHashSalt,
                Cycles,
                MemoryHardness,
                AlgorithmIdentifier) != 0)
        {
            throw new OutOfMemoryException("Sodium library out of memory exception");
        }

        // Key comparison + protection against Timing Attacks
        var comparisonResult = CryptographicOperations.FixedTimeEquals(key, passwordHashKey);
        return comparisonResult ? PasswordVerificationResult.Success : PasswordVerificationResult.Failed;
    }

    /// <summary>
    ///     Separates password hash salt and key using delimiter.
    /// </summary>
    /// <param name="passwordHash">Salt and key encoded as Base64 strings separated by delimiter.</param>
    /// <param name="key">Hashed password and salt.</param>
    /// <param name="salt">Salt used to hash password.</param>
    /// <returns>Decoded Base64 string encoded key and salt.</returns>
    public static bool TrySplitPasswordHash(
        ReadOnlySpan<char> passwordHash,
        [NotNullWhen(true)] out byte[]? key,
        [NotNullWhen(true)] out byte[]? salt)
    {
        key = salt = null;
        try
        {
            // Get delimiter index
            var delimiterIndex = passwordHash.IndexOf(Delimiter);
            if (delimiterIndex == -1) return false;

            // Split password hash
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

    /// <summary>
    ///     Initializes the library, should be called before accessing library methods.
    /// </summary>
    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    private static extern void sodium_init();

    /// <summary>
    ///     Generates salt.
    /// </summary>
    /// <param name="buffer">Buffer to be filled with random sequence.</param>
    /// <param name="bufferSize">Buffer size in bytes.</param>
    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    private static extern void randombytes_buf(byte[] buffer, int bufferSize);

    /// <summary>
    ///     Derives key from password and salt using Argon2 family algorithm.
    /// </summary>
    /// <param name="key">Buffer to be used as key storage.</param>
    /// <param name="keySize">Buffer size in bytes.</param>
    /// <param name="passwordBytes">Password to be hashed.</param>
    /// <param name="passwordBytesSize">Password size in bytes.</param>
    /// <param name="salt">Salt to be hashed together with password.</param>
    /// <param name="cycles">Maximum amount of computations to perform.</param>
    /// <param name="memoryHardness">Memory hardness is  maximum amount of RAM in bytes that the function will use.</param>
    /// <param name="algorithm">Identifier for algorithm to use and should be set to one of following values:
    ///     0 (default) - Currently recommended algorithm, which can change from one version of libsodium to another.
    ///     1 (Argon2i13) - Version 1.3 of Argon2i algorithm.
    ///     2 (Argon2id13) - Version 1.3 of Argon2id algorithm.
    /// </param>
    /// <returns>0 - Success, -1 - Failure such as out of memory exception.</returns>
    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    private static extern int crypto_pwhash(
        byte[] key,
        long keySize,
        byte[] passwordBytes,
        long passwordBytesSize,
        byte[] salt,
        long cycles,
        int memoryHardness,
        int algorithm);
}