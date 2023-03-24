using FluentValidation;

namespace Seje.OrdenCaptura.Api.Validator
{
    public class RegistrarOrdenCapturaValidator : AbstractValidator<RegistrarOrdenCaptura>
    {
        public RegistrarOrdenCapturaValidator()
        {
            RuleFor(x => x.InstanciaId).GreaterThan(0).WithMessage("Por favor ingrese la instancia.");
            RuleFor(x => x.OrganoJurisdiccionalId).GreaterThan(0).WithMessage("Por favor ingrese el organo jurisdiccional.");
        }
    }

    public class ActualizarOrdenCapturaValidator : AbstractValidator<ActualizarOrdenCaptura>
    {
        public ActualizarOrdenCapturaValidator()
        {
            RuleFor(x => x.OrdenCapturaId).GreaterThan(0).WithMessage("El id de la orden de captura es requerido.");
            RuleFor(x => x.InstanciaId).GreaterThan(0).WithMessage("Por favor ingrese la instancia.");
            RuleFor(x => x.OrganoJurisdiccionalId).GreaterThan(0).WithMessage("Por favor ingrese el organo jurisdiccional.");
        }
    }

    public class RegistrarExpedienteValidator : AbstractValidator<RegistrarExpediente>
    {
        public RegistrarExpedienteValidator()
        {
            RuleFor(x => x.NumeroExpediente).NotNull().NotEmpty().WithMessage("Por favor ingrese el número de expediente.");
        }
    }

    public class ActualizarExpedienteValidator : AbstractValidator<ActualizarExpediente>
    {
        public ActualizarExpedienteValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0).WithMessage("El id expediente es requerido.");
            RuleFor(x => x.NumeroExpediente).NotNull().NotEmpty().WithMessage("Por favor ingrese el número de expediente.");
        }
    }

    public class RegistrarDelitoValidator : AbstractValidator<RegistrarDelito>
    {
        public RegistrarDelitoValidator()
        {
            RuleFor(x => x.Nombre).NotNull().NotEmpty().WithMessage("Por favor ingrese el nombre del delito.");
        }
    }

    public class ActualizarDelitoValidator : AbstractValidator<ActualizarDelito>
    {
        public ActualizarDelitoValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0).WithMessage("El id delito es requerido.");
            RuleFor(x => x.Nombre).NotNull().NotEmpty().WithMessage("Por favor ingrese el nombre del delito.");
        }
    }

    public class RegistrarParteValidator : AbstractValidator<RegistrarParte>
    {
        public RegistrarParteValidator()
        {
            RuleFor(x => x.Nombre).NotNull().NotEmpty().WithMessage("Por favor ingrese el nombre de la parte.");
        }
    }

    public class ActualizarParteValidator : AbstractValidator<ActualizarParte>
    {
        public ActualizarParteValidator()
        {
            RuleFor(x => x.ParteId).GreaterThan(0).WithMessage("El id parte es requerido.");
            RuleFor(x => x.Nombre).NotNull().NotEmpty().WithMessage("Por favor ingrese el nombre del delito.");
        }
    }

    public class RegistrarOrdenCapturaEstadoValidator : AbstractValidator<OrdenCapturaEstado>
    {
        public RegistrarOrdenCapturaEstadoValidator()
        {
            RuleFor(x => x.Descripcion).NotNull().NotEmpty().WithMessage("Por favor ingrese el nombre del estado.");
        }
    }

    public class RegistrarOrdenCapturaParteValidator : AbstractValidator<OrdenCapturaParte>
    {
        public RegistrarOrdenCapturaParteValidator()
        {
            RuleFor(x => x.ParteId).NotNull().NotEmpty().WithMessage("Por favor ingrese el id de la parte.");
            RuleFor(x => x.ParteDescripcion).NotNull().NotEmpty().WithMessage("Por favor ingrese la descipción de la parte.");
            RuleFor(x => x.NumeroOrdenCaptura).NotNull().NotEmpty().WithMessage("Por favor ingrese el numero de orden de captura.");
        }
    }

    public class ActualizarOrdenCapturaParteValidator : AbstractValidator<OrdenCapturaParte>
    {
        public ActualizarOrdenCapturaParteValidator()
        {
            RuleFor(x => x.Id).NotNull().NotEmpty().WithMessage("Por favor ingrese el id.");
            RuleFor(x => x.ParteId).NotNull().NotEmpty().WithMessage("Por favor ingrese el id de la parte.");
        }
    }

    public class ActualizarOrdenCapturaEstadoValidator : AbstractValidator<OrdenCapturaEstado>
    {
        public ActualizarOrdenCapturaEstadoValidator()
        {
            RuleFor(x => x.OrdenCapturaEstadoId).GreaterThan(0).WithMessage("El id del estado es requerido.");
            RuleFor(x => x.Descripcion).NotNull().NotEmpty().WithMessage("Por favor ingrese el nombre del estado.");
        }
    }

    public class RegistrarFirmaValidator : AbstractValidator<Firma>
    {
        public RegistrarFirmaValidator()
        {
            RuleFor(x => x.CorreoFirmante).NotNull().NotEmpty().WithMessage("Por favor ingrese el correo del firmante.");
            RuleFor(x => x.NumeroOrdenCaptura).NotNull().NotEmpty().WithMessage("Por favor ingrese el número de orden de captura.");
        }
    }

    public class ActualizarFirmaValidator : AbstractValidator<Firma>
    {
        public ActualizarFirmaValidator()
        {
            RuleFor(x => x.FirmaId).GreaterThan(0).WithMessage("El Id de la firma es requerido.");
            RuleFor(x => x.CorreoFirmante).NotNull().NotEmpty().WithMessage("Por favor ingrese el correo del firmante.");
            RuleFor(x => x.NumeroOrdenCaptura).NotNull().NotEmpty().WithMessage("Por favor ingrese el número de orden de captura.");
        }
    }

    public class RegistrarConfiguracionValidator : AbstractValidator<Configuracion>
    {
        public RegistrarConfiguracionValidator()
        {
            RuleFor(x => x.Nombre).NotNull().NotEmpty().WithMessage("Por favor ingrese el nombre de la configuración.");
            RuleFor(x => x.Valor).NotNull().NotEmpty().WithMessage("Por favor ingrese el valor de la configuración.");
        }
    }

    public class ActualizarConfiguracionValidator : AbstractValidator<Configuracion>
    {
        public ActualizarConfiguracionValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0).WithMessage("El Id de la configuración es requerido.");
            RuleFor(x => x.Nombre).NotNull().NotEmpty().WithMessage("Por favor ingrese el nombre de la configuración.");
            RuleFor(x => x.Valor).NotNull().NotEmpty().WithMessage("Por favor ingrese el valor de la configuración");
        }
    }

    public class RegistrarTipoFirmaValidator : AbstractValidator<TipoFirma>
    {
        public RegistrarTipoFirmaValidator()
        {
            RuleFor(x => x.Nombre).NotNull().NotEmpty().WithMessage("Por favor ingrese el nombre del tipo de firma.");
        }
    }

    public class ActualizarTipoFirmaValidator : AbstractValidator<TipoFirma>
    {
        public ActualizarTipoFirmaValidator()
        {
            RuleFor(x => x.TipoFirmaId).GreaterThan(0).WithMessage("El Id del tipo de firma es requerido.");
            RuleFor(x => x.Nombre).NotNull().NotEmpty().WithMessage("Por favor ingrese el nombre del tipo de firma.");
        }
    }

    public class RegistrarDocumentoValidator : AbstractValidator<Documento>
    {
        public RegistrarDocumentoValidator()
        {
            RuleFor(x => x.NumeroOrdenCaptura).NotNull().NotEmpty().WithMessage("Por favor ingrese el numero de la orden de captura.");
            RuleFor(x => x.TipoDocumentoId).GreaterThan(0).WithMessage("Por favor ingrese el tipo de documento.");
            RuleFor(x => x.Nombre).NotNull().NotEmpty().WithMessage("Por favor ingrese el nombre del documento.");
            RuleFor(x => x.Codigo).NotNull().NotEmpty().WithMessage("Por favor ingrese el código del documento.");
        }
    }

    public class ActualizarDocumentoValidator : AbstractValidator<Documento>
    {
        public ActualizarDocumentoValidator()
        {
            RuleFor(x => x.DocumentoId).GreaterThan(0).WithMessage("El id del documento es requerido.");
            RuleFor(x => x.NumeroOrdenCaptura).NotNull().NotEmpty().WithMessage("Por favor ingrese el numero de la orden de captura.");
            RuleFor(x => x.TipoDocumentoId).GreaterThan(0).WithMessage("Por favor ingrese el tipo de documento.");
            RuleFor(x => x.Nombre).NotNull().NotEmpty().WithMessage("Por favor ingrese el nombre del documento.");
            RuleFor(x => x.Codigo).NotNull().NotEmpty().WithMessage("Por favor ingrese el código del documento.");
        }
    }

    public class RegistrarTipoDocumentoValidator : AbstractValidator<Models.TipoDocumento>
    {
        public RegistrarTipoDocumentoValidator()
        {
            RuleFor(x => x.Nombre).NotNull().NotEmpty().WithMessage("Por favor ingrese el nombre del tipo de documento.");
        }
    }

    public class ActualizarTipoDocumentoValidator : AbstractValidator<Models.TipoDocumento>
    {
        public ActualizarTipoDocumentoValidator()
        {
            RuleFor(x => x.TipoDocumentoId).GreaterThan(0).WithMessage("El Id del tipo de documento es requerido.");
            RuleFor(x => x.Nombre).NotNull().NotEmpty().WithMessage("Por favor ingrese el nombre del tipo de documento.");
        }
    }

    public class RegistrarFirmanteValidator : AbstractValidator<Firmante>
    {
        public RegistrarFirmanteValidator()
        {
            RuleFor(x => x.Identificador).NotNull().NotEmpty().WithMessage("Por favor ingrese el identificador del firmante.");
        }
    }

    public class ActualizarFirmanteValidator : AbstractValidator<Firmante>
    {
        public ActualizarFirmanteValidator()
        {
            RuleFor(x => x.FirmanteId).GreaterThan(0).WithMessage("El Id del firmante es requerido.");
            RuleFor(x => x.Identificador).NotNull().NotEmpty().WithMessage("Por favor ingrese el identificador del firmante.");
        }
    }
}
