using System;
using Android.App;
using CodeCamp.Core.ViewModels;
using LegacyBar.Library.BarActions;

namespace CodeCamp.App.Droid.Views
{
    [Activity(Label = "Speaker")]
    public class SpeakerView : ActivityBase
    {
        protected override void OnViewModelSet()
        {
            base.OnViewModelSet();

            SetContentView(Resource.Layout.Speaker);

            LegacyBar.AddAction(new ActionLegacyBarAction(this, () => ViewModel.EmailSpeakerCommand.Execute(null),
                                                          Resource.Drawable.Email));
        }

        private new SpeakerViewModel ViewModel
        {
            get { return (SpeakerViewModel) base.ViewModel; }
        }
    }
}