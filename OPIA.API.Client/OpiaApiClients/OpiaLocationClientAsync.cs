using System.Threading.Tasks;
using OPIA.API.Contracts.OPIAEntities.Request;
using OPIA.API.Contracts.OPIAEntities.Response.Locations;
using OPIA.API.Contracts.OPIAEntities.Response.Resolve;
using OPIA.API.Contracts.OPIAEntities.Response.Stops;
using OPIA.API.Contracts.OPIAEntities.Response.StopsAtLandmark;
using OPIA.API.Contracts.OPIAEntities.Response.StopsNearby;

namespace OPIA.API.Client.OpiaApiClients
{
    public partial class OpiaLocationClient
    {

        /// <summary>
        /// Suggests landmarks, stops, addresses, etc from free-form tex
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ResolveResponse> ResolveAsync(IRequest request)
        {
            var result = await this.GetApiResultAsync<IRequest, ResolveResponse>(request);
            return result;
        }

        /// <summary>
        /// Retrieves a list of Stops located at a Landmark
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<StopsAtLandmarkResponse> GetStopsAtLandmarkAsync(IRequest request)
        {
            var result = await this.GetApiResultAsync<IRequest, StopsAtLandmarkResponse>(request);
            return result;
        }

        /// <summary>
        /// Locates stops close to a specific location
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<StopsNearbyResponse> GetStopsNearbyAsync(IRequest request)
        {
            var result = await this.GetApiResultAsync<IRequest, StopsNearbyResponse>(request);
            return result;
        }

        /// <summary>
        /// Retrieves a list of stops by their Stop ID
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<StopsResponse> GetStopsByIdsAsync(IRequest request)
        {
            var result = await this.GetApiResultAsync<IRequest, StopsResponse>(request);
            return result;
        }

        /// <summary>
        /// Retrieves one or more locations by their ID
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<LocationsResponse> GetLocationsByIdsAsync(IRequest request)
        {
            var result = await this.GetApiResultAsync<IRequest, LocationsResponse>(request);
            return result;
        }
    }
}