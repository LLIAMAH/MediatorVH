# MediatorVH

Include to the project NuGetPackage: **MediatorVH** latest version

Add extension configuration
```
builder.Services.AddMediatorVh([Assembly.GetExecutingAssembly()]);
```

Remember, that exists two options to pass types to the types scanning process:

1. 
```
public static IServiceCollection AddMediatorVh(this IServiceCollection services, IEnumerable<Type> types)
{
   /// ...
}
```
2. 
```
public static IServiceCollection AddMediatorVh(this IServiceCollection services, IEnumerable<Assembly> assemblies)
{
   /// ...
}
```

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
