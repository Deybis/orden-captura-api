using System.Threading.Tasks;

namespace Seje.Services.Identity.Infrastructure
{
    public interface IAuthServerConnect
    {
        Task<string> RequestClientCredentialsTokenAsync();
    }
}
