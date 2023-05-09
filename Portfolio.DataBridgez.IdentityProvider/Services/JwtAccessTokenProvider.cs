using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using IdentityModel;
using Microsoft.IdentityModel.Tokens;
using Portfolio.Databridgez.Domain.Options.Jwt;

namespace Portfolio.DataBridgez.IdentityProvider.Services;

public sealed class JwtAccessTokenProvider : IAccessTokenProvider
{
    private readonly IConfiguration _config;
    
    public JwtAccessTokenProvider(IConfiguration config)
    {
        _config = config;
    }
    
    public Task<string> GenerateTokenAsync(AppUser user)
    {
        // Arrange configuration properties for the jwt token
        var jwtOptions = new JwtOptions();
        _config.GetSection(nameof(JwtOptions)).Bind(jwtOptions);
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Secret));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512);
        
        // Claims
        var claims = new[]
        {
            new Claim(JwtClaimTypes.Subject, user.Id.ToString()),
            new Claim(JwtClaimTypes.Email, user.Email)
        };

        // Configure the access jwt token
        var securityToken = new JwtSecurityToken(jwtOptions.ValidIssuer,
            jwtOptions.ValidAudience,
            claims,
            expires: DateTime.UtcNow.AddHours(15),
            signingCredentials: credentials);

        var accessToken = new JwtSecurityTokenHandler().WriteToken(securityToken);
        return Task.FromResult(accessToken);
    }
}