using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;

namespace EmployeeManagement.Configuration.DependencyInjection;

public static class WebApplicationExtensions
{
    public static void UseProjectConfiguration(this WebApplication app)
    {
        app.UseSwaggerIfDevelopment();
        app.UseCors("AllowAll");
        app.UseSecurity();
        app.UseRoutingAndAuthorization();
    }

    private static void UseSwaggerIfDevelopment(this WebApplication app)
    {
        app.UseSwagger(c =>
        {
            c.RouteTemplate = "employee-management/swagger/{documentName}/swagger.json";
        });

        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint(
                "/employee-management/swagger/v1/swagger.json",
                "Employee Management API V1"
            );

            options.RoutePrefix = "employee-management/swagger";
        });
    }

    private static void UseRoutingAndAuthorization(this WebApplication app)
    {
        app.MapControllers();
    }

    private static void UseSecurity(this WebApplication app)
    {
        app.UseAuthentication();
        app.UseAuthorization();
    }
}