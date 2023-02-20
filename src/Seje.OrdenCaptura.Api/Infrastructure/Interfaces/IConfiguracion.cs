using Seje.OrdenCaptura.SharedKernel.Results;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Seje.OrdenCaptura.Api.Infrastructure.Interfaces
{
    public interface IConfiguracion
    {
        Task<Result<List<Configuracion>>> List();
        Task<Result<Configuracion>> GetById(int firmaId);
        Task<Result<List<Configuracion>>> GetByFilter(FiltrosConfiguracion filtros);
        Task<Result<Configuracion>> Create(Configuracion model, string userName);
        Task<Result<Configuracion>> Update(Configuracion model, string userName);
        Task<Result<Configuracion>> Delete(int firmaId, string userName);
    }
}
