using System.Threading.Tasks;

namespace Seje.OrdenCaptura.Api
{
    public interface IAuthServerConnect
    {
        Task<string> RequestClientCredentialsTokenAsync();
    }
}
