using l7TeamVN.SaaS.Modules.Identity.Application.Authentication.Register;
using l7TeamVN.SaaS.Presentation.API;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace l7TeamVN.SaaS.Modules.Identity.API.Controllers;

[Route("api/[controller]")]
public class UserController(ISender sender) : ApiController(sender)
{
    [HttpPost("register")]
    public async Task<IActionResult> RegisterUser(RegisterUserCommand command)
    {
        var result = await sender.Send(command);

        if (result.IsFailure)
        {
            return HandleFailure(result);
        }

        return Ok(result.Value);
    }
}
