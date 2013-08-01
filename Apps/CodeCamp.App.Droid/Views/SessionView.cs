using System;
using Android.App;

namespace CodeCamp.App.Droid.Views
{
    [Activity(Label = "Session")]
    public class SessionView : ActivityBase
    {
        protected override void OnViewModelSet()
        {
            base.OnViewModelSet();

            SetContentView(Resource.Layout.Session);
        }
    }
}