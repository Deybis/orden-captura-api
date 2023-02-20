using Seje.OrdenCaptura.SharedKernel.Results;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Seje.OrdenCaptura.Api.Infrastructure.Interfaces
{
    public interface IFirma
    {
        Task<Result<List<Firma>>> List();
        Task<Result<Firma>> GetById(int firmaId);
        Task<Result<List<Firma>>> GetByFilter(FiltrosFirma filtros);
        Task<Result<Firma>> Create(Firma model, string userName);
        Task<Result<Firma>> Update(Firma model, string userName);
        Task<Result<Firma>> Delete(int firmaId, string userName);
    }
}
