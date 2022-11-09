using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Repres.Application.Interfaces.Repositories;
using Repres.Application.Interfaces.Serialization.Serializers;
using Repres.Application.Interfaces.Services.SheetApi;
using Repres.Application.Interfaces.Services.Storage;
using Repres.Application.Interfaces.Services.Storage.Provider;
using Repres.Application.Interfaces.Services.ThirdParty;
using Repres.Application.Serialization.JsonConverters;
using Repres.Application.Serialization.Options;
using Repres.Application.Serialization.Serializers;
using Repres.Infrastructure.Helper;
using Repres.Infrastructure.Repositories;
using Repres.Infrastructure.Services.SheetApi;
using Repres.Infrastructure.Services.SheetApi.Options;
using Repres.Infrastructure.Services.Storage;
using Repres.Infrastructure.Services.Storage.Provider;
using Repres.Infrastructure.Services.ThirdParty;
using Repres.Infrastructure.Services.ThirdParty.Options;
using System;
using System.Linq;
using System.Reflection;

namespace Repres.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddInfrastructureMappings(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            return services
                .AddTransient<IApiRepository, ApiRepository>()
                .AddTransient<IApiByUserRepository, ApiByUserRepository>()
                .AddTransient<IOuraRepository, OuraRepository>()
                .AddTransient(typeof(IRepositoryAsync<,>), typeof(RepositoryAsync<,>))
                .AddTransient<IDocumentRepository, DocumentRepository>()
                .AddTransient<IDocumentTypeRepository, DocumentTypeRepository>()
                .AddTransient(typeof(IUnitOfWork<>), typeof(UnitOfWork<>));
        }

        public static IServiceCollection AddThirdPartyServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddOptions<OuraAuthOptions>()
                .Bind(configuration.GetSection(nameof(OuraAuthOptions)));
            services.AddTransient<IApiService, OuraApiService>();

            services.AddOptions<SheetOptions>()
                .Bind(configuration.GetSection(nameof(SheetOptions)));
            services.AddTransient<ISheetApi, SheetApi>();
            
            services.AddSingleton(typeof(GoogleSheetsHelper));
            return services;
        }

        public static IServiceCollection AddExtendedAttributesUnitOfWork(this IServiceCollection services)
        {
            return services
                .AddTransient(typeof(IExtendedAttributeUnitOfWork<,,>), typeof(ExtendedAttributeUnitOfWork<,,>));
        }

        public static IServiceCollection AddServerStorage(this IServiceCollection services)
            => AddServerStorage(services, null);

        public static IServiceCollection AddServerStorage(this IServiceCollection services, Action<SystemTextJsonOptions> configure)
        {
            return services
                .AddScoped<IJsonSerializer, SystemTextJsonSerializer>()
                .AddScoped<IStorageProvider, ServerStorageProvider>()
                .AddScoped<IServerStorageService, ServerStorageService>()
                .AddScoped<ISyncServerStorageService, ServerStorageService>()
                .Configure<SystemTextJsonOptions>(configureOptions =>
                {
                    configure?.Invoke(configureOptions);
                    if (!configureOptions.JsonSerializerOptions.Converters.Any(c => c.GetType() == typeof(TimespanJsonConverter)))
                        configureOptions.JsonSerializerOptions.Converters.Add(new TimespanJsonConverter());
                });
        }
    }
}