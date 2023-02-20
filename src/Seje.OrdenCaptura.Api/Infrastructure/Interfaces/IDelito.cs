using Seje.OrdenCaptura.SharedKernel.Results;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Seje.OrdenCaptura.Api.Infrastructure.Interfaces
{
    public interface IDelito
    {
        Task<Result<List<Delito>>> List();
        Task<Result<Delito>> GetById(long delitoId);
        Task<Result<List<Delito>>> GetByFilter(FiltrosDelito filtros);
        Task<Result<Delito>> Create(RegistrarDelito model, string userName);
        Task<Result<Delito>> Update(ActualizarDelito model, string userName);
        Task<Result<Delito>> Delete(long id, string userName);
    }
}
