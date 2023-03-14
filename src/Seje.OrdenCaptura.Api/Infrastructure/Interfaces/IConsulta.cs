using Seje.OrdenCaptura.SharedKernel.Results;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Seje.OrdenCaptura.Api.Infrastructure.Interfaces
{
    public interface IConsulta
    {
        Task<Result<Seje.Expediente.Client.Models.Expediente>> GetExpediente(string numeroExpediente);
        Task<Result<Models.Estadistica>> GetEstadisticas(FiltrosEstadistica filtros);
    }
}
