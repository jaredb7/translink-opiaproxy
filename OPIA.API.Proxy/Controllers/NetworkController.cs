using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Castle.Core.Logging;
using OPIA.API.Client.OpiaApiClients;
using OPIA.API.Contracts.OPIAEntities.Request.Network;

namespace OPIA.API.Proxy.Controllers
{

    /// <summary>
    /// Controller proxy/facade for external clients. 
    /// We're attributing "Get" methods with "HttpPost" because it's easier to post the 
    /// objects from an external client than to translate query strings back into objects,
    /// only to fire them off again, simply to satisfy the Web Api naming conventions. 
    /// It allows us to share the data object contracts.
    /// </summary>
    public class NetworkController : ApiController
    {
        /// <summary>
        /// Injected by IoC container for logging purposes
        /// </summary>
        public ILogger Logger { get; set; }

        [HttpPost]
        public async Task<HttpResponseMessage> GetRouteTimeTables(RouteTimeTablesRequest request)
        {
            var networkRequestClient = new OpiaNetworkClient();
            var result = await networkRequestClient.GetRouteTimeTablesAsync(request);
            var response = Request.CreateResponse(HttpStatusCode.OK, result);
            return response;
        }

        [HttpPost]
        public async Task<HttpResponseMessage> GetStopTimeTables(StopTimeTablesRequest request)
        {
            var networkRequestClient = new OpiaNetworkClient();
            var result = await networkRequestClient.GetStopTimeTablesAsync(request);
            var response = Request.CreateResponse(HttpStatusCode.OK, result);
            return response;
        }

        [HttpPost]
        public async Task<HttpResponseMessage> GetTrips(TripsRequest request)
        {
            var networkRequestClient = new OpiaNetworkClient();
            var result = await networkRequestClient.GetTripsAsync(request);
            var response = Request.CreateResponse(HttpStatusCode.OK, result);
            return response;
        }

        [HttpPost]
        public async Task<HttpResponseMessage> GetTripMapPath(TripMapPathRequest request)
        {
            var networkRequestClient = new OpiaNetworkClient();
            var result = await networkRequestClient.GetTripMapPathAsync(request);
            var response = Request.CreateResponse(HttpStatusCode.OK, result);
            return response;
        }

        [HttpPost]
        public async Task<HttpResponseMessage> GetRouteMapPath(RouteMapPathRequest request)
        {
            var networkRequestClient = new OpiaNetworkClient();
            var result = await networkRequestClient.GetRouteMapPathAsync(request);
            var response = Request.CreateResponse(HttpStatusCode.OK, result);
            return response;
        }

        [HttpPost]
        public async Task<HttpResponseMessage> GetRoutes(RoutesRequest request)
        {
            var networkRequestClient = new OpiaNetworkClient();
            var result = await networkRequestClient.GetRoutesAsync(request);
            var response = Request.CreateResponse(HttpStatusCode.OK, result);
            return response;
        }



    }


}