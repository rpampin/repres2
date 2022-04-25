using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repres.Application.Features.Apis.Commands.Edit;
using Repres.Application.Features.Apis.Commands.ExecuteImport;
using Repres.Application.Features.Apis.Commands.TokenPersist;
using Repres.Application.Features.Apis.Queries.GetAccess;
using Repres.Application.Features.Apis.Queries.GetAll;
using Repres.Application.Features.Apis.Queries.GetById;
using Repres.Application.Requests.ThirdParty;
using Repres.Shared.Constants.Permission;
using System.Threading.Tasks;

namespace Repres.Server.Controllers.v1.ThirdParty
{
    public class ApisController : BaseApiController<ApisController>
    {
        /// <summary>
        /// Get All Apis
        /// </summary>
        /// <returns>Status 200 OK</returns>
        [Authorize(Policy = Permissions.Apis.View)]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var apis = await _mediator.Send(new GetAllApisQuery());
            return Ok(apis);
        }

        /// <summary>
        /// Get a Api By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Status 200 Ok</returns>
        [Authorize(Policy = Permissions.Apis.View)]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var api = await _mediator.Send(new GetApiByIdQuery() { Id = id });
            return Ok(api);
        }

        /// <summary>
        /// Create/Update a Brand
        /// </summary>
        /// <param name="command"></param>
        /// <returns>Status 200 OK</returns>
        [Authorize(Policy = Permissions.Apis.Edit)]
        [HttpPost]
        public async Task<IActionResult> Post(EditApiCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        /// <summary>
        /// Get a Api Access for User
        /// </summary>
        /// <returns>Status 200 Ok</returns>
        [HttpGet("access/{userId}")]
        public async Task<IActionResult> GetApiAccess(string userId)
        {
            var api = await _mediator.Send(new GetApiAccessQuery() { UserId = userId });
            return Ok(api);
        }

        /// <summary>
        /// Create/Update a Brand
        /// </summary>
        /// <param name="command"></param>
        /// <returns>Status 200 OK</returns>
        [HttpPost("token/persist")]
        public async Task<IActionResult> Post(TokenPersistCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        /// <summary>
        /// Execute APIs Processes by UserId
        /// </summary>
        /// <returns>Status 200 Ok</returns>
        [HttpPost("process/{userId}")]
        public async Task<IActionResult> ProcessApiData(string userId, [FromBody] ProcessDateRange parameters)
        {
            var api = await _mediator.Send(new ExecuteImportCommand() { UserId = userId, Start = parameters?.Start, End = parameters?.End });
            return Ok(api);
        }
    }
}
