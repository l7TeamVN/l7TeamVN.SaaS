using l7TeamVN.SaaS.Presentation.API;
using MediatR;
using Microsoft.AspNetCore.Components;

namespace l7TeamVN.SaaS.Modules.Identity.API.Controllers;

[Route("api/authen")]
public class AuthenticationController(ISender sender) : ApiController(sender)
{
}
