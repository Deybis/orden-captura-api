using AutoMapper;
using Seje.OrdenCaptura.CommandStack.Commands;
using Seje.OrdenCaptura.CommandStack.Events;

namespace Seje.OrdenCaptura.Api.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<QueryStack.OrdenCaptura, OrdenCaptura>().ReverseMap().ForAllMembers(x => x.AllowNull());
            CreateMap<QueryStack.OrdenCaptura, OrdenCapturaModificadaEvent>().ReverseMap().ForAllMembers(x => x.AllowNull());
            CreateMap<QueryStack.OrdenCaptura, OrdenCapturaRegistradaEvent>().ReverseMap().ForAllMembers(x => x.AllowNull());
            CreateMap<OrdenCaptura, RegistrarOrdenCapturaCommand>().ReverseMap().ForAllMembers(x => x.AllowNull());
            CreateMap<OrdenCaptura, ModificarOrdenCapturaCommand>().ReverseMap().ForAllMembers(x => x.AllowNull());
            CreateMap<CommandStack.Models.OrdenCaptura, OrdenCaptura>().ReverseMap().ForAllMembers(x => x.AllowNull());
            CreateMap<OrdenCapturaRegistradaEvent, OrdenCaptura>().ReverseMap().ForAllMembers(x => x.AllowNull());
            CreateMap<OrdenCapturaModificadaEvent, OrdenCaptura>().ReverseMap().ForAllMembers(x => x.AllowNull());
            CreateMap<OrdenCaptura, ActualizarOrdenCaptura>().ReverseMap().ForAllMembers(x => x.AllowNull());

            CreateMap<QueryStack.Expediente, Expediente>().ReverseMap().ForAllMembers(x => x.AllowNull());
            CreateMap<QueryStack.Expediente, RegistrarExpediente>().ReverseMap().ForAllMembers(x => x.AllowNull());
            CreateMap<QueryStack.Expediente, ActualizarExpediente>().ReverseMap().ForAllMembers(x => x.AllowNull());

            CreateMap<QueryStack.Delito, Delito>().ReverseMap().ForAllMembers(x => x.AllowNull());
            CreateMap<QueryStack.Delito, RegistrarDelito>().ReverseMap().ForAllMembers(x => x.AllowNull());
            CreateMap<QueryStack.Delito, ActualizarDelito>().ReverseMap().ForAllMembers(x => x.AllowNull());
            CreateMap<Delito, ActualizarDelito>().ReverseMap().ForAllMembers(x => x.AllowNull());
            CreateMap<Delito, RegistrarDelito>().ReverseMap().ForAllMembers(x => x.AllowNull());

            CreateMap<QueryStack.Parte, Parte>().ReverseMap().ForAllMembers(x => x.AllowNull());
            CreateMap<QueryStack.Parte, RegistrarParte>().ReverseMap().ForAllMembers(x => x.AllowNull());
            CreateMap<QueryStack.Parte, ActualizarParte>().ReverseMap().ForAllMembers(x => x.AllowNull());
            CreateMap<Parte, ActualizarParte>().ReverseMap().ForAllMembers(x => x.AllowNull());
            CreateMap<Parte, RegistrarParte>().ReverseMap().ForAllMembers(x => x.AllowNull());
            CreateMap<QueryStack.OrdenCapturaParte, OrdenCapturaParte>().ReverseMap().ForAllMembers(x => x.AllowNull());

            CreateMap<QueryStack.OrdenCapturaEstado, OrdenCapturaEstado>().ReverseMap().ForAllMembers(x => x.AllowNull());
            CreateMap<QueryStack.Firma, Firma>().ReverseMap().ForAllMembers(x => x.AllowNull());
            CreateMap<QueryStack.TipoFirma, TipoFirma>().ReverseMap().ForAllMembers(x => x.AllowNull());
            CreateMap<QueryStack.Configuracion, Configuracion>().ReverseMap().ForAllMembers(x => x.AllowNull());
            CreateMap<QueryStack.Documento, Documento>().ReverseMap().ForAllMembers(x => x.AllowNull());
            CreateMap<QueryStack.TipoDocumento, Models.TipoDocumento>().ReverseMap().ForAllMembers(x => x.AllowNull());
            CreateMap<QueryStack.Firmante, Firmante>().ReverseMap().ForAllMembers(x => x.AllowNull());
        }
    }
}
