using System.Net;
using System.Text.Json;
using EmployeeManagement.Application.DTO.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Microsoft.Extensions.Http;

namespace EmployeeManagement.Configuration.DependencyInjection;
public static class ServiceCollectionExtensions
{
    public static void RegisterApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        const string rootNamespace = "EmployeeManagement.";

        ConventionRegistrar.RegisterByNamespace(
            services,
            interfaceNamespace: rootNamespace + "Application.Services.Interfaces",
            implementationNamespace: rootNamespace + "Application.Services.Implementations"
        );

        ConventionRegistrar.RegisterByNamespace(
            services,
            interfaceNamespace: rootNamespace + "Persistence.Services.Interfaces",
            implementationNamespace: rootNamespace + "Persistence.Services.Implementations"
        );
        
        ConventionRegistrar.RegisterByNamespace(
            services,
            interfaceNamespace: rootNamespace + "Infrastructure.Services.Interfaces",
            implementationNamespace: rootNamespace + "Infrastructure.Services.Implementations"
        );

        ConventionRegistrar.RegisterByNamespace(
            services,
            interfaceNamespace: rootNamespace + "Persistence.Repositories.Interfaces",
            implementationNamespace: rootNamespace + "Persistence.Repositories.Implementations"
        );

        ConventionRegistrar.RegisterByNamespace(
            services,
            interfaceNamespace: rootNamespace + "Persistence.UoW.Interface",
            implementationNamespace: rootNamespace + "Persistence.UoW.Implementation"
        );

        ConventionRegistrar.RegisterFluentValidators(services,
            validatorsRootNamespace: rootNamespace + "Application.Dto.Request"
        );

        ConventionRegistrar.RegisterFluentValidatorsFromNamespace(
            services,
            validatorsNamespaceRoot: rootNamespace + "Application.Validators"
        );

        services.AddOptions()
            .Configure<JsonSerializerOptions>(options => { options.PropertyNameCaseInsensitive = true; });

        services.Configure<ServiceInfoOptions>(options =>
        {
            options.Services =
                configuration
                    .GetSection("Service-Info")
                    .Get<Dictionary<string, ServiceInfoItem>>()
                ?? new Dictionary<string, ServiceInfoItem>();
        });

        services.Configure<AppInfoOptions>(
            configuration.GetSection("AppInfo"));

        services.AddHttpClient();
        services.Configure<HttpClientFactoryOptions>(options =>
        {
            options.HttpMessageHandlerBuilderActions.Add(builder =>
            {
                if (builder.PrimaryHandler is HttpClientHandler httpClientHandler)
                {
                    httpClientHandler.AutomaticDecompression =
                        DecompressionMethods.Brotli | DecompressionMethods.GZip | DecompressionMethods.Deflate;
                }
                var provider = builder.Services;
                if (provider.GetService(typeof(DefaultRequestHeadersHandler)) is DelegatingHandler headerHandler)
                {
                    builder.AdditionalHandlers.Insert(0, headerHandler);
                }
            });
        });
        services.AddHttpContextAccessor();
        
   
    }
}