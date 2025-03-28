using DnsClient;
using Microsoft.AspNetCore.Mvc;

namespace clausndk.services.dnslookup.web.Controllers;

public class DnsController(ILookupClient dnsClient) : ControllerBase
{
    [HttpPost("api/dns/query")]
    public async Task<IActionResult> Query([FromBody] DnsQuery query)
    {
        var result = await dnsClient.QueryAsync(query.Entry, QueryType.ANY, QueryClass.IN, HttpContext.RequestAborted);
        return Ok(result);
    }

    public record DnsQuery
    {
        public required string Entry { get; set; }
    }
}