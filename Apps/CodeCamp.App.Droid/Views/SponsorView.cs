using System;
using Android.App;
using CodeCamp.Core.ViewModels;
using LegacyBar.Library.BarActions;

namespace CodeCamp.App.Droid.Views
{
    [Activity(Label = "Sponsor")]
    public class SponsorView : ActivityBase
    {
        protected override void OnViewModelSet()
        {
            base.OnViewModelSet();

            SetContentView(Resource.Layout.Sponsor);

            LegacyBar.AddAction(new ActionLegacyBarAction(this, () => ViewModel.ViewWebsiteCommand.Execute(null),
                                                          Resource.Drawable.Link));
        }

        private new SponsorViewModel ViewModel
        {
            get { return (SponsorViewModel) base.ViewModel; }
        }
    }
}