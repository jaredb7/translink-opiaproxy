using System.Threading.Tasks;
using OPIA.API.Contracts.Constants;
using OPIA.API.Contracts.OPIAEntities.Request;
using OPIA.API.Contracts.OPIAEntities.Response.RouteMapPath;
using OPIA.API.Contracts.OPIAEntities.Response.Routes;
using OPIA.API.Contracts.OPIAEntities.Response.RouteTimeTable;
using OPIA.API.Contracts.OPIAEntities.Response.StopTimeTable;
using OPIA.API.Contracts.OPIAEntities.Response.TripMapPath;
using OPIA.API.Contracts.OPIAEntities.Response.Trips;

namespace OPIA.API.Client.OpiaApiClients
{
    /// <summary>
    /// Abstraction/Facade client layer for interfacing with the actual Opia API method calls. Allows
    /// us to change names, add functionality, etc.
    /// </summary>
    public class OpiaNetworkClient : OpiaBaseClient
    {
        //GET /network/rest/stop-timetables Retrieves Stop Timetables for a particular date
        //GET /network/rest/route-timetables Retrieves timetables for one or more Routes on a date.
        //GET /network/rest/trips Retrieves details of a specific trip, including all its stops
        //GET /network/rest/trip-map-path Retrieves geo-coordinates a trip takes in polyline format
        //GET /network/rest/route-map-path Retrieves geo-coordinates a route takes
        //GET /network/rest/routes Retrieves Routes operating on a particular date

        public OpiaNetworkClient(): base(OpiaApiConstants.NetworkAPI)
        {

        }

        /// <summary>
        /// Retrieves timetables for one or more Routes on a date
        /// </summary>
        /// <param name="request">Request parameters object</param>
        /// <returns></returns>
        public RouteTimeTablesResponse GetRouteTimeTables(IRequest request)
        {
            var result = this.GetApiResult<IRequest, RouteTimeTablesResponse>(request);
            return result;
        }

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

        public StopTimeTablesResponse GetStopTimeTables(IRequest request)
        {
            var result = this.GetApiResult<IRequest, StopTimeTablesResponse>(request);
            return result;
        }

        public async Task<StopTimeTablesResponse> GetStopTimeTablesAsync(IRequest request)
        {
            var result = await this.GetApiResultAsync<IRequest, StopTimeTablesResponse>(request);
            return result;
        }


        public TripsResponse GetTrips(IRequest request)
        {
            var result = this.GetApiResult<IRequest, TripsResponse>(request);
            return result;
        }

        public async Task<TripsResponse> GetTripsAsync(IRequest request)
        {
            var result = await this.GetApiResultAsync<IRequest, TripsResponse>(request);
            return result;
        }

        public TripMapPathResponse GetTripMapPath(IRequest request)
        {
            var result = this.GetApiResult<IRequest, TripMapPathResponse>(request);
            return result;
        }

        public async Task<TripMapPathResponse> GetTripMapPathAsync(IRequest request)
        {
            var result = await this.GetApiResultAsync<IRequest, TripMapPathResponse>(request);
            return result;
        }

        public RouteMapPathResponse GetRouteMapPath(IRequest request)
        {
            var result = this.GetApiResult<IRequest, RouteMapPathResponse>(request);
            return result;
        }

        public async Task<RouteMapPathResponse> GetRouteMapPathAsync(IRequest request)
        {
            var result = await this.GetApiResultAsync<IRequest, RouteMapPathResponse>(request);
            return result;
        }

        public RoutesResponse GetRoutes(IRequest request)
        {
            var result = this.GetApiResult<IRequest, RoutesResponse>(request);
            return result;
        }

        public async Task<RoutesResponse> GetRoutesAsync(IRequest request)
        {
            var result = await this.GetApiResultAsync<IRequest, RoutesResponse>(request);
            return result;
        }
    }
}