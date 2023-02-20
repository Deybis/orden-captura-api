using Entities.Shared.Model;
using Seje.OrdenCaptura.SharedKernel.Results;
using System.Threading.Tasks;

namespace Seje.Firma.Client
{
    public interface IFirmaDigitalClient
    {
        Task<Result<FirmaResponse>> Firmar(FirmaRequest firmaRequest);
    }
}
