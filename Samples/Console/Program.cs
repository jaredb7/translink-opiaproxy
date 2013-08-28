using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using OPIA.API.Contracts.Constants;
using OPIA.API.Contracts.OPIAEntities.Request.Location;
using OPIA.API.Contracts.OPIAEntities.Request.Network;
using OPIA.API.Contracts.OPIAEntities.Response.RouteTimeTable;
using OPIA.API.Contracts.OPIAEntities.Response.Stops;

namespace SampleConsole
{
    class Program
    {
        /// <summary>
        /// Contrived example which demonstrates how to get the stops for a specific
        /// trip on a route, and display their addresses and geographical co-ordinates.
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            var sampleClass = new SampleClass();
            sampleClass.ShowHowToGetStopsAndStopAddressForARoute();
            Console.WriteLine("Press enter to close...");
            Console.ReadLine();
        }
    }

    public class SampleClass
    {
        private HttpClient _client;
        private const string Route130 = "130"; // this could just as easily be a train or ferry route

        public SampleClass()
        {
            SetupNewHttpClient();
        }

        private void SetupNewHttpClient()
        {
            const string baseUrl = "http://playopia.azurewebsites.net/api/"; // or from configuration file
            _client = new HttpClient { BaseAddress = new Uri(baseUrl) };
            // change this to "application/xml" if you're determined to do it old-school.
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _client.DefaultRequestHeaders.Add("ApiKey", "this_could_be_anything"); // and use SSL!
            // any SSL validation callbacks, auth or setting of tokens or cookies whatever for your specific proxy implementation will probably need to go in here. 
        }


        public void ShowHowToGetStopsAndStopAddressForARoute()
        {
            Log(string.Format("Loading Route for bus {0}", Route130));
            GetStopsAddressesFor(Route130);
        }


        public void GetStopsAddressesFor(string routeNo)
        {
            var routeTimeTables = GetRouteTimeTablesFor(routeNo);

            // get all the timetables from tomorrow's routes for all '130' buses
            var trips = from timetables in routeTimeTables select timetables.Trips; 
            Console.WriteLine("{0} trips found for bus route '{1}'", trips.Count(), Route130);

            // get the first set of stops from the first bus doing the first trip of the route (each '130' bus will
            // do a trip (which has multiple stops) multiple times a day, and there's multiple '130' buses). Each trip
            // can be inbound OR outbound, so check the Direction property to see which it is, or filter on it.
            var firstTripStopIds = trips.First().Select(t => t.StopIds.ToList()); 
            string displayStopIds = string.Join(", ", firstTripStopIds.First().ToList());
            Console.WriteLine("First trip visits the following Stops: {0}", displayStopIds);
            Console.WriteLine();
            ;

            var stopInfo = GetInformationForStops(firstTripStopIds.First().ToList());
            foreach (var detail in stopInfo)
            {
                Console.WriteLine(detail);
            }

        }

        private IEnumerable<RouteTimeTable> GetRouteTimeTablesFor(string routeNo)
        {
            var requestEntity = new RouteTimeTablesRequest
                                {
                                    RouteCodes = new List<string> { routeNo },
                                    Date = DateTime.Today.AddDays(1),
                                    // remember some express pre-paid bus routes don't run on weekends e.g. P137
                                    FilterToStartEndStops = false,
                                    VehicleType = VehicleType.Bus
                                };

            var response = _client.PostAsJsonAsync("network/getroutetimetables", requestEntity).Result;
            response.EnsureSuccessStatusCode();
            var routeTimeTables = response.Content.ReadAsAsync<RouteTimeTablesResponse>().Result.RouteTimeTables.ToList();
            return routeTimeTables;
        }

        /// <summary>
        /// Gets the addresses for all the stops, and their geographical co-ordinates,
        /// and tries to format it a little more neatly.
        /// </summary>
        /// <param name="stopIds"></param>
        /// <returns></returns>
        private IEnumerable<string> GetInformationForStops(IEnumerable<string> stopIds)
        {
            var requestEntity = new StopsRequest
            {
                StopIds = stopIds.ToList()
            };

            var response = _client.PostAsJsonAsync("location/getstopsbyids", requestEntity).Result;
            response.EnsureSuccessStatusCode();
            StopsResponse result = response.Content.ReadAsAsync<StopsResponse>().Result;

            var stopInfo = result.Stops.Select(
                                                s =>
                                                    new
                                                    {
                                                        Zone = s.Zone,
                                                        StopId = s.StopId,
                                                        Street = s.Street,
                                                        Suburb = s.Suburb,
                                                        Latitude = s.Position.Lat,
                                                        Longitude = s.Position.Lng
                                                    });

            var stopDetails = new List<string>()
                              {
                                  string.Format("{0}     :\t{1}\t\t\t\t{2}\t\t\t\t{3}", "StopId", "Street", "Suburb", "Position")
                              };
            Array.ForEach(stopInfo.ToArray(), s => stopDetails.Add(string.Format("{0} (Z{1}):\t{2}\t{3}\t\t\t({4},{5}))", s.StopId, s.Zone, s.Street, s.Suburb, s.Latitude, s.Longitude)));
            return stopDetails;
        }


        private void Log(string message)
        {
            Console.WriteLine("{0} {1}", DateTime.Now.ToString("s"), message);
        }


    }
}