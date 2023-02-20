using Seje.OrdenCaptura.SharedKernel.Results;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Seje.OrdenCaptura.Api.Infrastructure.Interfaces
{
    public interface IParte
    {
        Task<Result<List<Parte>>> List();
        Task<Result<Parte>> GetById(long parteId);
        Task<Result<List<Parte>>> GetByFilter(FiltrosParte filtros);
        Task<Result<Parte>> Create(RegistrarParte model, string userName);
        Task<Result<Parte>> Update(ActualizarParte model, string userName);
        Task<Result<Parte>> Delete(int parteId, string userName);
    }
}
