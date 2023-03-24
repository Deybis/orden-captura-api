using Seje.OrdenCaptura.SharedKernel.Results;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Seje.OrdenCaptura.Api.Infrastructure.Interfaces
{
    public interface IOrdenCapturaEstado
    {
        Task<Result<List<OrdenCapturaEstado>>> List();
        Task<Result<OrdenCapturaEstado>> GetById(long id);
        Task<Result<List<OrdenCapturaEstado>>> GetByFilter(FiltrosOrdenCapturaEstado filtros);
        Task<Result<OrdenCapturaEstado>> Create(OrdenCapturaEstado model, string userName);
        Task<Result<OrdenCapturaEstado>> Update(OrdenCapturaEstado model, string userName);
        Task<Result<OrdenCapturaEstado>> Delete(long id, string userName);
    }
}
