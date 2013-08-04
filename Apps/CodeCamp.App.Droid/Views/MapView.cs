using Android.App;
using Android.Webkit;

namespace CodeCamp.App.Droid.Views
{
    [Activity(Label = "Facility Map")]
    public class MapView : ActivityBase
    {
        protected override void OnViewModelSet()
        {
            base.OnViewModelSet();

            SetContentView(Resource.Layout.Map);

            var map = FindViewById<WebView>(Resource.Id.Map);

            map.LoadUrl("file:///android_asset/Map/map.html");
            map.Settings.SetSupportZoom(true);
            map.Settings.BuiltInZoomControls = true;
            map.Settings.UseWideViewPort = true;
            map.Settings.DefaultZoom = WebSettings.ZoomDensity.Far;
        }

        protected override bool DisableMenuSwipe
        {
            get { return true; }
        }
    }
}