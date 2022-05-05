using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repres.Application.Features.TimeZones.Queries.GetAll;
using System.Threading.Tasks;

namespace Repres.Server.Controllers.Utilities
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class TimeZoneController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TimeZoneController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Get All Time Zones
        /// </summary>
        /// <returns>Status 200 OK</returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var timeZones = await _mediator.Send(new GetAllTimeZonesQuery());
            return Ok(timeZones);
        }
    }
}
