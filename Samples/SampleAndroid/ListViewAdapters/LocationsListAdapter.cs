using System.Collections.Generic;
using System.Linq;
using Android.App;
using Android.Util;
using Android.Views;
using Android.Widget;
using Java.Lang;
using OPIA.API.Contracts.OPIAEntities.Response.Resolve;
using Exception = System.Exception;

namespace SampleAndroid.ListViewAdapters
{
    public class LocationsListAdapter :BaseAdapter
    {
        private readonly Activity _context;
        private readonly Location[] _locations;

        public LocationsListAdapter(Activity context, IEnumerable<Location> locations)
        {
            _context = context;
            _locations = locations.ToArray(); 
        }

        public override Object GetItem(int position)
        {
            return position;
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var location = _locations[position];

            // Try to reuse convertView if it's not null, otherwise inflate it from our item layout
            // This gives us some performance gains by not always inflating a new view, it recycles
            // existing one/s that have cycled off top/bottom of list view.
            var view = (convertView ?? _context.LayoutInflater.Inflate(Resource.Layout.locationView, parent, false)) as LinearLayout;
            try
            {
                var imageItem = view.FindViewById<ImageView>(Resource.Id.imageItem);
                var textTop = view.FindViewById<TextView>(Resource.Id.textTop);
                var textBottom = view.FindViewById<TextView>(Resource.Id.textBottom);
                textTop.Text = string.Format("{0} {1} {2}, {3}", location.StreetNumber, location.StreetName, location.StreetType, location.Suburb);
                textBottom.Text = string.Format("{0} ({1}) - ({2}, {3})", location.Id, location.LocationType, location.Position.Lat, location.Position.Lng);
            }
            catch (Exception ex)
            {
                Log.Error("Location GetView", ex.Message);
                throw;
            }
            return view;
        }

        public override int Count
        {
            get { return _locations.Count(); }
        }
    }
}