using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Portfolio.DataBridgez.Application.Interfaces;
using Portfolio.Databridgez.Application.Mappings;

namespace Portfolio.Databridgez.Infrastructure.Identity;

public sealed class IdentityStore :
    IUserPasswordStore<AppUser>,
    IUserSecurityStampStore<AppUser>,
    IUserEmailStore<AppUser>,
    IQueryableUserStore<AppUser>,
    IProtectedUserStore<AppUser>,
    IUserLockoutStore<AppUser>
{
    private bool _disposed;
    private readonly IRepository<AppUser> _users;

    public IQueryable<AppUser> Users { get; }

    public IdentityStore(IRepository<AppUser> users)
    {
        _users = users;
        Users = _users.FindAll(true);
    }

    #region IUserStore

    public Task<string> GetUserIdAsync(AppUser user, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        ThrowIfDisposed();
        ArgumentNullException.ThrowIfNull(user);
        return Task.FromResult(Convert.ToString(user.Id));
    }

    public async Task<string?> GetUserNameAsync(AppUser user, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        ThrowIfDisposed();
        
        var userEntity = await _users
            .FindByCondition(u => u.Id == user.Id, false, cancellationToken)
            .SingleAsync(cancellationToken);

        userEntity.MapUserToExistingUser(user);
        
        return userEntity.UserName;
    }

    public async Task SetUserNameAsync(AppUser user, string? userName, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        ArgumentException.ThrowIfNullOrEmpty(userName);
        ThrowIfDisposed();
        
        var userEntity = await _users
            .FindByCondition(u => u.Id == user.Id, true, cancellationToken)
            .SingleOrDefaultAsync(cancellationToken);
        
        if (userEntity == null) return;
        userEntity.UserName = userName;
        
        _users.Update(userEntity, cancellationToken);
        await _users.SaveChangesAsync(cancellationToken);

        userEntity.MapUserToExistingUser(user);
    }

    public async Task<string?> GetNormalizedUserNameAsync(AppUser user, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        ThrowIfDisposed();
        
        var userEntity = await _users
            .FindByCondition(u => u.Id == user.Id, false, cancellationToken)
            .SingleAsync(cancellationToken);
        
        userEntity.MapUserToExistingUser(user);
        
        return userEntity.NormalizedUserName;
    }

    public async Task SetNormalizedUserNameAsync(AppUser user, string? normalizedUserName, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        ArgumentException.ThrowIfNullOrEmpty(normalizedUserName);
        ThrowIfDisposed();
        
        var userEntity = await _users
            .FindByCondition(u => u.Id == user.Id, true, cancellationToken)
            .SingleOrDefaultAsync(cancellationToken);

        if (userEntity == null) return;
        userEntity.NormalizedUserName = normalizedUserName;
        
        _users.Update(userEntity, cancellationToken);
        await _users.SaveChangesAsync(cancellationToken);
        
        userEntity.MapUserToExistingUser(user);
    }

    public async Task<IdentityResult> CreateAsync(AppUser user, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        ThrowIfDisposed();
        
        await _users.CreateAsync(user, cancellationToken);
        await _users.SaveChangesAsync(cancellationToken);
        
        return IdentityResult.Success;
    }

    public async Task<IdentityResult> UpdateAsync(AppUser user, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        ThrowIfDisposed();

        var userEntity = await _users.GetByIdAsync(user.Id, true, cancellationToken);
        if (userEntity == null)
        {
            return IdentityResult.Failed();
        }

        _users.Update(userEntity, cancellationToken);
        await _users.SaveChangesAsync(cancellationToken);
        
        user.MapUserToExistingUser(userEntity);

        return IdentityResult.Success;
    }

    public async Task<IdentityResult> DeleteAsync(AppUser user, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        ThrowIfDisposed();
        
        var userEntity = await _users
            .FindByCondition(u => u.Id == user.Id, true, cancellationToken)
            .SingleOrDefaultAsync(cancellationToken);
        
        if (userEntity == null)
        {
            return IdentityResult.Failed();
        }
        
        _users.Delete(userEntity, cancellationToken);
        await _users.SaveChangesAsync(cancellationToken);

        userEntity.MapUserToExistingUser(user);
        
        return IdentityResult.Success;
    }

    public async Task<AppUser?> FindByIdAsync(string id, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        ThrowIfDisposed();
        
        if (long.TryParse(id, out var userId))
        {
            return null;
        }

        var userEntity = await _users
            .FindByCondition(u => u.Id == userId, false, cancellationToken)
            .SingleOrDefaultAsync(cancellationToken);
        
        return userEntity;
    }

    public async Task<AppUser?> FindByIdAsync(long userId, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        var userEntity = await _users
            .FindByCondition(u => u.Id == userId, false, cancellationToken)
            .SingleOrDefaultAsync(cancellationToken);
        return userEntity;
    }

    public async Task<AppUser?> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        ThrowIfDisposed();
        
        var userEntity = await _users
            .FindByCondition(u => u.NormalizedUserName == normalizedUserName, false,
                cancellationToken)
            .SingleOrDefaultAsync(cancellationToken);
        
        return userEntity;
    }

    #endregion

    #region IUserPasswordStore

    public async Task SetPasswordHashAsync(AppUser user, string? passwordHash, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        ArgumentException.ThrowIfNullOrEmpty(passwordHash);
        ThrowIfDisposed();
        
        var userEntity = await _users
            .FindByCondition(u => u.Id == user.Id, true, cancellationToken)
            .SingleOrDefaultAsync(cancellationToken);

        if (userEntity == null) return;
        userEntity.PasswordHash = passwordHash;
        
        _users.Update(userEntity, cancellationToken);
        await _users.SaveChangesAsync(cancellationToken);
        
        userEntity.MapUserToExistingUser(user);
    }

    public async Task<string?> GetPasswordHashAsync(AppUser user, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        ThrowIfDisposed();
        
        var userEntity = await _users
            .FindByCondition(u => u.Id == user.Id, false, cancellationToken)
            .SingleAsync(cancellationToken);

        userEntity.MapUserToExistingUser(user);
        
        return userEntity.PasswordHash;
    }

    public async Task<bool> HasPasswordAsync(AppUser user, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        ThrowIfDisposed();
        
        var userEntity = await _users
            .FindByCondition(u => u.Id == user.Id, false, cancellationToken)
            .SingleAsync(cancellationToken);

        userEntity.MapUserToExistingUser(user);
        
        return !string.IsNullOrEmpty(userEntity.PasswordHash);
    }

    #endregion

    #region IUserSecurityStampStore

    public async Task SetSecurityStampAsync(AppUser user, string stamp, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        ThrowIfDisposed();
        
        var userEntity = await _users
            .FindByCondition(u => u.Id == user.Id, true, cancellationToken)
            .SingleOrDefaultAsync(cancellationToken);
        
        if (userEntity == null) return;
        userEntity.SecurityStamp = stamp;
        
        _users.Update(userEntity, cancellationToken);
        await _users.SaveChangesAsync(cancellationToken);
        
        userEntity.MapUserToExistingUser(user);
    }

    public async Task<string?> GetSecurityStampAsync(AppUser user, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        ThrowIfDisposed();
        
        var userEntity = await _users
            .FindByCondition(u => u.Id == user.Id, false, cancellationToken)
            .SingleAsync(cancellationToken);

        userEntity.MapUserToExistingUser(user);
        
        return userEntity.SecurityStamp;
    }

    #endregion

    #region IDisposable

    public void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                _users.Dispose();
            }

            _disposed = true;
        }
    }

    public void Dispose()
    {
    }
    
    private void ThrowIfDisposed()
    {
        if (_disposed)
        {
            throw new ObjectDisposedException(GetType().Name);
        }
    }

    #endregion

    #region IUserEmailStore

    public async Task SetEmailAsync(AppUser user, string? email, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        ArgumentException.ThrowIfNullOrEmpty(email);
        ThrowIfDisposed();
        
        var userEntity = await _users
            .FindByCondition(u => u.Id == user.Id, true, cancellationToken)
            .SingleOrDefaultAsync(cancellationToken);
        
        if (userEntity == null)
        {
            return;
        }

        userEntity.Email = email;
        await _users.SaveChangesAsync(cancellationToken);
        
        userEntity.MapUserToExistingUser(user);
    }

    public async Task<string?> GetEmailAsync(AppUser user, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        ThrowIfDisposed();
        
        var userEntity = await _users
            .FindByCondition(u => u.Id == user.Id, false, cancellationToken)
            .SingleAsync(cancellationToken);

        userEntity.MapUserToExistingUser(user);
        
        return userEntity.Email;
    }

    public async Task<bool> GetEmailConfirmedAsync(AppUser user, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        ThrowIfDisposed();
        
        var userEntity = await _users
            .FindByCondition(u => u.Id == user.Id, false, cancellationToken)
            .SingleAsync(cancellationToken);

        userEntity.MapUserToExistingUser(user);
        
        return userEntity.EmailConfirmed;
    }

    public async Task SetEmailConfirmedAsync(AppUser user, bool confirmed, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        ThrowIfDisposed();
        
        var userEntity = await _users
            .FindByCondition(u => u.Id == user.Id, true, cancellationToken)
            .SingleOrDefaultAsync(cancellationToken);

        if (userEntity == null)
        {
            return;
        }

        userEntity.EmailConfirmed = true;
        await _users.SaveChangesAsync(cancellationToken);
        
        userEntity.MapUserToExistingUser(user);
    }

    public async Task<AppUser?> FindByEmailAsync(string normalizedEmail, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        ArgumentException.ThrowIfNullOrEmpty(normalizedEmail);
        ThrowIfDisposed();
        
        var userEntity = await _users
            .FindByCondition(u => u.NormalizedEmail == normalizedEmail, false, cancellationToken)
            .SingleOrDefaultAsync(cancellationToken);

        return userEntity;
    }

    public async Task<string?> GetNormalizedEmailAsync(AppUser user, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        ArgumentNullException.ThrowIfNull(user);
        ThrowIfDisposed();
        
        var userEntity = await _users
            .FindByCondition(u => u.Id == user.Id, false, cancellationToken)
            .SingleAsync(cancellationToken);

        userEntity.MapUserToExistingUser(user);
        
        return userEntity.NormalizedEmail;
    }

    public async Task SetNormalizedEmailAsync(AppUser user, string? normalizedEmail, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        ArgumentNullException.ThrowIfNull(user);
        ArgumentException.ThrowIfNullOrEmpty(normalizedEmail);
        ThrowIfDisposed();
        
        var userEntity = await _users
            .FindByCondition(u => u.Id == user.Id, true, cancellationToken)
            .SingleOrDefaultAsync(cancellationToken);

        if (userEntity == null)
        {
            return;
        }

        userEntity.NormalizedEmail = normalizedEmail;
        await _users.SaveChangesAsync(cancellationToken);

        userEntity.MapUserToExistingUser(user);
    }

    #endregion

    #region IUserLockoutStore

    public async Task<DateTimeOffset?> GetLockoutEndDateAsync(AppUser user, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        ArgumentNullException.ThrowIfNull(user);
        ThrowIfDisposed();
        
        var userEntity = await _users
            .FindByCondition(u => u.Id == user.Id, false, cancellationToken)
            .SingleAsync(cancellationToken);

        userEntity.MapUserToExistingUser(user);
        
        return userEntity.LockoutEnd;
    }

    public async Task SetLockoutEndDateAsync(AppUser user, DateTimeOffset? lockoutEnd, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        ArgumentNullException.ThrowIfNull(user);
        ThrowIfDisposed();
        
        var userEntity = await _users
            .FindByCondition(u => u.Id == user.Id, true, cancellationToken)
            .SingleOrDefaultAsync(cancellationToken);

        if (userEntity == null)
        {
            return;
        }

        userEntity.LockoutEnd = lockoutEnd;
        await _users.SaveChangesAsync(cancellationToken);
        
        userEntity.MapUserToExistingUser(user);
    }

    public async Task<int> IncrementAccessFailedCountAsync(AppUser user, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        ArgumentNullException.ThrowIfNull(user);
        ThrowIfDisposed();
        
        var userEntity = await _users
            .FindByCondition(u => u.Id == user.Id, true, cancellationToken)
            .SingleOrDefaultAsync(cancellationToken);

        if (userEntity == null)
        {
            return user.AccessFailedCount;
        }
        
        userEntity.AccessFailedCount++;
        await _users.SaveChangesAsync(cancellationToken);
        
        userEntity.MapUserToExistingUser(user);
        return user.AccessFailedCount;
    }

    public async Task ResetAccessFailedCountAsync(AppUser user, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        ArgumentNullException.ThrowIfNull(user);
        ThrowIfDisposed();

        var userEntity = await _users
            .FindByCondition(u => u.Id == user.Id, true, cancellationToken)
            .SingleOrDefaultAsync(cancellationToken);

        if (userEntity == null)
        {
            return;
        }
        
        userEntity.AccessFailedCount = 0;
        await _users.SaveChangesAsync(cancellationToken);
        
        userEntity.MapUserToExistingUser(user);
    }

    public async Task<int> GetAccessFailedCountAsync(AppUser user, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        ArgumentNullException.ThrowIfNull(user);
        ThrowIfDisposed();
        
        var userEntity = await _users
            .FindByCondition(u => u.Id == user.Id, false, cancellationToken)
            .SingleAsync(cancellationToken);

        userEntity.MapUserToExistingUser(user);
        
        return userEntity.AccessFailedCount;
    }

    public async Task<bool> GetLockoutEnabledAsync(AppUser user, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        ArgumentNullException.ThrowIfNull(user);
        ThrowIfDisposed();
        
        var userEntity = await _users
            .FindByCondition(u => u.Id == user.Id, false, cancellationToken)
            .SingleAsync(cancellationToken);
        
        userEntity.MapUserToExistingUser(user);
        return userEntity.LockoutEnabled;
    }

    public async Task SetLockoutEnabledAsync(AppUser user, bool enabled, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        ThrowIfDisposed();
        
        var userEntity = await _users
            .FindByCondition(u => u.Id == user.Id, true, cancellationToken)
            .SingleOrDefaultAsync(cancellationToken);

        if (userEntity == null)
        {
            return;
        }

        userEntity.LockoutEnabled = enabled;
        await _users.SaveChangesAsync(cancellationToken);
        
        userEntity.MapUserToExistingUser(user);
    }

    #endregion
}