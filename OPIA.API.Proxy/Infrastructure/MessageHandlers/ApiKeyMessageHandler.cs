using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace OPIA.API.Proxy.Infrastructure.MessageHandlers
{

    /// <summary>
    /// Rudimentary Api Key handler (perhaps for tracking usage per client), all it does is check that your client
    /// sent an "ApiKey" header with some arbitrary text in it. You'll need to harden this appropriate 
    /// to your needs. Oh, and SSL. Because SSL.
    /// </summary>
    public class ApiKeyMessageHandler : DelegatingHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (!ValidateApiKey(request))
            {
                var invalidApiKeyResponse = request.CreateErrorResponse(HttpStatusCode.Unauthorized, "Invalid API Key");
                return base.SendAsync(request, cancellationToken).ContinueWith(task => invalidApiKeyResponse, cancellationToken);
            }
            return base.SendAsync(request, cancellationToken);
        }

        private bool ValidateApiKey(HttpRequestMessage request)
        {
            IEnumerable<string> apiKeyHeaders;
            if (!request.Headers.TryGetValues("ApiKey", out apiKeyHeaders)) return false;

            string apiKey = apiKeyHeaders.FirstOrDefault();
            if (!string.IsNullOrWhiteSpace(apiKey))
            {
                // validate your API Key here
                return true;
            }
            return false;
        }
    }
}