using Application.Features.OrderEvents.Commands.Add;
using Application.Features.OrderEvents.Queries.GetAll;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    public class OrderEventsController : ApiControllerBase
    {
        [HttpPost("Add")]
        public async Task<IActionResult> AddAsync(OrderEventAddCommand command)
        {
            return Ok(await Mediator.Send(command));
        }
        [HttpGet("Pull")]
        public async Task<IActionResult> GetAllAsync(bool? isDeleted)
        {
            var query = new OrderEventGetAllQuery(isDeleted);
            return Ok(await Mediator.Send(query));
        }
    }
}