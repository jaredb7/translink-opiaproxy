using System.Threading.Tasks;
using OPIA.API.Contracts.OPIAEntities.Request;
using OPIA.API.Contracts.OPIAEntities.Response.RouteMapPath;
using OPIA.API.Contracts.OPIAEntities.Response.Routes;
using OPIA.API.Contracts.OPIAEntities.Response.RouteTimeTable;
using OPIA.API.Contracts.OPIAEntities.Response.StopTimeTable;
using OPIA.API.Contracts.OPIAEntities.Response.TripMapPath;
using OPIA.API.Contracts.OPIAEntities.Response.Trips;

namespace OPIA.API.Client.OpiaApiClients
{
    public partial class OpiaNetworkClient
    {

        /// <summary>
        /// Retrieves timetables for one or more Routes on a date
        /// </summary>
        /// <param name="request">Request parameters object</param>
        /// <returns></returns>
        public async Task<RouteTimeTablesResponse> GetRouteTimeTablesAsync(IRequest request)
        {
            var result = await this.GetApiResultAsync<IRequest, RouteTimeTablesResponse>(request);
            return result;
        }

        public async Task<StopTimeTablesResponse> GetStopTimeTablesAsync(IRequest request)
        {
            var result = await this.GetApiResultAsync<IRequest, StopTimeTablesResponse>(request);
            return result;
        }

        public async Task<TripsResponse> GetTripsAsync(IRequest request)
        {
            var result = await this.GetApiResultAsync<IRequest, TripsResponse>(request);
            return result;
        }

        public async Task<TripMapPathResponse> GetTripMapPathAsync(IRequest request)
        {
            var result = await this.GetApiResultAsync<IRequest, TripMapPathResponse>(request);
            return result;
        }

        public async Task<RouteMapPathResponse> GetRouteMapPathAsync(IRequest request)
        {
            var result = await this.GetApiResultAsync<IRequest, RouteMapPathResponse>(request);
            return result;
        }

        public async Task<RoutesResponse> GetRoutesAsync(IRequest request)
        {
            var result = await this.GetApiResultAsync<IRequest, RoutesResponse>(request);
            return result;
        }
    }
}