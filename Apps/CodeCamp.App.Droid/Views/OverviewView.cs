using System;
using Android.App;
using Cirrious.MvvmCross.Binding.Droid.BindingContext;
using Cirrious.MvvmCross.Binding.Droid.Views;
using Cirrious.MvvmCross.Droid.Views;

namespace CodeCamp.App.Droid.Views
{
    [Activity(Label = "NYC Code Camp 8", MainLauncher = true)]
    public class OverviewView : MvxActivity
    {
        protected override void OnViewModelSet()
        {
            base.OnViewModelSet();

            SetContentView(Resource.Layout.Overview);
            var list = FindViewById<MvxListView>(Resource.Id.SessionList);
            list.Adapter = new SessionListAdapter(this, (IMvxAndroidBindingContext)BindingContext);
        }
    }
}