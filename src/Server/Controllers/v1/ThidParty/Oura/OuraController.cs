using Microsoft.AspNetCore.Mvc;
using Repres.Application.Features.Oura.Command.ResetData;
using System.Threading.Tasks;

namespace Repres.Server.Controllers.v1.ThidParty.Oura
{
    public class OuraController : BaseApiController<OuraController>
    {
        /// <summary>
        /// Remove Oura user's data and google sheet
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>Status 200 OK</returns>
        [HttpPost("reset/{userId}")]
        public async Task<IActionResult> Post(string userId)
        {
            return Ok(await _mediator.Send(new ResetDataCommand { UserId = userId }));
        }
    }
}
