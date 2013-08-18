using OPIA.API.Contracts.Constants;
using OPIA.API.Contracts.OPIAEntities.Request;
using OPIA.API.Contracts.OPIAEntities.Response.Plan;
using OPIA.API.Contracts.OPIAEntities.Response.PlanUrl;

namespace OPIA.API.Client.OpiaApiClients
{
    /// Abstraction/Facade client layer for interfacing with the actual Opia API method calls. Allows
    /// us to change names, add functionality, etc.
    public partial class OpiaTravelClient : OpiaBaseClient
    {
        //GET /travel/rest/plan/{fromLocationId}/{toLocationId} Generates travel plans between two locations
        //GET /travel/rest/plan-url/{fromLocationId}/{toLocationId} Generates a URL to Translink's Journey Planner which suggests possible journeys


        public OpiaTravelClient() : base(OpiaApiConstants.LocationAPI)
        {
        }

        /// <summary>
        /// Generates travel plans between two locations
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public PlanResponse GetPlan(IRequest request)
        {
            var result = this.GetApiResult<IRequest, PlanResponse>(request);
            return result;
        }

        /// <summary>
        /// Generates a URL to Translink's Journey Planner which suggests possible journeys
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public PlanUrlResponse GetPlanUrl(IRequest request)
        {
            var result = this.GetApiResult<IRequest, PlanUrlResponse>(request);
            return result;
        }
    }
}