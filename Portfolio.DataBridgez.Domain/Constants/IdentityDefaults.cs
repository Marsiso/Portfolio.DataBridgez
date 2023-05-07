using Microsoft.AspNetCore.Identity;

namespace Portfolio.DataBridgez.Domain.Constants;

/// <summary>
///     ASP.NET Core Identity <see cref="IdentityOptions"/> defaults. 
/// </summary>
public static class IdentityDefaults
{
    /// <summary>
    ///     ASP.NET Core Identity <see cref="UserOptions"/> defaults.
    /// </summary>
    public static class User
    {
        /// <summary>
        ///     ASP.NET Core Identity <see cref="UserOptions"/> property
        ///     <see cref="UserOptions.AllowedUserNameCharacters"/> defaults.
        /// </summary>
        public const string AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        
        /// <summary>
        ///     ASP.NET Core Identity <see cref="UserOptions"/> property
        ///     <see cref="UserOptions.RequireUniqueEmail"/> defaults.
        /// </summary>
        public const bool RequireUniqueEmail = true;
    }
    
    /// <summary>
    ///     ASP.NET Core Identity <see cref="LockoutOptions"/> defaults.
    /// </summary>
    public static class Lockout
    {
        /// <summary>
        ///     ASP.NET Core Identity <see cref="LockoutOptions"/> property
        ///     <see cref="LockoutOptions.DefaultLockoutTimeSpan"/> defaults.
        /// </summary>
        public static readonly TimeSpan DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
        
        /// <summary>
        ///     ASP.NET Core Identity <see cref="LockoutOptions"/> property
        ///     <see cref="LockoutOptions.AllowedForNewUsers"/> defaults.
        /// </summary>
        public const bool AllowedForNewUsers = true;
        
        /// <summary>
        ///     ASP.NET Core Identity <see cref="LockoutOptions"/> property
        ///     <see cref="LockoutOptions.MaxFailedAccessAttempts"/> defaults.
        /// </summary>
        public const int MaxFailedAccessAttempts = 5;
    }
    
    /// <summary>
    ///     ASP.NET Core Identity <see cref="PasswordOptions"/> defaults.
    /// </summary>
    public static class Password
    {
        /// <summary>
        ///     ASP.NET Core Identity <see cref="PasswordOptions"/> property
        ///     <see cref="PasswordOptions.RequireDigit"/> defaults.
        /// </summary>
        public const bool RequireDigit = true;
        
        /// <summary>
        ///     ASP.NET Core Identity <see cref="PasswordOptions"/> property
        ///     <see cref="PasswordOptions.RequiredLength"/> defaults.
        /// </summary>
        public const int RequiredLength = 7;
        
        /// <summary>
        ///     ASP.NET Core Identity <see cref="PasswordOptions"/> property
        ///     <see cref="PasswordOptions.RequiredUniqueChars"/> defaults.
        /// </summary>
        public const int RequiredUniqueChars = 1;
        
        /// <summary>
        ///     ASP.NET Core Identity <see cref="PasswordOptions"/> property
        ///     <see cref="PasswordOptions.RequireLowercase"/> defaults.
        /// </summary>
        public const bool RequireLowercase = true;
        
        /// <summary>
        ///     ASP.NET Core Identity <see cref="PasswordOptions"/> property
        ///     <see cref="PasswordOptions.RequireNonAlphanumeric"/> defaults.
        /// </summary>
        public const bool RequireNonAlphanumeric = true;
        
        /// <summary>
        ///     ASP.NET Core Identity <see cref="PasswordOptions"/> property
        ///     <see cref="PasswordOptions.RequireUppercase"/> defaults.
        /// </summary>
        public const bool RequireUppercase = true;
    }
    
    /// <summary>
    ///     ASP.NET Core Identity <see cref="StoreOptions"/> defaults.
    /// </summary>
    public static class Stores
    {
        /// <summary>
        ///     ASP.NET Core Identity <see cref="StoreOptions"/> property
        ///     <see cref="StoreOptions.MaxLengthForKeys"/> defaults.
        /// </summary>
        public const int MaxLengthForKeys = 450;
    }

    /// <summary>
    ///     ASP.NET Core Identity <see cref="SignInOptions"/> defaults.
    /// </summary>
    public static class SignIn
    {
        /// <summary>
        ///     ASP.NET Core Identity <see cref="SignInOptions"/> property
        ///     <see cref="SignInOptions.RequireConfirmedEmail"/> defaults.
        /// </summary>
        public const bool RequireConfirmedEmail = true;
    }

    /// <summary>
    ///     Applies defaults to ASP.NET Core Identity <see cref="IdentityOptions"/>. 
    /// </summary>
    /// <param name="options"></param>
    public static void ApplyIdentityDefaults(this IdentityOptions options)
    {
        #region User
        
        options.User.AllowedUserNameCharacters = User.AllowedUserNameCharacters;
        options.User.RequireUniqueEmail = User.RequireUniqueEmail;
        
        #endregion
            
        #region Lockout
        
        options.Lockout.DefaultLockoutTimeSpan = Lockout.DefaultLockoutTimeSpan;
        options.Lockout.AllowedForNewUsers = Lockout.AllowedForNewUsers;
        options.Lockout.MaxFailedAccessAttempts = Lockout.MaxFailedAccessAttempts;
        
        #endregion

        #region Password
        
        options.Password.RequireDigit = Password.RequireDigit;
        options.Password.RequiredLength = Password.RequiredLength;
        options.Password.RequireLowercase = Password.RequireLowercase;
        options.Password.RequireNonAlphanumeric = Password.RequireNonAlphanumeric;
        options.Password.RequireUppercase = Password.RequireUppercase;
        options.Password.RequiredUniqueChars = Password.RequiredUniqueChars;
        
        #endregion

        #region Stores

        options.Stores.MaxLengthForKeys = Stores.MaxLengthForKeys;

        #endregion

        #region SignIn

        options.SignIn.RequireConfirmedEmail = SignIn.RequireConfirmedEmail;

        #endregion
    }
}