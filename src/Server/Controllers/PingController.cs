using Microsoft.AspNetCore.Mvc;

namespace Repres.Server.Controllers
{
    /// <summary>
    /// Abstract BaseApi Controller Class
    /// </summary>
    [ApiController]
    [Route("api/ping")]
    public class PingController : ControllerBase
    {
        [HttpGet]
        public ActionResult<string> Get() => Ok("pong");
    }
}
