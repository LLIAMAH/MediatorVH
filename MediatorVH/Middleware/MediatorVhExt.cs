using MediatorVH.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Scrutor;

namespace MediatorVH.Middleware;

public static class MediatorVhExt
{
    public static IServiceCollection AddMediatorVh(this IServiceCollection services, IEnumerable<Type> types)
    {
        services.Scan(scan => scan.FromAssembliesOf(types)
            .SelectTypesFilter());
        return services;
    }

    public static IServiceCollection AddMediatorVh(this IServiceCollection services, IEnumerable<Assembly> assemblies)
    {
        services.Scan(scan =>
            scan.FromAssemblies(assemblies).SelectTypesFilter());
        return services;
    }

    private static IImplementationTypeSelector SelectTypesFilter(this IImplementationTypeSelector typesSelector)
    {
        typesSelector
            .AddClasses(classes => classes.AssignableTo(typeof(IQueryHandler<,>)), publicOnly: false)
            .AsImplementedInterfaces()
            .AddClasses(classes => classes.AssignableTo(typeof(ICommandHandler<>)), publicOnly: false)
            .AsImplementedInterfaces()
            .AddClasses(classes => classes.AssignableTo(typeof(ICommandHandler<,>)), publicOnly: false)
            .AsImplementedInterfaces()
            .WithScopedLifetime();

        return typesSelector;
    }
}