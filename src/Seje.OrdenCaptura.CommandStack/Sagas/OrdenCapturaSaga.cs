using MediatR;
using MementoFX.Persistence;
using Seje.OrdenCaptura.CommandStack.Commands;
using Seje.OrdenCaptura.SharedKernel.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Seje.OrdenCaptura.CommandStack.Sagas
{
    public class OrdenCapturaSaga : 
        IRequestHandler<RegistrarOrdenCapturaCommand, IResult<Models.OrdenCaptura>>,
        IRequestHandler<ModificarOrdenCapturaCommand, IResult<Models.OrdenCaptura>>
    {
        public IRepository Repository { get; }
        public OrdenCapturaSaga(IRepository repository)
        {
            Repository = repository;
        }

        public async Task<IResult<Models.OrdenCaptura>> Handle(RegistrarOrdenCapturaCommand request, CancellationToken cancellationToken)
        {
            var model = Models.OrdenCaptura.Factory.Create(
                request.OrdenCapturaId,
                request.OrganoJurisdiccionalId,
                request.OrdenCapturaCodigo,
                request.NumeroOrdenCaptura,
                request.Correlativo,
                request.OrdenCapturaEstadoId,
                request.OrdenCapturaEstadoDescripcion,
                request.ExpedienteId,
                request.NumeroExpediente,
                request.InstanciaId,
                request.InstanciaDescripcion,
                request.CorreoSecretario,
                request.CorreoJuez,
                request.CorreoEscribiente,
                request.FechaEmision,
                request.AlertaInternacional,
                request.DepartamentoId,
                request.DepartamentoDescripcion,
                request.MunicipioId,
                request.MunicipioDescripcion,
                request.UserName);
            await Repository.SaveAsync(model);
            return Result<Models.OrdenCaptura>.Ok(model);
        }

        public async Task<IResult<Models.OrdenCaptura>> Handle(ModificarOrdenCapturaCommand request, CancellationToken cancellationToken)
        {
            var model = Models.OrdenCaptura.Factory.Modify(
                request.OrdenCapturaId,
                request.OrganoJurisdiccionalId,
                request.OrdenCapturaCodigo,
                request.NumeroOrdenCaptura,
                request.Correlativo,
                request.OrdenCapturaEstadoId,
                request.OrdenCapturaEstadoDescripcion,
                request.ExpedienteId,
                request.NumeroExpediente,
                request.InstanciaId,
                request.InstanciaDescripcion,
                request.CorreoSecretario,
                request.CorreoJuez,
                request.CorreoEscribiente,
                request.FechaEmision,
                request.FechaEntrega,
                request.AlertaInternacional,
                request.DepartamentoId,
                request.DepartamentoDescripcion,
                request.MunicipioId,
                request.MunicipioDescripcion,
                request.Observaciones,
                request.UserName);
            await Repository.SaveAsync(model);
            return Result<Models.OrdenCaptura>.Ok(model);
        }
    }
}
