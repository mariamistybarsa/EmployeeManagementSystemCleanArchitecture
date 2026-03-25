using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace EmployeeManagement.Configuration.DependencyInjection;


public class DefaultRequestHeadersHandler : DelegatingHandler
{
    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        if (!request.Headers.Contains("Accept"))
            request.Headers.TryAddWithoutValidation("Accept", "*/*");

        if (!request.Headers.Contains("Accept-Encoding"))
            request.Headers.TryAddWithoutValidation("Accept-Encoding", "br, gzip, deflate");

        if (request.Headers.UserAgent.Count == 0)
            request.Headers.UserAgent.TryParseAdd("Mozilla/5.0 (Windows NT 10.0; Win64; x64)");

        return base.SendAsync(request, cancellationToken);
    }
}