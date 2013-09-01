using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Media;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Android.OS;
using Newtonsoft.Json;
using OPIA.API.Contracts.OPIAEntities.Request.Location;
using OPIA.API.Contracts.OPIAEntities.Response.Resolve;
using Encoding = System.Text.Encoding;

namespace SampleAndroid
{
    [Activity(Label = "SampleAndroid", MainLauncher = true, Icon = "@drawable/icon")]
    public class FindStopsNearbyActivity : Activity
    {
        private HttpClient _client;
        private Button _button;
        private ListView _listView;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            _button = FindViewById<Button>(Resource.Id.MyButton);
            _listView = FindViewById<ListView>(Resource.Id.ResultsListView);

            _button.Click += delegate { FindStopsNearMe(); };
        }

        private async void FindStopsNearMe()
        {
            if (_client == null)
            {
                SetupNewHttpClient();
            }

            // NOTE: Location is of type OPIA.API.Contracts.OPIAEntities.Response.Resolve.Location
            Location location = await GetLocationAsync();
            if (location != null)
            {
            }
        }

        private void SetupNewHttpClient()
        {
            const string baseUrl = "http://playopia.azurewebsites.net/api/"; // or from configuration file
            _client = new HttpClient { BaseAddress = new Uri(baseUrl) };
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _client.DefaultRequestHeaders.Add("ApiKey", "this_could_be_anything"); // and use SSL!
        }

        private async Task<Location> GetLocationAsync()
        {
            Log.Info("GLA", "Getting a location....");
            var requestEntity = new ResolveRequest
            {
                LookupText = "Sunnybank Hills Shopping Centre",
                LocationType = 0,
                MaxResults = 10,
            };

            // we don't have a PostAsJsonAsync in Android, this is the next best thing... probably a good idea to farm this 
            // out to an extension method, since we'd be using it a LOT.
            Log.Info("GLA", "Serialising request....");
            HttpContent content = new StringContent(JsonConvert.SerializeObject(requestEntity), Encoding.UTF8, "application/json");
            Log.Info("GLA", "Hitting API Proxy with request");
            var response = await _client.PostAsync("location/resolve", content);
            response.EnsureSuccessStatusCode();
            Log.Info("GLA", "Request successful: {0}", response.StatusCode);

            // Ditto on the ReadAsAsync<T> method, we don't have one of those either.
            Log.Info("GLA", "Reading response....");
            string dataString = response.Content.ReadAsStringAsync().Result;
            Log.Info("GLA", "Deserialising response....");
            var result = JsonConvert.DeserializeObject<ResolveResponse>(dataString);

            Log.Info("GLA", "{0} Resolve: Locations Found: {1}", DateTime.Now.ToString("s"), result.Locations.Count());
            Location firstResult = null;
            if (result.Locations.Any())
            {
                firstResult = result.Locations.First(l => l.Id.Contains("Shoppingtown"));               
                Log.Info("GLA", "Found: {0}", firstResult.Id);
            }
            return firstResult;
        }
    }
}

