using System.Security.Claims;

namespace Auth.Common.Contracts
{
    public interface ITokenService
    {
        string GenerateAccessToken(IEnumerable<Claim> claims);
        string GenerateRefreshToken(IEnumerable<Claim> claims);
    }
}
