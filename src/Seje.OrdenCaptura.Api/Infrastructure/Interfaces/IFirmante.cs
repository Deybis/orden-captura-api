using Seje.OrdenCaptura.SharedKernel.Results;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Seje.OrdenCaptura.Api.Infrastructure.Interfaces
{
    public interface IFirmante
    {
        Task<Result<List<Firmante>>> List();
        Task<Result<Firmante>> GetById(int firmanteId);
        Task<Result<List<Firmante>>> GetByFilter(FiltrosFirmante filtros);
        Task<Result<Firmante>> Create(Firmante model, string userName);
        Task<Result<Firmante>> Update(Firmante model, string userName);
        Task<Result<Firmante>> Delete(int firmanteId, string userName);
    }
}
