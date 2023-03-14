using Entities.Shared.Paging.Generic;
using Seje.OrdenCaptura.SharedKernel.Results;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Seje.OrdenCaptura.Api.Infrastructure.Interfaces
{
    public interface IExpediente
    {
        Task<Result<List<Expediente>>> List();
        Task<PagedResult<Expediente>> List(FiltrosExpediente filtros);
        Task<Result<Expediente>> GetById(long expedienteId);
        Task<Result<List<Expediente>>> GetByFilter(FiltrosExpediente filtros);
        Task<Result<RegistrarExpediente>> Create(RegistrarExpediente model, string userName);
        Task<Result<ActualizarExpediente>> Update(ActualizarExpediente model, string userName);
    }
}
