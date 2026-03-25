using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using EmployeeManagement.Configuration.Security;
using EmployeeManagement.Persistence.AppDbContext;
using EmployeeManagement.Shared.Swagger;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace EmployeeManagement.Configuration.DependencyInjection;

public static class StartupConfigurationExtensions
{
    public static void ConfigureProjectSettings(this WebApplicationBuilder builder, IConfiguration config)
    {
        builder.ConfigureDatabase();
     
        builder.ConfigureMvcAndSwagger(config);
        builder.Services.AddDistributedMemoryCache();
        builder.Services.AddHttpContextAccessor();
        builder.Services.ConfigureOptions<ConfigureSwaggerOptions>();
        builder.Services.RegisterApplicationServices(config);
    }
    
    private static void ConfigureDatabase(this WebApplicationBuilder builder)
    {
        var connectionString = builder.Configuration.GetConnectionString("EmployeeManagementDb");
        if (string.IsNullOrWhiteSpace(connectionString))
        {
            throw new InvalidOperationException("Connection string 'EmployeeManagementDb' is not configured.");
        }

        builder.Services.AddDbContext<EmployeeManagementDbContext>(options =>
            options.UseSqlServer(connectionString));
    }
    
    private static void ConfigureMvcAndSwagger(this WebApplicationBuilder builder, IConfiguration config)
    {
      builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(options =>
        {
            options.AddBearerSecurity();
        });

        builder.Services.AddJwtAuthentication(config, requireHttpsMetadata: false);
    }


 
}