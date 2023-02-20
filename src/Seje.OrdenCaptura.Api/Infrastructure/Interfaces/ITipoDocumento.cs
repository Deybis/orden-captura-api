using Seje.OrdenCaptura.SharedKernel.Results;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Seje.OrdenCaptura.Api.Infrastructure.Interfaces
{
    public interface ITipoDocumento
    {
        Task<Result<List<Models.TipoDocumento>>> List();
        Task<Result<Models.TipoDocumento>> GetById(int tipoDocumentoId);
        Task<Result<List<Models.TipoDocumento>>> GetByFilter(FiltrosTipoDocumento filtros);
        Task<Result<Models.TipoDocumento>> Create(Models.TipoDocumento model, string userName);
        Task<Result<Models.TipoDocumento>> Update(Models.TipoDocumento model, string userName);
        Task<Result<Models.TipoDocumento>> Delete(int id, string userName);
    }
}
