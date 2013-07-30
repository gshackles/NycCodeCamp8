using System;
using Android.App;
using Cirrious.MvvmCross.Droid.Views;

namespace CodeCamp.App.Droid.Views
{
    [Activity(Label = "NYC Code Camp 8", MainLauncher = true)]
    public class OverviewView : MvxActivity
    {
        protected override void OnViewModelSet()
        {
            base.OnViewModelSet();

            Console.WriteLine("Overview: view model set");
        }
    }
}