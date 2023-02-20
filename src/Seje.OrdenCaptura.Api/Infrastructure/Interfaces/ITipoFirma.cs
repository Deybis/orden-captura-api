using Seje.OrdenCaptura.SharedKernel.Results;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Seje.OrdenCaptura.Api.Infrastructure.Interfaces
{
    public interface ITipoFirma
    {
        Task<Result<List<TipoFirma>>> List();
        Task<Result<TipoFirma>> GetById(int tipoFirmaId);
        Task<Result<List<TipoFirma>>> GetByFilter(FiltrosTipoFirma filtros);
        Task<Result<TipoFirma>> Create(TipoFirma model, string userName);
        Task<Result<TipoFirma>> Update(TipoFirma model, string userName);
        Task<Result<TipoFirma>> Delete(int id, string userName);
    }
}
