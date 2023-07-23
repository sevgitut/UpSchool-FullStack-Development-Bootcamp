﻿using Application.Common.Models.Log;
using Domain.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using WebApi.Hubs;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeleniumLogsController : ControllerBase
    {
        private readonly IHubContext<SeleniumLogHub> _seleniumLogHubContext;

        public SeleniumLogsController(IHubContext<SeleniumLogHub> seleniumLogHubContext)
        {
            _seleniumLogHubContext = seleniumLogHubContext;
        }

        [HttpPost]
        public async Task<IActionResult> SendLogNotificationAsync(SendLogNotificationApiDto logNotificationApiDto)
        {
            await _seleniumLogHubContext.Clients.AllExcept(logNotificationApiDto.ConnectionId)
                .SendAsync("NewSeleniumLogAdded", logNotificationApiDto.Log);

            return Ok();
        }
    }
}
