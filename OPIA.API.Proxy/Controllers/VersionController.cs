using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Castle.Core.Logging;
using OPIA.API.Client.OpiaApiClients;
using OPIA.API.Contracts.OPIAEntities.Request;
using OPIA.API.Contracts.OPIAEntities.Request.Travel;
using OPIA.API.Contracts.OPIAEntities.Request.Version;
using OPIA.API.Contracts.OPIAEntities.Response.Plan;
using OPIA.API.Contracts.OPIAEntities.Response.PlanUrl;

namespace OPIA.API.Proxy.Controllers
{

    /// <summary>
    /// Controller proxy/facade for external clients to check version and build informatio on
    /// the OPIA API - basically for diagnostics purposes. 
    /// </summary>
    public class VersionController : ApiController
    {
        /// <summary>
        /// Injected by IoC container for logging purposes
        /// </summary>
        public ILogger Logger { get; set; }

        /// <summary>
        /// Hits the 'api' RPC command 
        /// </summary>
        /// <returns>The current version API ("\"1.0\"")</returns>
        /// <exception cref="HttpRequestException">An HttpRequestException containing an HTTP error response (e.g. 404, 403, etc.)</exception>
        [HttpGet]
        public async Task<HttpResponseMessage> GetCurrentApiVersion()
        {
            var versionClient = new OpiaVersionClient();
            var result = await versionClient.GetApiResultAsync<IRequest, string>(new ApiVersionRequest());
            var response = Request.CreateResponse(HttpStatusCode.OK, result);
            return response;
        }

        /// <summary>
        /// This should only be used to get the build version when reporting defects; has
        /// apparently no bearing on the actual API version. Just used for diagnostic purposes
        /// </summary>
        /// <returns>The current version API ("\"1.0\"")</returns>
        /// <exception cref="HttpRequestException">An HttpRequestException containing an HTTP error response (e.g. 404, 403, etc.)</exception>
        [HttpGet]
        public async Task<HttpResponseMessage> GetCurrentApiBuildVersion(PlanUrlRequest request)
        {
            var versionClient = new OpiaVersionClient();
            var result = await versionClient.GetApiResultAsync<IRequest, string>(new BuildVersionRequest());
            var response = Request.CreateResponse(HttpStatusCode.OK, result);
            return response;
        }


    }


}