using System.Reflection;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace ProductOverview.Application;

public static class DiRegistrations
{
    public static IServiceCollection RegisterApplicationServices(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddMediatR(Assembly.GetExecutingAssembly());

        return services;
    }
}
