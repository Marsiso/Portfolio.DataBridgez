using Portfolio.DataBridgez.Domain.Entities;

namespace Portfolio.DataBridgez.Domain.Constants;

/// <summary>
///     Defaults for database and its entities in database system.
/// </summary>
public static class DbDefaults
{
    /// <summary>
    ///     Schema name in use.
    /// </summary>
    public const string SchemaName = "dbo";
    
    public static class Tables
    {
        /// <summary>
        ///     Defaults for <see cref="Entities.User"/> entity.
        /// </summary>
        public static class Users
        {
            public const string TableName = "users";
            
            public static class Columns
            {
                #region Identifier

                public const string IdentifierFieldName = "id";

                #endregion
                
                #region UserName
                
                public const string UserNameFieldName = "user_name";
                public const string NormalizedUserNameFieldName = "normalized_user_name";
                public const int MinimumUserNameLength = 7;
                public const int MaximumUserNameLength = 256;
                
                #endregion
                
                #region EmailAddress
                
                public const string EmailFieldName = "email_address";
                public const string EmailConfirmedFieldName = "email_address_confirmed";
                public const string NormalizedEmailFieldName = "normalized_email_address";
                public const int MaximumEmailLength = 256;
                
                #endregion

                #region PhoneNumber

                public const string PhoneNumberFieldName = "phone_number";
                public const string PhoneNumberConfirmedFieldName = "phone_number_confirmed";

                #endregion
                
                #region Security
                
                public const string PasswordHashFieldName = "password_hash";
                public const string SecurityStampFieldName = "security_stamp";
                public const string ConcurrencyStampFieldName = "concurrency_stamp";
                public const string TwoFactorEnabledFieldName = "two_factor_enabled";
                public const string LockOutEndFieldName = "date_end_lock_out";
                public const string LockOutEnabledFieldName = "lock_out_enabled";
                public const string AccessFailedCountFieldName = "access_failed_count";

                #endregion
            }
        }

        /// <summary>
        ///     Defaults for <see cref="Role"/> entity.
        /// </summary>
        public static class Roles
        {
            public const string TableName = "roles";

            public static class Columns
            {
                public const string IdentifierFieldName = "id";
                public const string NameFieldName = "name";
                public const int MinimumNameLength = 2;
                public const int MaximumNameLength = 256;
                public const string NormalizedNameFieldName = "normalized_name";
                public const string ConcurrencyStampFieldName = "concurrency_stamp";
            }
        }
        
        /// <summary>
        ///     Defaults for <see cref="Entities.UserRole"/> entity.
        /// </summary>
        public static class UserRoles
        {
            public const string TableName = "user_roles";
            
            public static class Columns
            {
                public const string UserIdentifierFieldName = "user_id";
                public const string RoleIdentifierFieldName = "role_id";
            }
        }
        
        /// <summary>
        ///     Defaults for <see cref="UserTokens"/> entity.
        /// </summary>
        public static class UserTokens
        {
            public const string TableName = "user_tokens";
            
            public static class Columns
            {
                public const string UserIdentifierFieldName = "user_id";
                public const string LoginProviderFieldName = "login_provider";
                public const string NameFieldName = "name";
                public const string ValueFieldName = "value";
                public const int MaximumNameLength = 450;
                public const int MaximumLoginProviderLength = 450;
            }
        }
        
        /// <summary>
        ///     Defaults for <see cref="Entities.RoleClaim"/> entity.
        /// </summary>
        public static class RoleClaims
        {
            public const string TableName = "role_claims";

            public static class Columns
            {
                public const string IdentifierFieldName = "id";
                public const string RoleIdentifierFieldName = "role_id";
                public const string ClaimTypeFieldName = "claim_type";
                public const string ClaimValueFieldName = "claim_value";
            }
        }
        
        /// <summary>
        ///     Defaults for <see cref="Entities.UserClaim"/> entity.
        /// </summary>
        public static class UserClaims
        {
            public const string TableName = "user_claims";

            public static class Columns
            {
                public const string IdentifierFieldName = "id";
                public const string UserIdentifierFieldName = "user_id";
                public const string ClaimTypeFieldName = "claim_type";
                public const string ClaimValueFieldName = "claim_value";
            }
        }

        /// <summary>
        ///     Defaults for <see cref="Entities.UserLogin"/> entity.
        /// </summary>
        public static class UserLogins
        {
            public const string TableName = "user_logins";
            
            public static class Columns
            {
                public const string UserIdentifierFieldName = "id";
                public const string LoginProviderFieldName = "login_provider";
                public const string ProviderKeyFieldName = "provider_key";
                public const string ProviderDisplayNameFieldName = "provider_display_name";
                public const int MaximumProviderKeyLength = 450;
                public const int MaximumLoginProviderLength = 450;
            }
        }
    }
}