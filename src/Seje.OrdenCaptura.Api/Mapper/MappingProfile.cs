using AutoMapper;
using Seje.OrdenCaptura.CommandStack.Commands;
using Seje.OrdenCaptura.CommandStack.Events;

namespace Seje.OrdenCaptura.Api.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<QueryStack.OrdenCaptura, OrdenCaptura>().ReverseMap();
            CreateMap<QueryStack.OrdenCaptura, OrdenCapturaModificadaEvent>().ReverseMap();
            CreateMap<QueryStack.OrdenCaptura, OrdenCapturaRegistradaEvent>().ReverseMap();
            CreateMap<OrdenCaptura, RegistrarOrdenCapturaCommand>().ReverseMap();
            CreateMap<OrdenCaptura, ModificarOrdenCapturaCommand>().ReverseMap();
            CreateMap<CommandStack.Models.OrdenCaptura, OrdenCaptura>().ReverseMap();
            CreateMap<OrdenCapturaRegistradaEvent, OrdenCaptura>().ReverseMap();
            CreateMap<OrdenCapturaModificadaEvent, OrdenCaptura>().ReverseMap();
            CreateMap<OrdenCaptura, ActualizarOrdenCaptura>().ReverseMap();

            CreateMap<QueryStack.Expediente, Expediente>().ReverseMap();
            CreateMap<QueryStack.Expediente, RegistrarExpediente>().ReverseMap();
            CreateMap<QueryStack.Expediente, ActualizarExpediente>().ReverseMap();

            CreateMap<QueryStack.Delito, Delito>().ReverseMap();
            CreateMap<QueryStack.Delito, RegistrarDelito>().ReverseMap();
            CreateMap<QueryStack.Delito, ActualizarDelito>().ReverseMap();
            CreateMap<Delito, ActualizarDelito>().ReverseMap();
            CreateMap<Delito, RegistrarDelito>().ReverseMap();

            CreateMap<QueryStack.Parte, Parte>().ReverseMap();
            CreateMap<QueryStack.Parte, RegistrarParte>().ReverseMap();
            CreateMap<QueryStack.Parte, ActualizarParte>().ReverseMap();
            CreateMap<Parte, ActualizarParte>().ReverseMap();
            CreateMap<Parte, RegistrarParte>().ReverseMap();

            CreateMap<QueryStack.OrdenCapturaEstado, OrdenCapturaEstado>().ReverseMap();
            CreateMap<QueryStack.Firma, Firma>().ReverseMap();
            CreateMap<QueryStack.TipoFirma, TipoFirma>().ReverseMap();
            CreateMap<QueryStack.Configuracion, Configuracion>().ReverseMap();
            CreateMap<QueryStack.Documento, Documento>().ReverseMap();
            CreateMap<QueryStack.TipoDocumento, Models.TipoDocumento>().ReverseMap();
            CreateMap<QueryStack.Firmante, Firmante>().ReverseMap();
        }
    }
}
