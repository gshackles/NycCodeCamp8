using Android.App;
using CodeCamp.Core.ViewModels;

namespace CodeCamp.App.Droid.Views
{
    [Activity(Label = "Speakers")]
    public class SpeakersView : ActivityBase
    {
        protected override void OnViewModelSet()
        {
            base.OnViewModelSet();

            SetContentView(Resource.Layout.Speakers);
        }

        protected override System.Windows.Input.ICommand RefreshCommand
        {
            get { return ViewModel.RefreshDataCommand; }
        }

        private new SpeakersViewModel ViewModel
        {
            get { return (SpeakersViewModel) base.ViewModel; }
        }
    }
}