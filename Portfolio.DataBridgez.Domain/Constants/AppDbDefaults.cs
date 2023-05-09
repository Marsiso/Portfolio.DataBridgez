using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Portfolio.DataBridgez.Domain.Entities;

namespace Portfolio.DataBridgez.Domain.Constants;

/// <summary>
///     Code-first approach defaults for application entities.
/// </summary>
public static class AppDbDefaults
{
    /// <summary>
    ///     The database schema name.
    /// </summary>
    public const string SchemaName = "dbo";
    
    /// <summary>
    ///     Validation attribute <see cref="StringLengthAttribute" /> message template.
    /// </summary>
    public const string StringLengthMessageTemplate = "{0} must be at least {2} characters and up to {1} characters long";
    
    /// <summary>
    ///     Validation attribute <see cref="StringLengthAttribute" /> message template.
    /// </summary>
    public const string MaximumStringLengthMessageTemplate = "{0} must be up to {1} characters long";
    
    /// <summary>
    ///     Validation attribute <see cref="StringLengthAttribute" /> message template.
    /// </summary>
    public const string RequireMessageTemplate = "{0} is required";

    /// <summary>
    ///     Database table defaults.
    /// </summary>
    public static class Tables
    {
        /// <summary>
        ///     The <see cref="Entities.Entity" /> table base defaults.
        /// </summary>
        public static class Entity
        {
            #region PrimaryKey

            /// <summary>
            ///     The <see cref="Entities.Entity.Id" /> column name.
            /// </summary>
            public const string PrimaryKeyColumnName = "id";
            
            /// <summary>
            ///     The <see cref="Entities.Entity.Id" /> display name.
            /// </summary>
            public const string PrimaryKeyDisplayName = "id";

            #endregion
        }
        
        /// <summary>
        ///     The <see cref="AppUser" /> table defaults.
        /// </summary>
        public static class AppUsers
        {
            /// <summary>
            ///     The <see cref="AppUser" /> table name.
            /// </summary>
            public const string TableName = "users";

            /// <summary>
            ///     The <see cref="AppUser" /> table columns defaults.
            /// </summary>
            public static class Column
            {
                #region UserName

                /// <summary>
                ///     The <see cref="AppUser.UserName" /> column name.
                /// </summary>
                public const string UserNameColumnName = "user_name";
                
                /// <summary>
                ///     The <see cref="AppUser.UserName" /> display name.
                /// </summary>
                public const string UserNameDisplayName = "User name";

                /// <summary>
                ///     The <see cref="AppUser.NormalizedEmail" /> column name.
                /// </summary>
                public const string NormalizedUserNameColumnName = "normalized_user_name";
                
                /// <summary>
                ///     The <see cref="AppUser.NormalizedEmail" /> display name.
                /// </summary>
                public const string NormalizedUserNameDisplayName = "Normalized user name";

                /// <summary>
                ///     The application <see cref="AppUser.UserName" /> maximum length.
                /// </summary>
                public const int MaximumUserNameLengthConstraint = 256;
                
                /// <summary>
                ///     The <see cref="AppUser.UserName" /> required length.
                /// </summary>
                public const int MinimumUserNameLengthConstraint = 2;

                #endregion

                #region EmailAddress

                /// <summary>
                ///     The <see cref="AppUser.Email" /> column name.
                /// </summary>
                public const string EmailColumnName = "email";
                
                /// <summary>
                ///     The <see cref="AppUser.Email" /> display name.
                /// </summary>
                public const string EmailDisplayName = "Email";

                /// <summary>
                ///     The <see cref="AppUser.EmailConfirmed" /> display name.
                /// </summary>
                public const string EmailConfirmedColumnName = "email_confirmed";
                
                /// <summary>
                ///     The <see cref="AppUser.EmailConfirmed" /> display name.
                /// </summary>
                public const string EmailConfirmedDisplayName = "Email confirmed flag";

                /// <summary>
                ///     The <see cref="AppUser.NormalizedEmail" /> display name.
                /// </summary>
                public const string NormalizedEmailColumnName = "normalized_email";
                
                /// <summary>
                ///     The <see cref="AppUser.NormalizedEmail" /> display name.
                /// </summary>
                public const string NormalizedEmailDisplayName = "Normalized email";

                /// <summary>
                ///     The <see cref="AppUser.NormalizedEmail" /> display name.
                /// </summary>
                public const int MaximumEmailLengthConstraint = 256;

                #endregion

                #region Security

                /// <summary>
                ///     The <see cref="AppUser.PasswordHash" /> column name.
                /// </summary>
                public const string PasswordHashColumnName = "password_hash";
                
                /// <summary>
                ///     The <see cref="AppUser.PasswordHash" /> display name.
                /// </summary>
                public const string PasswordHashDisplayName = "Password hash";
                
                /// <summary>
                ///     The <see cref="AppUser.PasswordHash" /> display name.
                /// </summary>
                public const int PasswordHashLengthConstraint = 32;

                /// <summary>
                ///     The <see cref="AppUser.SecurityStamp" /> column name.
                /// </summary>
                public const string SecurityStampColumnName = "security_stamp";

                /// <summary>
                ///     The <see cref="AppUser.SecurityStamp" /> display name.
                /// </summary>
                public const string SecurityStampDisplayName = "Security stamp";

                /// <summary>
                ///     The <see cref="AppUser.SecurityStamp" /> column name.
                /// </summary>
                public const string ConcurrencyStampColumnName = "concurrency_stamp";
                
                /// <summary>
                ///     The <see cref="AppUser.SecurityStamp" /> display name.
                /// </summary>
                public const string ConcurrencyStampDisplayName = "Concurrency stamp";

                /// <summary>
                ///     The <see cref="AppUser.LockoutEnd" /> column name.
                /// </summary>
                public const string LockoutEndColumnName = "date_end_lock_out";
                
                /// <summary>
                ///     The <see cref="AppUser.LockoutEnd" /> display name.
                /// </summary>
                public const string LockoutEndDisplayName = "Lockout end";

                /// <summary>
                ///     The <see cref="AppUser.LockoutEnabled" /> column name.
                /// </summary>
                public const string LockoutEnabledColumnName = "lock_out_enabled";
                
                /// <summary>
                ///     The <see cref="AppUser.LockoutEnabled" /> display name.
                /// </summary>
                public const string LockoutEnabledDisplayName = "Lockout enabled";

                /// <summary>
                ///     The <see cref="AppUser.AccessFailedCount" /> column name.
                /// </summary>
                public const string AccessFailedCountColumnName = "access_failed_count";
                
                /// <summary>
                ///     The <see cref="AppUser.AccessFailedCount" /> column name.
                /// </summary>
                public const string AccessFailedCountDisplayName = "Access failed count";

                #endregion
            }
        }
    }
}