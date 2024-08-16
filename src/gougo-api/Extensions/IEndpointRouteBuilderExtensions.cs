namespace gougo_api.Extensions;

public static class IEndpointRouteBuilderExtensions
{
    public static void MapEndpoints(this IEndpointRouteBuilder app,
        Assembly assembly)
    {
        var endpointRouteHandlerInterfaceType = typeof(IEndpointRouteHandler);

        var endpointRouteHandlerTypes = assembly.GetTypes().Where(t =>
            t is { IsClass: true, IsAbstract: false, IsGenericType: false }
            && t.GetConstructor(Type.EmptyTypes) != null
            && endpointRouteHandlerInterfaceType.IsAssignableFrom(t)
        );

        foreach (var endpointRouteHandlerType in endpointRouteHandlerTypes)
        {
            var instantiatedType = (IEndpointRouteHandler)Activator
                .CreateInstance(endpointRouteHandlerType)!;

            instantiatedType.MapEndpoints(app);
        }
    }
}