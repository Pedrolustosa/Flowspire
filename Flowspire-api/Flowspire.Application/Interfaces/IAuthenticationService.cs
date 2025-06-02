using System.Threading.Tasks;

namespace Flowspire.Application.Interfaces
{
    public interface IAuthenticationService
    {
        Task<(string AccessToken, string RefreshToken)> LoginUserAsync(string email, string password);
        Task<(string AccessToken, string RefreshToken)> RefreshTokenAsync(string refreshToken);
    }
}
