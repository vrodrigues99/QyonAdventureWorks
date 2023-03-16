using Domain;
using Domain.Core.Notifications;
using Domain.Entities;
using Domain.Handlers;
using Domain.Interfaces;
using Domain.Interfaces.Service;
using Infrastructure.Data.Context;
using Infrastructure.Data.Repositories.Base;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Services;
using Services.Base;

namespace IoC
{
    public static class NativeInjectorBootStrapper
    {
        public static void RegisterDependenciInjetions(IServiceCollection services)
        {
            // ASPNET
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            // Domain Bus (Mediator)
            services.AddScoped<IMediatorHandler, MediatorHandler>();

            // Domain
            services.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();

            //Repository
            services.AddScoped<IBaseRepository<Competidores>, BaseRepository<Competidores>>();
            services.AddScoped<IBaseRepository<PistaCorrida>, BaseRepository<PistaCorrida>>();
            services.AddScoped<IBaseRepository<HistoricoCorrida>, BaseRepository<HistoricoCorrida>>();

            //CONTEXT
            services.AddScoped<DataContext>();

            // SERVICES
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IBaseService<Competidores>, BaseService<Competidores>>();
            services.AddScoped<IBaseService<PistaCorrida>, BaseService<PistaCorrida>>();
            services.AddScoped<IBaseService<HistoricoCorrida>, BaseService<HistoricoCorrida>>();
        }
    }
}
