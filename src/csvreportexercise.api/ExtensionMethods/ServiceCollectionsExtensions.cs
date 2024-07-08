using csvreportexercise.api.Configurations;
using csvreportexercise.application.Handlers.V1;
using csvreportexercise.application.Handlers.V1.Interfaces;
using csvreportexercise.application.Models.V1;
using csvreportexercise.application.Services.V1;
using csvreportexercise.application.Services.V1.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;

namespace csvreportexercise.api.ExtensionMethods;

public static class ServiceCollectionsExtensions
{
    public static void ConfigureSwagger(this IServiceCollection services)
    {
        services.AddApiVersioning(options =>
        {
            options.DefaultApiVersion = new ApiVersion(1, 0);
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.ReportApiVersions = true;
            options.ApiVersionReader = ApiVersionReader.Combine(new UrlSegmentApiVersionReader(),
                new HeaderApiVersionReader("x-api-version"),
                new MediaTypeApiVersionReader("x-api-version"));
        });

        services.ConfigureOptions<ConfigureSwaggerOptions>();

        services.AddVersionedApiExplorer(setup =>
        {
            setup.GroupNameFormat = "'v'VVV";
            setup.SubstituteApiVersionInUrl = true;
        });
    }

    public static void AddDependencies(this IServiceCollection services)
    {
        services.AddScoped<ICsvReportHandler, CsvReportHandler>();
        services.AddSingleton<ICacheService, CacheService>();
        services.AddScoped<IFormFileService, FormFileService>();
        services.AddScoped<IOpenLibraryService, OpenLibraryService>();
    }

    public static void ConfigureOptions(this IServiceCollection services, ConfigurationManager configuration)
    {
        var openLibrarySection = configuration.GetSection("OpenLibrary");
        services.Configure<OpenLibrarySettings>(openLibrarySection);
    }
}