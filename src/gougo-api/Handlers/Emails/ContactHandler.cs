using Application.Interfaces.Business;
using Domain.DataTransfersObject.Inputs.Contacts;

namespace gougo_api.Handlers.Emails;

public class ContactHandler : IEndpointRouteHandler
{
    public void MapEndpoints(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/contact", Send)
            .WithTags("Contact")
            .WithName("ContactMail")
            .Produces(201)
            .Produces(401)
            .Produces<ProblemDetails>(400)
            .ProducesValidationProblem()
            .WithOpenApi(operation => new OpenApiOperation(operation)
            {
                Summary = "send contact mail to gougo"
            });
    }

    private IResult Send([FromBody] ContactInput data, [FromServices] IBusinessService businessService)
    {
        businessService.SendContactMail(data);

        return TypedResults.NoContent();
    }
}