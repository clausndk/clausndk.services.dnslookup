using DnsClient;
using System.Net;

var rootServers = new List<string> { 
        "198.41.0.4", "170.247.170.2", "192.33.4.12", "199.7.91.13", "192.203.230.10", "192.5.5.241", "192.112.36.4", "198.97.190.53", "192.36.148.17", "192.58.128.30", "193.0.14.129", "199.7.83.42", "202.12.27.33",
        "2001:500:2::c"
    }
    .Select(ip => NameServer.ToNameServer(IPAddress.Parse(ip)))
    .ToArray();
		
var options = new LookupClientOptions(rootServers)
{
    UseCache = false,
    UseRandomNameServer = true,
    AutoResolveNameServers = false,
    Recursion = false,
    Retries = 3,
    Timeout = TimeSpan.FromSeconds(5)
};
var client = new LookupClient(options);

var builder = WebApplication.CreateSlimBuilder(args);
builder.Services.AddSingleton<ILookupClient>(client);
builder.Services.AddControllers();

var app = builder.Build();
app.UseRouting();
app.UseEndpoints(erb => erb.MapControllers());

await app.RunAsync();
