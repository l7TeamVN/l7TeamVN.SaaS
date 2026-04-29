using l7TeamVN.SaaS.Modules.Identity.Application.Users;
using l7TeamVN.SaaS.Presentation.API;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace l7TeamVN.SaaS.Modules.Identity.API.Controllers;

[Route("api/[controller]")]
public class UserController(ISender sender) : ApiController
{
    [HttpGet]
    public async Task<IActionResult> GetUsers()
    {
        var result = await sender.Send(new GetUsersQuery());

        if (result.IsFailure)
        {
            return HandleFailure(result);
        }

        return Ok(result.Value);
    }
}
