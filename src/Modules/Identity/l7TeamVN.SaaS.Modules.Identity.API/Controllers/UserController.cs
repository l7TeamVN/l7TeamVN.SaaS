using l7TeamVN.SaaS.Modules.Identity.Application.Messaging.Users;
using l7TeamVN.SaaS.Presentation.API;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace l7TeamVN.SaaS.Modules.Identity.API.Controllers;

[Route("api/[controller]")]
[Authorize]
public class UserController(ISender sender) : ApiController
{
    [HttpGet]
    public async Task<IActionResult> GetUsersAsync()
    {
        var result = await sender.Send(new GetUsersQuery());

        if (result.IsFailure)
        {
            return HandleFailure(result);
        }

        return Ok(result.Value);
    }
}
