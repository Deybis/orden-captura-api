﻿using MediatR;
using MementoFX.Messaging;
using MementoFX.Messaging.Rebus;
using MementoFX.Persistence;
using MementoFX.Persistence.SqlServer.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Rebus.Config;
using Rebus.Routing.TypeBased;
using Rebus.Serialization.Json;
using Seje.Firma.Client;
using Seje.OrdenCaptura.Api.Infrastructure.Interfaces;
using Seje.OrdenCaptura.Api.Mapper;
using Seje.OrdenCaptura.Api.Services;
using Seje.OrdenCaptura.CommandStack.Events;
using Seje.OrdenCaptura.QueryStack;
using Seje.OrdenCaptura.QueryStack.Denormalizers;
using System;
using System.Net.Http;
using System.Reflection;
using Seje.FileManager.Client;
using Seje.Authorization.Service;
using Seje.OrdenCaptura.Api.Models;
using Seje.Expediente.Client;
using WkHtmlToPdfDotNet.Contracts;
using WkHtmlToPdfDotNet;

namespace Seje.OrdenCaptura.Api
{
    public static class ServiceCollectionExtensions
    {
        public const string REBUS_RABBIT_CON_STRING = "Rebus:Transport:ConnectionString";
        public const string REBUS_QUEUE_NAME = "Rebus:QueueName";
        public const string SQL_SERVER_EVENT_CON_STRING = "EventsConnectionString";
        public const string SQL_SERVER_READ_CON_STRING = "OrdenCapturaConnectionString";
        public const string API_URL_FIRMA = "Apis:FirmaApiUrl";
        public const string API_URL_FILEMANAGER = "FileManagerSettings:UrlService";
        public const string API_URL_EXPEDIENTE = "Apis:ExpedienteUrl";

        public static void AddServiceConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMediatR(typeof(CommandStack.Commands.RegistrarOrdenCapturaCommand));
            services.AddMediatR(typeof(CommandStack.Commands.ModificarOrdenCapturaCommand));

            services.AddSingleton(typeof(IConverter), new SynchronizedConverter(new PdfTools()));
            services.AddAutoMapper(config => config.AddProfile<MappingProfile>(), AppDomain.CurrentDomain.GetAssemblies());
            services.AddScoped(typeof(IOrdenCaptura), typeof(OrdenCapturaService));
            services.AddScoped(typeof(IExpediente), typeof(Services.ExpedienteService));
            services.AddScoped(typeof(IDelito), typeof(DelitoService));
            services.AddScoped(typeof(IParte), typeof(ParteService));
            services.AddScoped(typeof(IOrdenCapturaEstado), typeof(OrdenCapturaEstadoService));
            services.AddScoped(typeof(IFirma), typeof(FirmaService));
            services.AddScoped(typeof(IConfiguracion), typeof(ConfiguracionService));
            services.AddScoped(typeof(ITipoFirma), typeof(TipoFirmaService));
            services.AddScoped(typeof(IDocumento), typeof(DocumentoService));
            services.AddScoped(typeof(ITipoDocumento), typeof(TipoDocumentoService));
            services.AddScoped(typeof(IFirmante), typeof(FirmanteService));
            services.AddScoped(typeof(IConsulta), typeof(ConsultaService));
            services.AddScoped<OrdenCapturaService>();
            services.AddScoped<Services.ExpedienteService>();
            services.AddScoped<DelitoService>();
            services.AddScoped<ParteService>();
            services.AddScoped<OrdenCapturaEstadoService>();
            services.AddScoped<FirmaService>();
            services.AddScoped<ConfiguracionService>();
            services.AddScoped<TipoFirmaService>();
            services.AddScoped<DocumentoService>();
            services.AddScoped<TipoDocumentoService>();
            services.AddScoped<FirmanteService>();
            services.AddScoped<ConsultaService>();
            services.AddScoped<ProtectedApiBearerTokenHandler>();

            services.AddEventStoreConfiguration(configuration);
            services.AddBusConfiguration(configuration);
            services.AddHandlersConfiguration();
            services.AddQueryStackConfiguration(configuration);
            services.AddHttpClientsConfiguration(configuration);
            services.AddAuthorizationService(configuration);
        }

        private static void AddAuthorizationService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthorizationConfig(opts =>
            {
                var redisOpts = new Authorization.Service.Models.RedisConfiguration();
                configuration.GetSection(AppSettings.REDIS_CONFIGURATION).Bind(redisOpts);

                opts.Host = configuration.GetValue<string>(AppSettings.API_URL_AUTHORIZATION);
                opts.Component = configuration.GetValue<string>(AppSettings.COMPONENT_AUTHORIZATION);
                opts.RedisConfiguration = redisOpts;
            });
        }

        private static void AddEventStoreConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString(SQL_SERVER_EVENT_CON_STRING);
            var schemaName = "Events";

            Settings settings = new Settings(connectionString, useSingleTable: true);
            services.AddSingleton(settings);

            EventTableMapping eventTableMapping = new EventTableMapping(schemaName);
            eventTableMapping.MapEventsOfAggregate<CommandStack.Models.OrdenCaptura>("OrdenesCaptura");
            services.AddSingleton(eventTableMapping);

            services.AddTransient<IEventStore, MementoFX.Persistence.SqlServer.SqlServerEventStore>();
            services.AddTransient<IRepository, Repository>();
        }


        private static void AddBusConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddRebus(config =>
                config.Logging(l => l.Trace())
                    .Routing(r => r.TypeBased()
                    .MapAssemblyNamespaceOf<OrdenCapturaRegistradaEvent>(configuration[REBUS_QUEUE_NAME])
                    .MapAssemblyNamespaceOf<OrdenCapturaModificadaEvent>(configuration[REBUS_QUEUE_NAME]))
                    .Transport(t =>
                    {
                        t.UseRabbitMq(configuration[REBUS_RABBIT_CON_STRING], configuration[REBUS_QUEUE_NAME]);
                    })
                    .Serialization(s => s.UseNewtonsoftJson(JsonInteroperabilityMode.PureJson))
            );
            services.AddTransient<IEventDispatcher, RebusEventDispatcher>();
        }

        private static void AddHandlersConfiguration(this IServiceCollection services)
        {
            services.AutoRegisterHandlersFromAssemblyOf<OrdenCapturaDenormalizer>();
        }

        private static void AddQueryStackConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<OrdenCapturaDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString(SQL_SERVER_READ_CON_STRING),
                    sqlOptions => sqlOptions.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName))
            );
            services.AddScoped<IDatabase, Database>();

            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
        }

        private static void AddHttpClientsConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            // Firma Digital
            //services.AddHttpClient<IFirmaDigitalClient, FirmaClient>(config =>
            //{
            //    config.BaseAddress = new Uri(configuration.GetValue<string>(API_URL_FIRMA));
            //    config.Timeout = TimeSpan.FromSeconds(180);
            //}).AddTransientHttpErrorPolicy(p => p.WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(4, retryAttempt))))
            //.ConfigurePrimaryHttpMessageHandler(() =>
            //{
            //    var handler = new HttpClientHandler();
            //    handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };
            //    return handler;
            //})
            //.AddHttpMessageHandler<ProtectedApiBearerTokenHandler>();
            services.AddHttpClient<IFirmaDigitalClient, FirmaClient>(config =>
            {
                config.BaseAddress = new Uri(configuration.GetValue<string>(API_URL_FIRMA));
                config.Timeout = TimeSpan.FromSeconds(180);
            }).AddTransientHttpErrorPolicy(p => p.WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(4, retryAttempt))))
            .AddHttpMessageHandler<ProtectedApiBearerTokenHandler>();

            // FileManager
            services.AddHttpClient<IFileManagerClient, FileManagerClient>(config =>
            {
                config.BaseAddress = new Uri(configuration.GetValue<string>(API_URL_FILEMANAGER));
                config.Timeout = TimeSpan.FromSeconds(180);
            }).AddTransientHttpErrorPolicy(p => p.WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(4, retryAttempt))))
            .ConfigurePrimaryHttpMessageHandler(() =>
            {
                var handler = new HttpClientHandler();
                handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };
                return handler;
            })
            .AddHttpMessageHandler<ProtectedApiBearerTokenHandler>();

            // Expediente
            services.AddHttpClient<IExpedienteService, Seje.Expediente.Client.ExpedienteService>(config =>
            {
                config.BaseAddress = new Uri(configuration.GetValue<string>(API_URL_EXPEDIENTE));
                config.Timeout = TimeSpan.FromSeconds(180);
            }).AddTransientHttpErrorPolicy(p => p.WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(4, retryAttempt))))
            .ConfigurePrimaryHttpMessageHandler(() =>
            {
                var handler = new HttpClientHandler();
                handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };
                return handler;
            })
            .AddHttpMessageHandler<ProtectedApiBearerTokenHandler>();
        }
    }
}
