using Portfolio.DataBridgez.Domain.Entities;

namespace Portfolio.DataBridgez.Application.Interfaces;

public interface IAccessTokenProvider
{
    Task<string> GenerateTokenAsync(AppUser user);
}