using Microsoft.AspNetCore.Identity;

namespace Portfolio.DataBridgez.Domain.Constants;

/// <summary>
///     Code-first approach defaults for application database and its entities.
/// </summary>
public static class DbDefaults
{
    /// <summary>
    ///     Database schema name that tables are mapped to.
    /// </summary>
    public const string SchemaName = "dbo";

    /// <summary>
    ///     Database table defaults.
    /// </summary>
    public static class Table
    {
        /// <summary>
        ///     Application <see cref="Entities.User" /> entity defaults.
        /// </summary>
        public static class User
        {
            /// <summary>
            ///     Table name that application <see cref="Entities.User" /> entity is mapped to.
            /// </summary>
            public const string TableName = "users";

            /// <summary>
            ///     Application <see cref="Entities.User" /> entity column defaults.
            /// </summary>
            public static class Column
            {
                #region PrimaryKey

                /// <summary>
                ///     Column name that application <see cref="Entities.User" /> property
                ///     <see cref="IdentityUser{TKey}.Id" /> is mapped to.
                /// </summary>
                public const string PrimaryKeyColumnName = "id";

                #endregion

                #region UserName

                /// <summary>
                ///     Column name that application <see cref="Entities.User" /> property
                ///     <see cref="IdentityUser{TKey}.UserName" /> is mapped to.
                /// </summary>
                public const string UserNameColumnName = "user_name";

                /// <summary>
                ///     Column name that application <see cref="Entities.User" /> property
                ///     <see cref="IdentityUser{TKey}.NormalizedUserName" /> is mapped to.
                /// </summary>
                public const string NormalizedUserNameColumnName = "normalized_user_name";

                /// <summary>
                ///     Column that application <see cref="Entities.User" /> property
                ///     <see cref="IdentityUser{TKey}.UserName" /> is mapped to constraint.
                /// </summary>
                public const int MaximumUserNameLengthConstraint = 256;

                #endregion

                #region EmailAddress

                /// <summary>
                ///     Column that application <see cref="Entities.User" /> property
                ///     <see cref="IdentityUser{TKey}.Email" /> is mapped to.
                /// </summary>
                public const string EmailColumnName = "email_address";

                /// <summary>
                ///     Column that application <see cref="Entities.User" /> property
                ///     <see cref="IdentityUser{TKey}.EmailConfirmed" /> is mapped to.
                /// </summary>
                public const string EmailConfirmedColumnName = "email_address_confirmed";

                /// <summary>
                ///     Column that application <see cref="Entities.User" /> property
                ///     <see cref="IdentityUser{TKey}.NormalizedEmail" /> is mapped to.
                /// </summary>
                public const string NormalizedEmailColumnName = "normalized_email_address";

                /// <summary>
                ///     Column that application <see cref="Entities.User" /> property <see cref="Entities.User.Email" />
                ///     or <see cref="IdentityUser{TKey}.NormalizedEmail" /> is mapped to constraint.
                /// </summary>
                public const int MaximumEmailLengthConstraint = 256;

                #endregion

                #region PhoneNumber

                /// <summary>
                ///     Column that application <see cref="Entities.User" /> property
                ///     <see cref="IdentityUser{TKey}.PhoneNumber" /> is mapped to.
                /// </summary>
                public const string PhoneNumberColumnName = "phone_number";

                /// <summary>
                ///     Column that application <see cref="Entities.User" /> property
                ///     <see cref="IdentityUser{TKey}.PhoneNumberConfirmed" /> is mapped to.
                /// </summary>
                public const string PhoneNumberConfirmedColumnName = "phone_number_confirmed";

                #endregion

                #region Security

                /// <summary>
                ///     Column that application <see cref="Entities.User" /> property
                ///     <see cref="IdentityUser{TKey}.PasswordHash" /> is mapped to.
                /// </summary>
                public const string PasswordHashColumnName = "password_hash";

                /// <summary>
                ///     Column that application <see cref="Entities.User" /> property
                ///     <see cref="IdentityUser{TKey}.SecurityStamp" /> is mapped to.
                /// </summary>
                public const string SecurityStampFieldName = "security_stamp";

                /// <summary>
                ///     Column that application <see cref="Entities.User" /> property
                ///     <see cref="IdentityUser{TKey}.PhoneNumberConfirmed" /> is mapped to.
                /// </summary>
                public const string ConcurrencyStampFieldName = "concurrency_stamp";

                /// <summary>
                ///     Column that application <see cref="Entities.User" /> property
                ///     <see cref="IdentityUser{TKey}.TwoFactorEnabled" /> is mapped to.
                /// </summary>
                public const string TwoFactorEnabledColumnName = "two_factor_enabled";

                /// <summary>
                ///     Column that application <see cref="Entities.User" /> property
                ///     <see cref="IdentityUser{TKey}.LockoutEnd" /> is mapped to.
                /// </summary>
                public const string LockoutEndColumnName = "date_end_lock_out";

                /// <summary>
                ///     Column that application <see cref="Entities.User" /> property
                ///     <see cref="IdentityUser{TKey}.LockoutEnabled" /> is mapped to.
                /// </summary>
                public const string LockoutEnabledColumnName = "lock_out_enabled";

                /// <summary>
                ///     Column that application <see cref="Entities.User" /> property
                ///     <see cref="IdentityUser{TKey}.AccessFailedCount" /> is mapped to.
                /// </summary>
                public const string AccessFailedCountFieldName = "access_failed_count";

                #endregion
            }
        }

        /// <summary>
        ///     Application <see cref="Entities.Role" /> entity defaults.
        /// </summary>
        public static class Role
        {
            /// <summary>
            ///     Table name that application <see cref="Entities.Role" /> entity is mapped to.
            /// </summary>
            public const string TableName = "roles";

            /// <summary>
            ///     Application <see cref="Entities.Role" /> entity column defaults.
            /// </summary>
            public static class Column
            {
                #region PrimaryKey

                /// <summary>
                ///     Column that application <see cref="Entities.Role" /> property
                ///     <see cref="IdentityRole{TKey}.Id" /> is mapped to.
                /// </summary>
                public const string PrimaryKeyColumnName = "id";

                #endregion

                /// <summary>
                ///     Column that application <see cref="Entities.Role" /> property
                ///     <see cref="IdentityRole{TKey}.ConcurrencyStamp" /> is mapped to.
                /// </summary>
                public const string ConcurrencyStampColumnName = "concurrency_stamp";

                #region Name

                /// <summary>
                ///     Column that application <see cref="Entities.Role" /> property
                ///     <see cref="IdentityRole{TKey}.Name" /> is mapped to.
                /// </summary>
                public const string NameColumnName = "name";

                /// <summary>
                ///     Column that application <see cref="Entities.Role" /> property
                ///     <see cref="IdentityRole{TKey}.Name" /> is mapped to constraint.
                /// </summary>
                public const int MaximumNameLengthConstraint = 256;

                /// <summary>
                ///     Column that application <see cref="Entities.Role" /> property
                ///     <see cref="IdentityRole{TKey}.NormalizedName" /> is mapped to.
                /// </summary>
                public const string NormalizedNameColumnName = "normalized_name";

                #endregion
            }
        }

        /// <summary>
        ///     Application <see cref="Entities.UserRole" /> entity defaults.
        /// </summary>
        public static class UserRole
        {
            /// <summary>
            ///     Table name that application <see cref="Entities.UserRole" /> entity is mapped to.
            /// </summary>
            public const string TableName = "user_roles";

            /// <summary>
            ///     Application <see cref="Entities.UserRole" /> entity column defaults.
            /// </summary>
            public static class Column
            {
                /// <summary>
                ///     Column that application <see cref="Entities.UserRole" /> property
                ///     <see cref="IdentityUserRole{TKey}.UserId" /> is mapped to.
                /// </summary>
                public const string UserPrimaryKeyColumnName = "user_id";

                /// <summary>
                ///     Column that application <see cref="Entities.UserRole" /> property
                ///     <see cref="IdentityUserRole{TKey}.RoleId" /> is mapped to.
                /// </summary>
                public const string RolePrimaryKeyColumnName = "role_id";
            }
        }

        /// <summary>
        ///     Application <see cref="Entities.UserToken" /> entity defaults.
        /// </summary>
        public static class UserToken
        {
            /// <summary>
            ///     Table name that application <see cref="Entities.UserToken" /> is mapped to.
            /// </summary>
            public const string TableName = "user_tokens";

            /// <summary>
            ///     Application <see cref="Entities.UserToken" /> entity column defaults.
            /// </summary>
            public static class Column
            {
                /// <summary>
                ///     Column that application <see cref="Entities.UserToken" /> property
                ///     <see cref="IdentityUserToken{TKey}.UserId" /> is mapped to.
                /// </summary>
                public const string UserPrimaryKeyColumnName = "user_id";

                /// <summary>
                ///     Column that application <see cref="Entities.UserToken" /> property
                ///     <see cref="IdentityUserToken{TKey}.LoginProvider" /> is mapped to.
                /// </summary>
                public const string LoginProviderColumnName = "login_provider";

                /// <summary>
                ///     Column that application <see cref="Entities.UserToken" /> property
                ///     <see cref="IdentityUserToken{TKey}.Name" /> is mapped to.
                /// </summary>
                public const string NameColumnName = "name";

                /// <summary>
                ///     Column that application <see cref="Entities.UserToken" /> property
                ///     <see cref="IdentityUserToken{TKey}.Value" /> is mapped to.
                /// </summary>
                public const string ValueColumnName = "value";

                /// <summary>
                ///     Column that application <see cref="Entities.UserToken" /> property
                ///     <see cref="IdentityUserToken{TKey}.Name" /> is mapped to constraint.
                /// </summary>
                public const int MaximumNameLengthConstraint = 450;

                /// <summary>
                ///     Column that application <see cref="Entities.UserToken" /> property
                ///     <see cref="IdentityUserToken{TKey}.LoginProvider" /> is mapped to constraint.
                /// </summary>
                public const int MaximumLoginProviderLengthConstraint = 450;
            }
        }

        /// <summary>
        ///     Application <see cref="Entities.RoleClaim" /> entity defaults.
        /// </summary>
        public static class RoleClaim
        {
            /// <summary>
            ///     Table name that application <see cref="Entities.RoleClaim" /> is mapped to.
            /// </summary>
            public const string TableName = "role_claims";

            /// <summary>
            ///     Application <see cref="Entities.RoleClaim" /> entity column defaults.
            /// </summary>
            public static class Column
            {
                /// <summary>
                ///     Column that application <see cref="Entities.RoleClaim" /> property
                ///     <see cref="IdentityRoleClaim{TKey}.Id" /> is mapped to constraint.
                /// </summary>
                public const string PrimaryKeyColumnName = "id";

                /// <summary>
                ///     Column that application <see cref="Entities.RoleClaim" /> property
                ///     <see cref="IdentityRoleClaim{TKey}.RoleId" /> is mapped to constraint.
                /// </summary>
                public const string RolePrimaryKeyColumnName = "role_id";

                /// <summary>
                ///     Column that application <see cref="Entities.RoleClaim" /> property
                ///     <see cref="IdentityRoleClaim{TKey}.ClaimType" /> is mapped to constraint.
                /// </summary>
                public const string ClaimTypeColumnName = "claim_type";

                /// <summary>
                ///     Column that application <see cref="Entities.RoleClaim" /> property
                ///     <see cref="IdentityRoleClaim{TKey}.ClaimValue" /> is mapped to constraint.
                /// </summary>
                public const string ClaimValueColumnName = "claim_value";
            }
        }

        /// <summary>
        ///     Application <see cref="Entities.UserClaim" /> entity defaults.
        /// </summary>
        public static class UserClaim
        {
            /// <summary>
            ///     Table name that application <see cref="Entities.UserClaim" /> is mapped to.
            /// </summary>
            public const string TableName = "user_claims";

            /// <summary>
            ///     Application <see cref="Entities.UserClaim" /> entity column defaults.
            /// </summary>
            public static class Columns
            {
                /// <summary>
                ///     Column that application <see cref="Entities.UserClaim" /> property
                ///     <see cref="IdentityUserClaim{TKey}.ClaimValue" /> is mapped to constraint.
                /// </summary>
                public const string PrimaryKeyColumnName = "id";

                /// <summary>
                ///     Column that application <see cref="Entities.UserClaim" /> property
                ///     <see cref="IdentityUserClaim{TKey}.UserId" /> is mapped to constraint.
                /// </summary>
                public const string UserPrimaryKeyColumnName = "user_id";

                /// <summary>
                ///     Column that application <see cref="Entities.UserClaim" /> property
                ///     <see cref="IdentityUserClaim{TKey}.ClaimType" /> is mapped to constraint.
                /// </summary>
                public const string ClaimTypeColumnName = "claim_type";

                /// <summary>
                ///     Column that application <see cref="Entities.UserClaim" /> property
                ///     <see cref="IdentityUserClaim{TKey}.ClaimValue" /> is mapped to constraint.
                /// </summary>
                public const string ClaimValueColumnName = "claim_value";
            }
        }

        /// <summary>
        ///     Application <see cref="Entities.UserLogin" /> entity defaults.
        /// </summary>
        public static class UserLogin
        {
            /// <summary>
            ///     Table name that application <see cref="Entities.UserLogin" /> is mapped to.
            /// </summary>
            public const string TableName = "user_logins";

            /// <summary>
            ///     Application <see cref="Entities.UserLogin" /> entity column defaults.
            /// </summary>
            public static class Column
            {
                /// <summary>
                ///     Column that application <see cref="Entities.UserLogin" /> property
                ///     <see cref="IdentityUserLogin{TKey}.UserId" /> is mapped to.
                /// </summary>
                public const string UserPrimaryKeyColumnName = "user_id";

                /// <summary>
                ///     Column that application <see cref="Entities.UserLogin" /> property
                ///     <see cref="IdentityUserLogin{TKey}.LoginProvider" /> is mapped to.
                /// </summary>
                public const string LoginProviderColumnName = "login_provider";

                /// <summary>
                ///     Column that application <see cref="Entities.UserLogin" /> property
                ///     <see cref="IdentityUserLogin{TKey}.ProviderKey" /> is mapped to.
                /// </summary>
                public const string ProviderKeyColumnName = "provider_key";

                /// <summary>
                ///     Column that application <see cref="Entities.UserLogin" /> property
                ///     <see cref="IdentityUserLogin{TKey}.ProviderDisplayName" /> is mapped to.
                /// </summary>
                public const string ProviderDisplayNameColumnName = "provider_display_name";

                /// <summary>
                ///     Column that application <see cref="Entities.UserLogin" /> property
                ///     <see cref="IdentityUserLogin{TKey}.ProviderKey" /> is mapped to constraint.
                /// </summary>
                public const int MaximumProviderKeyLengthConstraint = 450;

                /// <summary>
                ///     Column that application <see cref="Entities.UserLogin" /> property
                ///     <see cref="IdentityUserLogin{TKey}.LoginProvider" /> is mapped to constraint.
                /// </summary>
                public const int MaximumLoginProviderLengthConstraint = 450;
            }
        }
    }
}