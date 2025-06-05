# MediatorVH

Include to the project NuGetPackage: **MediatorVH** latest version

Add extension configuration
```
builder.Services.AddMediatorVh([Assembly.GetExecutingAssembly()]);
```

Remember, that exists two options to pass types to the types scanning process:

1. Scan takes the provided type assemblies and will scan all these assemblies for required interfaces
```
public static IServiceCollection AddMediatorVh(this IServiceCollection services,
                                               IEnumerable<Type> types) { ...
```
2. Scan takes the provided assemblies and will scan all of them for required interfaces
```
public static IServiceCollection AddMediatorVh(this IServiceCollection services,
                                               IEnumerable<Assembly> assemblies) { ...
```

-for client source scanning is responsible the Scrutor v6.0.1.

And finally create standard Feature sets of the Command/Handler
```
public record GetUsersCommand : ICommand<ResultList<UserDto>>;

public class GetUsersCommandHandler : ICommandHandler<GetUsersCommand, ResultList<UserDto>>
{
    /// Initialization of the command required objects

    public async Task<ResultList<UserDto>> Handle(GetUsersCommand request,
        CancellationToken cancellationToken)
    {
        IEnumerable<UserDto> data = ....
        /// Some code to get data

        return new ResultList<UserDto>(data);
    }
}
```
