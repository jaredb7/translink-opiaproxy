using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Media;
using Android.Runtime;
using Android.Util;
using Android.Widget;
using Android.OS;
using Newtonsoft.Json;
using OPIA.API.Contracts.OPIAEntities.Request.Location;
using OPIA.API.Contracts.OPIAEntities.Response.Resolve;
using SampleAndroid.ListViewAdapters;
using Encoding = System.Text.Encoding;

namespace SampleAndroid
{
    [Activity(Label = "OpiaProxy Sample", MainLauncher = true, Icon = "@drawable/icon")]
    public class FindMyLocationActivity : Activity
    {
        private HttpClient _client;
        private EditText _edLocation; 
        private Button _buttonFindMyLocation;
        private ListView _listViewResults;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            _edLocation = FindViewById<EditText>(Resource.Id.edLocation);
            _buttonFindMyLocation = FindViewById<Button>(Resource.Id.btnFindMyLocation);
            _listViewResults = FindViewById<ListView>(Resource.Id.lvResults);

            _buttonFindMyLocation.Click += delegate { FindMyLocation(); };
        }

        private async void FindMyLocation()
        {
            if (_client == null)
            {
                SetupNewHttpClient();
            }

            // NOTE: Location is of type OPIA.API.Contracts.OPIAEntities.Response.Resolve.Location
            var locations = await GetLocationAsync(_edLocation.Text);
            if (locations != null && locations.Any())
            {
                var resultsListAdapter = new LocationsListAdapter(this, locations.ToList());
                _listViewResults.Adapter = resultsListAdapter;
            }
            else
            {
                new AlertDialog.Builder(this)
                    .SetTitle("Couldn't find your location")
                    .SetMessage("Please check spelling or try a different location")
                    .SetPositiveButton("OK", (s, e) => _edLocation.RequestFocus())
                    .SetCancelable(true)
                    .Show();
            }
        }

        private void SetupNewHttpClient()
        {
            const string baseUrl = "http://playopia.azurewebsites.net/api/"; // or from configuration file
            _client = new HttpClient { BaseAddress = new Uri(baseUrl) };
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _client.DefaultRequestHeaders.Add("ApiKey", "this_could_be_anything"); // and use SSL!
        }

        private async Task<IEnumerable<Location>> GetLocationAsync(string locationText)
        {
            Log.Info("GLA", "Getting a location....");
            var requestEntity = new ResolveRequest
            {
                LookupText = locationText,
                LocationType = 0,
                MaxResults = 10,
            };

            // we don't have a PostAsJsonAsync in Android, this is the next best thing... probably a good idea to farm this 
            // out to an extension method, since we'd be using it a LOT.
            Log.Info("GLA", "Serialising request....");
            HttpContent content = new StringContent(JsonConvert.SerializeObject(requestEntity), Encoding.UTF8, "application/json");
            Log.Info("GLA", "Hitting OPIA API Proxy with request");
            var response = await _client.PostAsync("location/resolve", content);
            response.EnsureSuccessStatusCode();
            Log.Info("GLA", "Request successful: {0}", response.StatusCode);

            // Ditto on the ReadAsAsync<T> method, we don't have one of those either.
            Log.Info("GLA", "Reading response....");
            string dataString = response.Content.ReadAsStringAsync().Result;
            Log.Info("GLA", "Deserialising response....");
            var result = JsonConvert.DeserializeObject<ResolveResponse>(dataString);

            Log.Info("GLA", "{0} Resolve: Locations Found: {1}", DateTime.Now.ToString("s"), result.Locations.Count());
            return result.Locations;
        }
    }
}

