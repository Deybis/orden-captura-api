using Seje.OrdenCaptura.SharedKernel.Results;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Seje.OrdenCaptura.Api.Infrastructure.Interfaces
{
    public interface IDocumento
    {
        Task<Result<List<Documento>>> List();
        Task<Result<Documento>> GetById(long documentoId);
        Task<Result<List<Documento>>> GetByFilter(FiltrosDocumento filtros);
        Task<Result<Documento>> Create(Documento documento, string userName);
        Task<Result<List<RegistrarDocumento>>> CreateMultiple(List<RegistrarDocumento> files, string userName);
        Task<Result<Documento>> Update(Documento model, string userName);
        Task<Result<Documento>> Delete(long id, string userName);
    }
}
