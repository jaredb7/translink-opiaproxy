using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Castle.Core.Logging;
using OPIA.API.Client.OpiaApiClients;
using OPIA.API.Contracts.OPIAEntities.Request;
using OPIA.API.Contracts.OPIAEntities.Request.Travel;
using OPIA.API.Contracts.OPIAEntities.Response.Plan;
using OPIA.API.Contracts.OPIAEntities.Response.PlanUrl;

namespace OPIA.API.Proxy.Controllers
{

    /// <summary>
    /// Controller proxy/facade for external clients. 
    /// We're attributing "Get" methods with "HttpPost" because it's easier to post the 
    /// objects from an external client than to translate query strings back into objects,
    /// only to fire them off again, simply to satisfy the Web Api naming conventions. 
    /// It allows us to share the data object contracts.
    /// </summary>
    public class TravelController : ApiController
    {
        /// <summary>
        /// Injected by IoC container for logging purposes
        /// </summary>
        public ILogger Logger { get; set; }

        /// <summary>
        /// Generates travel plans between two locations
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<HttpResponseMessage> GetPlanAsync(PlanRequest request)
        {
            var travelClient = new OpiaTravelClient();
            var result = await travelClient.GetApiResultAsync<IRequest, PlanResponse>(request);
            var response = Request.CreateResponse(HttpStatusCode.OK, result);
            return response;
        }

        /// <summary>
        /// Generates a URL to Translink's Journey Planner which suggests possible journeys
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<HttpResponseMessage> GetPlanUrlAsync(PlanUrlRequest request)
        {
            var travelClient = new OpiaTravelClient();
            var result = await travelClient.GetApiResultAsync<IRequest, PlanUrlResponse>(request);
            var response = Request.CreateResponse(HttpStatusCode.OK, result);
            return response;
        }


    }


}