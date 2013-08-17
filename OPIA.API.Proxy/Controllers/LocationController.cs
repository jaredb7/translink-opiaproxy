using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Castle.Core.Logging;
using OPIA.API.Client.OpiaApiClients;
using OPIA.API.Contracts.OPIAEntities.Request;
using OPIA.API.Contracts.OPIAEntities.Request.Location;
using OPIA.API.Contracts.OPIAEntities.Request.Network;
using OPIA.API.Contracts.OPIAEntities.Response.Locations;
using OPIA.API.Contracts.OPIAEntities.Response.Stops;
using OPIA.API.Contracts.OPIAEntities.Response.StopsAtLandmark;
using OPIA.API.Contracts.OPIAEntities.Response.StopsNearby;

namespace OPIA.API.Proxy.Controllers
{

    /// <summary>
    /// Controller proxy/facade for external clients. 
    /// We're attributing "Get" methods with "HttpPost" because it's easier to post the 
    /// objects from an external client than to translate query strings back into objects,
    /// only to fire them off again, simply to satisfy the Web Api naming conventions. 
    /// It allows us to share the data object contracts.
    /// </summary>
    public class LocationController : ApiController
    {
        /// <summary>
        /// Injected by IoC container for logging purposes
        /// </summary>
        public ILogger Logger { get; set; }

        /// <summary>
        /// Attempts to resolves free-form text into a an address, stop, landmark, etc.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<HttpResponseMessage> Resolve(ResolveRequest request)
        {
            var locationClient = new OpiaLocationClient();
            var result = await locationClient.ResolveAsync(request);
            var response = Request.CreateResponse(HttpStatusCode.OK, result);
            return response;
        }

        /// <summary>
        /// Retrieves a list of Stops located at a Landmark
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<HttpResponseMessage> GetStopsAtLandmark(StopsAtLandmarkRequest request)
        {
            var locationClient = new OpiaLocationClient();
            var result = await locationClient.GetStopsAtLandmarkAsync(request);
            var response = Request.CreateResponse(HttpStatusCode.OK, result);
            return response;
        }

        /// <summary>
        /// Locates stops close to a specific location
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<HttpResponseMessage> GetStopsNearby(StopsNearbyRequest request)
        {
            var locationClient = new OpiaLocationClient();
            var result = await locationClient.GetStopsNearbyAsync(request);
            var response = Request.CreateResponse(HttpStatusCode.OK, result);
            return response;
        }

        /// <summary>
        /// Retrieves a list of stops by their Stop ID
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<HttpResponseMessage> GetStopsByIds(StopsRequest request)
        {
            var locationClient = new OpiaLocationClient();
            var result = await locationClient.GetStopsByIdsAsync(request);
            var response = Request.CreateResponse(HttpStatusCode.OK, result);
            return response;
        }

        /// <summary>
        /// Retrieves one or more locations by their ID
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<HttpResponseMessage> GetLocationsByIds(LocationsRequest request)
        {
            var locationClient = new OpiaLocationClient();
            var result = await locationClient.GetLocationsByIdsAsync(request);
            var response = Request.CreateResponse(HttpStatusCode.OK, result);
            return response;
        }


    }


}