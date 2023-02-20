using AutoMapper;
using Microsoft.Extensions.Logging;
using Rebus.Handlers;
using Seje.OrdenCaptura.CommandStack.Events;
using System;
using System.Threading.Tasks;

namespace Seje.OrdenCaptura.QueryStack.Denormalizers
{
    public class OrdenCapturaDenormalizer :
        IHandleMessages<OrdenCapturaRegistradaEvent>,
        IHandleMessages<OrdenCapturaModificadaEvent>
    {
        private readonly IMapper _mapper;
        private readonly IRepository<OrdenCaptura> _repository;
        private readonly ILogger<OrdenCapturaDenormalizer> _logger;

        public OrdenCapturaDenormalizer(
          IMapper mapper,
          IRepository<OrdenCaptura> repository,
          ILogger<OrdenCapturaDenormalizer> logger)
        {
            _mapper = mapper;
            _repository = repository;
            _logger = logger;
        }

        public async Task Handle(OrdenCapturaRegistradaEvent message)
        {
            try
            {
                var ordenCaptura = _mapper.Map<OrdenCaptura>(message);
                ordenCaptura.UsuarioCreacion = message.UserName;
                ordenCaptura.UsuarioModificacion = message.UserName;
                ordenCaptura.FechaCreacion = DateTime.Now;
                ordenCaptura.FechaModificacion = DateTime.Now;
                var create = await _repository.AddAsync(ordenCaptura);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public async Task Handle(OrdenCapturaModificadaEvent message)
        {
            try
            {
                var ordenCaptura = _mapper.Map<OrdenCaptura>(message);
                ordenCaptura.UsuarioCreacion = message.UserName;
                ordenCaptura.UsuarioModificacion = message.UserName;
                ordenCaptura.FechaCreacion = DateTime.Now;
                ordenCaptura.FechaModificacion = DateTime.Now;
                await _repository.UpdateAsync(ordenCaptura);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }
    }
}
