using System.Linq;
using System.Net;
using System.Threading.Tasks;
using DNS.Client;
using DNS.Client.RequestResolver;
using DNS.Protocol;
using DNS.Protocol.ResourceRecords;
using Nancy;
using HttpStatusCode = Nancy.HttpStatusCode;
using Request = DNS.Protocol.Request;

namespace clausndk.services.dnslookup
{
    public class LookupModule : NancyModule
    {
        public LookupModule()
            : base("/api/")
        {
            Get("lookup/{domainname}", Lookup);
        }

        private async Task<object> Lookup(dynamic arg)
        {
            string domainname = arg.domainname;
            if (string.IsNullOrEmpty(domainname))
                return HttpStatusCode.BadRequest;

            var request = new ClientRequest(IPAddress.Parse("213.133.98.98"), 53, new Request(), new TcpRequestResolver());
            request.Questions.Add(new Question(Domain.FromString(domainname), RecordType.ANY, RecordClass.ANY));
            request.RecursionDesired = true;
            request.OperationCode = OperationCode.Query;

            var response = await request.Resolve();
            var list = response.AnswerRecords
                               .Select(r => new ResponseDto
                                            {
                                                Type = r.Type,
                                                Address = (BaseResourceRecord)r,
                                                Ttl = r.TimeToLive.TotalMilliseconds
                                            })
                               .ToList();

            return Response.AsJson(list);
        }
    }

    internal class ResponseDto {
        public RecordType Type { get; set; }
        public BaseResourceRecord Address { get; set; }
        public double Ttl { get; set; }
    }
}
