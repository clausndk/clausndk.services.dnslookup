using Microsoft.AspNetCore.Mvc;

namespace clausndk.services.dnslookup.web.Controllers
{
    [ApiController]
    public class PingController : ControllerBase
    {
        [HttpGet("api/ping")]
        public string Ping()
        {
            return $"clausndk DNS lookup service pong {DateTimeOffset.UtcNow:O}";
        }
    }
}