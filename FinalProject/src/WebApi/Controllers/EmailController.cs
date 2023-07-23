using Microsoft.AspNetCore.Mvc;
using Application.Features.Email.Commands.Add;

namespace WebApi.Controllers;

public class EmailController : ApiControllerBase
{
    [HttpPost("SendEmail")]
    public async Task<IActionResult> SendEmailAsync(SendEmailAddCommand command)
    {
        return Ok(await Mediator.Send(command));
    }
}