using MediatorVH.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace MediatorVH.Middleware;

public static class MediatorVhExt
{
    public static IServiceCollection AddMediatorVh(this IServiceCollection services, IEnumerable<Type> types)
    {
        services.Scan(scan => scan.FromAssembliesOf(types)
            .AddClasses(classes => classes.AssignableTo(typeof(IQueryHandler<,>)), publicOnly: false)
            .AsImplementedInterfaces()
            .AddClasses(classes => classes.AssignableTo(typeof(ICommandHandler<>)), publicOnly: false)
            .AsImplementedInterfaces()
            .AddClasses(classes => classes.AssignableTo(typeof(ICommandHandler<,>)), publicOnly: false)
            .AsImplementedInterfaces()
            .WithScopedLifetime());

        return services;
    }
}