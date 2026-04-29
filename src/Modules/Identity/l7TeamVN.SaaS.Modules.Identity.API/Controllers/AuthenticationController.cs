using l7TeamVN.SaaS.Modules.Identity.Application.Messaging.Authentication;
using l7TeamVN.SaaS.Presentation.API;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace l7TeamVN.SaaS.Modules.Identity.API.Controllers;

[Route("api/auth")]
public class AuthenticationController(ISender sender) : ApiController
{
    [HttpPost("register")]
    public async Task<IActionResult> RegisterUserAsync(RegisterUserCommand command)
    {
        var result = await sender.Send(command);

        if (result.IsFailure)
        {
            return HandleFailure(result);
        }

        return Ok(result.Value);
    }

    [HttpPost("login")]
    public async Task<IActionResult> LoginUserAsync(LoginUserCommand command)
    {
        var result = await sender.Send(command);
        if (result.IsFailure)
        {
            return HandleFailure(result);
        }
        return Ok(result.Value);
    }
}
