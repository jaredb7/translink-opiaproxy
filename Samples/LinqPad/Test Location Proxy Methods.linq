<Query Kind="Program">
  <Reference Relative="..\..\Projects\Personal\OpiaAPI\OPIA.API.Contracts\bin\Debug\OPIA.API.Contracts.dll">C:\Users\John\Projects\Personal\OpiaAPI\OPIA.API.Contracts\bin\Debug\OPIA.API.Contracts.dll</Reference>
  <NuGetReference>Microsoft.AspNet.WebApi.Client</NuGetReference>
  <Namespace>Newtonsoft.Json</Namespace>
  <Namespace>Newtonsoft.Json.Converters</Namespace>
  <Namespace>Newtonsoft.Json.Linq</Namespace>
  <Namespace>Newtonsoft.Json.Schema</Namespace>
  <Namespace>Newtonsoft.Json.Serialization</Namespace>
  <Namespace>OPIA.API.Contracts.Constants</Namespace>
  <Namespace>OPIA.API.Contracts.OPIAEntities.Request</Namespace>
  <Namespace>OPIA.API.Contracts.OPIAEntities.Request.Location</Namespace>
  <Namespace>OPIA.API.Contracts.OPIAEntities.Request.Network</Namespace>
  <Namespace>OPIA.API.Contracts.OPIAEntities.Request.Travel</Namespace>
  <Namespace>OPIA.API.Contracts.OPIAEntities.Request.Version</Namespace>
  <Namespace>OPIA.API.Contracts.OPIAEntities.Response</Namespace>
  <Namespace>OPIA.API.Contracts.OPIAEntities.Response.Common</Namespace>
  <Namespace>OPIA.API.Contracts.OPIAEntities.Response.Locations</Namespace>
  <Namespace>OPIA.API.Contracts.OPIAEntities.Response.Plan</Namespace>
  <Namespace>OPIA.API.Contracts.OPIAEntities.Response.PlanUrl</Namespace>
  <Namespace>OPIA.API.Contracts.OPIAEntities.Response.Resolve</Namespace>
  <Namespace>OPIA.API.Contracts.OPIAEntities.Response.RouteMapPath</Namespace>
  <Namespace>OPIA.API.Contracts.OPIAEntities.Response.Routes</Namespace>
  <Namespace>OPIA.API.Contracts.OPIAEntities.Response.RouteTimeTable</Namespace>
  <Namespace>OPIA.API.Contracts.OPIAEntities.Response.Stops</Namespace>
  <Namespace>OPIA.API.Contracts.OPIAEntities.Response.StopsAtLandmark</Namespace>
  <Namespace>OPIA.API.Contracts.OPIAEntities.Response.StopsNearby</Namespace>
  <Namespace>OPIA.API.Contracts.OPIAEntities.Response.StopTimeTable</Namespace>
  <Namespace>OPIA.API.Contracts.OPIAEntities.Response.TripMapPath</Namespace>
  <Namespace>OPIA.API.Contracts.OPIAEntities.Response.Trips</Namespace>
  <Namespace>System.Net.Http</Namespace>
  <Namespace>System.Net.Http.Formatting</Namespace>
  <Namespace>System.Net.Http.Handlers</Namespace>
  <Namespace>System.Net.Http.Headers</Namespace>
</Query>

/* Remember to add reference to NuGet Web Api Client package, and to the OPIA.API.Contracts assembly*/

void Main()
{
  var tests = new ProxyLocationControllerMethodsTests();
  tests.ResolveFreeFormText();
  tests.GetStopsAtLandmark();
  tests.GetStopsNearbyLocationId();
  tests.GetStopsByIds();
  tests.GetLocationsByIds();
}

    public class ProxyLocationControllerMethodsTests
    {
        private HttpClient _client;

        public ProxyLocationControllerMethodsTests()
        {
            SetupNewHttpClient();
        }


        private void SetupNewHttpClient()
        {
            string baseUrl = "http://playopia.azurewebsites.net/api/"; //ConfigurationManager.AppSettings["proxyApiBaseUrl"];
            _client = new HttpClient {BaseAddress = new Uri(baseUrl)};
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			_client.DefaultRequestHeaders.Add("ApiKey", "this_could_be_anything"); // and use SSL!
            // any SSL, auth or setting of tokens or cookies whatever will probably need to go in here. 
        }

        public void ResolveFreeFormText()
        {
            Console.WriteLine("In Resolve...");
            var requestEntity = new ResolveRequest
                                {
                                    LookupText = "Anzac Sq., Brisbane",
                                    LocationType = 0,
                                    MaxResults = 10,
                                };

            var response = _client.PostAsJsonAsync("location/resolve", requestEntity).Result;
            response.EnsureSuccessStatusCode();
            var result = response.Content.ReadAsAsync<ResolveResponse>().Result;
            Console.WriteLine("{0} Resolve: Locations Found: {1}", DateTime.Now.ToString("s"), result.Locations.Count());
            result.Dump();
        }


        public void GetStopsAtLandmark()
        {
            Console.WriteLine("In GetStopsAtLandmark...");
            // note that this is not the right landmark type, but it does serve to exercise the query (we just get nothing back)
            // Need to find a landmark of type LocationId.LandmarkType.Landmark to use as a parameter
            var requestEntity = new StopsAtLandmarkRequest()
                                {
                                    LocationId = "LM:Hotels & Motels:The Sebel King George Square",
                                };

            var response = _client.PostAsJsonAsync("location/getstopsatlandmark", requestEntity).Result;
            response.EnsureSuccessStatusCode();
            var result = response.Content.ReadAsAsync<ResolveResponse>().Result;
            Console.WriteLine("{0} GetStopsAtLandmark: Landmark Locations Found (expected zero)", DateTime.Now.ToString("s"));
            result.Dump();
        }


        public void GetStopsNearbyLocationId()
        {
            Console.WriteLine("In GetStopsNearby...");
            var requestEntity = new StopsNearbyRequest
                                {
                                    LocationId = "AD:Anzac Rd, Eudlo",
                                    UseWalkingDistance = true,
                                    RadiusInMetres = 2000,
                                    MaxResults = 10,
                                };

            var response = _client.PostAsJsonAsync("location/getstopsnearby", requestEntity).Result;
            response.EnsureSuccessStatusCode();
            var result = response.Content.ReadAsAsync<StopsNearbyResponse>().Result;
            if (result.NearbyStops.Any())
            {
              foreach(var stop in result.NearbyStops)
              {
                Console.WriteLine("Stop Id: {0} is {1}m away", stop.StopId, stop.Distance.DistanceM);
              }
            }
            Console.WriteLine("{0} GetStopsNearby Found: {1}", DateTime.Now.ToString("s"), result.NearbyStops.Count());
            result.Dump();
        }


        public void GetStopsByIds()
        {
            Console.WriteLine("In GetStopsByIds...");
            var requestEntity = new StopsRequest
                                {
                                    StopIds = new List<string>() {"000026", "005468"}
                                };

            var response = _client.PostAsJsonAsync("location/getstopsbyids", requestEntity).Result;
            response.EnsureSuccessStatusCode();
            var result = response.Content.ReadAsAsync<StopsResponse>().Result;
            Console.WriteLine("{0} GetStopsByIds Found: {1}", DateTime.Now.ToString("s"), result.Stops.Count());
            result.Dump();
        }


        public void GetLocationsByIds()
        {
            Console.WriteLine("In GetLocationsByIds...");
            var requestEntity = new LocationsRequest()
                                {
                                    LocationIds =
                                        new List<string>()
                                        {
                                            "LM:Parks & Reserves:Anzac Square",
                                            "LM:Parks & Reserves:Botanic Gardens"
                                        }
                                };

            var response = _client.PostAsJsonAsync("location/getlocationsbyids", requestEntity).Result;
            response.EnsureSuccessStatusCode();
            var result = response.Content.ReadAsAsync<LocationsResponse>().Result;
            Console.WriteLine("{0} GetLocationsByIds Found: {1}", DateTime.Now.ToString("s"), result.Locations.Count());
            result.Dump();
        }
    }