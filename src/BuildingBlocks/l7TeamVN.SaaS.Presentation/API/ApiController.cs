using l7TeamVN.SaaS.SharedKernel.Results;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace l7TeamVN.SaaS.Presentation.API;

[ApiController]
public class ApiController : ControllerBase
{
    protected IActionResult HandleFailure(Result result) =>
        result switch
        {
            { IsSuccess: true } => throw new InvalidOperationException(),
            _ =>
                BadRequest(CreateProblemDetails(result))
        };

    private static ProblemDetails CreateProblemDetails(Result result)
    {
        if (result.Errors.Count() > 1)
        {
            return new ProblemDetails
            {
                Title = "Bad Request",
                Type = result.Errors[0].Code,
                Detail = result.Errors[0].Message,
                Status = StatusCodes.Status400BadRequest,
                Extensions = {
                    ["errors"] = result.Errors.Skip(1).Select(e => new
                    {
                        e.Code,
                        e.Message
                    })
                }
            };
        }
        return new ProblemDetails
        {
            Title = "Bad Request",
            Type = result.Error.Code,
            Detail = result.Error.Message,
            Status = StatusCodes.Status400BadRequest
        };
    }
}
