using Core.Entities.Identity;

namespace core.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(AppUser appUser);
    }
}