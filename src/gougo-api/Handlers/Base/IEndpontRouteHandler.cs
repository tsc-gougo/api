namespace gougo_api.Handlers.Base;

public interface IEndpointRouteHandler
{
    void MapEndpoints(IEndpointRouteBuilder app);
}