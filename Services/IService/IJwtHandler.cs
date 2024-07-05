using Booking_API.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Booking_API.Services.IService
{
    public interface IJwtHandler
    {
        SigningCredentials GetSigningCredentials();
        List<Claim> GetClaims(ApplicationUser user);
        JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims);
    }
}
