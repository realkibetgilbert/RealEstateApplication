using Access.API.Domain.Entities;

namespace Access.API.Domain.Interfaces
{
    public interface ITokenRepository
    {
        string CreateJwtToken(AuthUser user, List<string> roles);
    }
}
