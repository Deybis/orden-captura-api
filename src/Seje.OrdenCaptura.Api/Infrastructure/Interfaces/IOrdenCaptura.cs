using Entities.Shared.Model;
using Entities.Shared.Paging.Generic;
using Seje.OrdenCaptura.SharedKernel.Results;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Seje.OrdenCaptura.Api.Infrastructure.Interfaces
{
    public interface IOrdenCaptura
    {
        Task<Result<List<OrdenCaptura>>> List();
        Task<PagedResult<OrdenCaptura>> List(FiltrosOrdenCaptura filtros,string userName);
        Task<Result<OrdenCaptura>> GetById(long ordenCapturaId);
        Task<Result<List<OrdenCaptura>>> GetByFilter(FiltrosOrdenCaptura filtros);
        Task<Result<OrdenCaptura>> Create(RegistrarOrdenCaptura model, string userName);
        Task<Result<OrdenCaptura>> Update(ActualizarOrdenCaptura model, string userName);
        Task<Result<List<Firma>>> RegistrarFirmas(string numeroOrdenCaptura,int numeroFirmas, string userName);
        Task<Result<OrdenCapturaParte>> RegistrarPartes(OrdenCapturaParte model, string userName);
        Task<Result<FirmaResponse>> Firmar(FirmaRequest request, string userName);
    }
}
