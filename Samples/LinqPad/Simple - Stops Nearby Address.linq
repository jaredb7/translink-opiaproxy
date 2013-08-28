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
    var pc = new TestClass();
    pc.GetStopsNearbyAddress();
}

public class TestClass
{
 
    private HttpClient SetupNewHttpClient()
  	{
  		string baseUrl = "http://playopia.azurewebsites.net/api/";  // or configged via ConfigurationManager.AppSettings["proxyApiBaseUrl"];
  		var client = new HttpClient { BaseAddress = new Uri(baseUrl) };
  		client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json")); // change this to "application/xml" if you're determined to do it old-school.
        client.DefaultRequestHeaders.Add("ApiKey", "this_could_be_anything"); // and use SSL!
  		// any SSL validation callbacks, auth or setting of tokens or cookies whatever for your specific proxy implementation will probably need to go in here. 
  		return client;
  	}
  
  	public void GetStopsNearbyAddress()
  	{
  		HttpClient client = SetupNewHttpClient(); // normally you'd do this once per class, not once per method.
  		var requestEntity = new StopsNearbyRequest
  		{
  			LocationId = "AD:Anzac Rd, Eudlo", // LocationId would need to be retrieved via another API call,
  			UseWalkingDistance = true,
  			RadiusInMetres = 2000,
  			MaxResults = 10,
  		};
  
  		var response = client.PostAsJsonAsync("location/getstopsnearby", requestEntity).Result;
  		response.EnsureSuccessStatusCode();
  		StopsNearbyResponse result = response.Content.ReadAsAsync<StopsNearbyResponse>().Result;
      if (result.NearbyStops.Any())
      {
        foreach(var stop in result.NearbyStops)
        {
          Console.WriteLine("Stop Id: {0} is {1}m away", stop.StopId, stop.Distance.DistanceM);
        }
      }
      Console.WriteLine("{0} GetStopsNearby Found: {1}", DateTime.Now.ToString("s"), result.NearbyStops.Count());
  	}
  
}

// Define other methods and classes here
