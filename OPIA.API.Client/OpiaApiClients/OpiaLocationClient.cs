using System.Threading.Tasks;
using OPIA.API.Contracts.Constants;
using OPIA.API.Contracts.OPIAEntities.Request;
using OPIA.API.Contracts.OPIAEntities.Response.Locations;
using OPIA.API.Contracts.OPIAEntities.Response.Resolve;
using OPIA.API.Contracts.OPIAEntities.Response.Stops;
using OPIA.API.Contracts.OPIAEntities.Response.StopsAtLandmark;
using OPIA.API.Contracts.OPIAEntities.Response.StopsNearby;

namespace OPIA.API.Client.OpiaApiClients
{
    /// Abstraction/Facade client layer for interfacing with the actual Opia API method calls. Allows
    /// us to change names, add functionality, etc.
    public partial class OpiaLocationClient : OpiaBaseClient
    {

        //GET /location/rest/resolve Suggests landmarks, stops, addresses, etc from free-form text
        //GET /location/rest/stops-at-landmark/{locationId} Retrieves a list of Stops located at a Landmark
        //GET /location/rest/stops-nearby/{locationId} Locates stops close to a specific location
        //GET /location/rest/stops Retrieves a list of stops by their Stop ID
        //GET /location/rest/locations Retrieves one or more locations by their ID

        public OpiaLocationClient() : base(OpiaApiConstants.LocationAPI)
        {
        }


        /// <summary>
        /// Suggests landmarks, stops, addresses, etc from free-form tex
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public ResolveResponse Resolve(IRequest request)
        {
            var result = this.GetApiResult<IRequest, ResolveResponse>(request);
            return result;
        }

        /// <summary>
        /// Retrieves a list of Stops located at a Landmark
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public StopsAtLandmarkResponse GetStopsAtLandmark(IRequest request)
        {
            var result = this.GetApiResult<IRequest, StopsAtLandmarkResponse>(request);
            return result;
        }


        /// <summary>
        /// Locates stops close to a specific location
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public StopsNearbyResponse GetStopsNearby(IRequest request)
        {
            var result = this.GetApiResult<IRequest, StopsNearbyResponse>(request);
            return result;
        }

        /// <summary>
        /// Retrieves a list of stops by their Stop ID
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public StopsResponse GetStopsByIds(IRequest request)
        {
            var result = this.GetApiResult<IRequest, StopsResponse>(request);
            return result;
        }

        /// <summary>
        /// Retrieves one or more locations by their ID
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public LocationsResponse GetLocationsByIds(IRequest request)
        {
            var result = this.GetApiResult<IRequest, LocationsResponse>(request);
            return result;
        }
    }
}