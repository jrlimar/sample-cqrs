using Dev.Api.Commands;
using Dev.Api.Queries;
using Dev.Api.RebusHandlers;
using Dev.Core.AppSettings;
using Dev.Core.Mediator;
using Dev.Core.Messages.IntegrationEvents;
using Dev.Core.Notifications;
using Dev.Domain.Interfaces;
using Dev.Repositories.SqlServer;
using Dev.Services.Handlers;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Rebus.Config;
using Rebus.Persistence.InMem;
using Rebus.Retry.Simple;
using Rebus.Routing.TypeBased;
using Rebus.ServiceProvider;
using Rebus.Timeouts;
using System;

namespace Dev.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IPedidoRepository, PedidoRepository>();
            services.AddScoped<IMediatorHandler, MediatorHandler>();
            services.AddScoped<IDomainNotification, DomainNotificationHandler>();
            services.AddScoped<IPedidoQueries, PedidoQueries>();

            ConfigureAppSettings(services);
            ConfigureMediator(services);
            ConfigureSwagger(services);
            ConfigureRebus(services);

            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //Rebus
            app.UseRebus(c =>
            {
                c.Subscribe<PedidoRealizadoEvent>(); ////aqui são mensagens Publish
            });

            app.UseSwagger();
            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1"); });
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }

        private static void ConfigureRebus(IServiceCollection services)
        {
            var nomeFila = "fila_rebus2";

            services.AddRebus(configure => configure
                    .Transport(t => t.UseRabbitMq("amqp://localhost", nomeFila))
                    .Routing(r =>
                    {
                        r.TypeBased()
                            .MapAssemblyOf<AdicionarPedidoRebusCommand>(nomeFila); //aqui são mensagens mapeadas(Send)
                    })
                    //.Timeouts(t => t.StoreInMemory()) // ficou infinito
                    .Options(o =>
                    {
                        o.SetNumberOfWorkers(1);
                        o.SetMaxParallelism(1);
                        o.SimpleRetryStrategy(maxDeliveryAttempts: 2, secondLevelRetriesEnabled: true);
                        o.SetBusName("Demo Rebus2");
                    })
                );

            services.AutoRegisterHandlersFromAssemblyOf<PedidoRebusCommandHandler>();
            services.AutoRegisterHandlersFromAssemblyOf<PedidoRealizadoEventHandler>();
        }


        private void ConfigureAppSettings(IServiceCollection services)
        {
            services.AddSingleton<RabbitSettings>(Configuration.GetSection("RabbitMq").Get<RabbitSettings>());
        }

        private static void ConfigureMediator(IServiceCollection services)
        {
            services.AddMediatR(AppDomain.CurrentDomain.Load("Dev.Api"));
            services.AddMediatR(AppDomain.CurrentDomain.Load("Dev.Core"));
            services.AddMediatR(AppDomain.CurrentDomain.Load("Dev.Domain"));
        }

        private static void ConfigureSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Cqrs-Event",
                    Version = "v1",
                    Description = "Cqrs-Event",
                    License = new OpenApiLicense { Name = "PIXEON", Url = new Uri("https://www.pixeon.com") }

                });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] { }
                    }
                });
            });
        }
    }
}
