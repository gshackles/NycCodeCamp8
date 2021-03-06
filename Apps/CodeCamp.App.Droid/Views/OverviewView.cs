using System.Windows.Input;
using Android.App;
using Cirrious.MvvmCross.Binding.Droid.BindingContext;
using Cirrious.MvvmCross.Binding.Droid.Views;
using CodeCamp.App.Droid.Extensions;
using CodeCamp.App.Droid.Views.Adapters;
using CodeCamp.Core.ViewModels;
using LegacyBar.Library.BarActions;

namespace CodeCamp.App.Droid.Views
{
    [Activity(Label = "NYC Code Camp 8", MainLauncher = true)]
    public class OverviewView : ActivityBase
    {
        protected override void OnViewModelSet()
        {
            base.OnViewModelSet();

            SetContentView(Resource.Layout.Overview);
            var list = FindViewById<MvxListView>(Resource.Id.SessionList);
            list.Adapter = new SessionListAdapter(this, (IMvxAndroidBindingContext)BindingContext, ViewModel.ViewSessionCommand);

            ViewModel.BindLoadingMessage(this, vm => vm.IsRefreshing, "Refreshing...");

            LegacyBar.AddAction(new ActionLegacyBarAction(this, () => ViewModel.ViewFullScheduleCommand.Execute(null), Resource.Drawable.Calendar));
        }

        private new OverviewViewModel ViewModel
        {
            get { return (OverviewViewModel) base.ViewModel; }
        }

        protected override ICommand RefreshCommand
        {
            get { return ViewModel.RefreshDataCommand; }
        }
    }
}