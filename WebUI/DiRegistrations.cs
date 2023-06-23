using Microsoft.OpenApi.Models;
using ProductOverview.Infrastructure.DbContexts;

namespace WebUI;

public static class DiRegistrations
{
    public static IServiceCollection AddWebUIServices(this IServiceCollection services)
    {
        services.AddDatabaseDeveloperPageExceptionFilter();

        services.AddHttpContextAccessor();

        services.AddHealthChecks()
                .AddDbContextCheck<ApplicationDbContext>();

        services.AddControllersWithViews();

        services.AddRazorPages();

        services.AddSwaggerGen(x =>
        {
            x.SwaggerDoc("v1", new OpenApiInfo { Title = "ProductsOverview Api", Version = "v1" });
            x.EnableAnnotations();
        });

        services.AddCors(options =>
        {
            options.AddPolicy("DevCorsPolicy",
                              builder => builder.WithOrigins("http://localhost:4200")
                                                .AllowAnyHeader()
                                                .AllowAnyMethod());
        });

        return services;
    }
}
