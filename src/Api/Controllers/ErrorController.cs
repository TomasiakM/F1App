using Domain.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[ApiExplorerSettings(IgnoreApi = true)]
public sealed class ErrorController : ControllerBase
{
    [Route("/error")]
    public IActionResult Error()
    {
        var exception = HttpContext.Features.Get<IExceptionHandlerFeature>()?.Error;

        if(exception is null)
        {
            return Problem();
        }

        if(exception is ValidationFailedException validationEx)
        {
            return CreateValidationResponse(validationEx);
        }

        var message = "Coś poszło nie tak";
        var status = 500;

        switch (exception)
        {
            case DomainException ex:
                message = ex.Message;
                status = 400;
                break;
            case UnauthorizedException ex:
                message = ex.Message;
                status = 401;
                break;
            case ForbiddenException ex:
                message = ex.Message;
                status = 403;
                break;
            case NotFoundException ex:
                message = ex.Message;
                status = 404;
                break;
        }

        return Problem(statusCode: status, detail: message);
    }

    private static IActionResult CreateValidationResponse(ValidationFailedException validationEx)
    {
        var problemDetails = new ProblemDetails()
        {
            Status = 400,
            Detail = "Błąd walidacji danych",
        };

        problemDetails.Extensions.Add("errors", validationEx.ValidationErrors);

        return new ObjectResult(problemDetails)
        {
            StatusCode = 400,
        };
    }
}
