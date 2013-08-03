using Android.App;
using Cirrious.MvvmCross.Binding.Droid.BindingContext;
using Cirrious.MvvmCross.Binding.Droid.Views;
using CodeCamp.App.Droid.Extensions;
using CodeCamp.App.Droid.Views.Adapters;
using CodeCamp.Core.ViewModels;

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
        }

        private new OverviewViewModel ViewModel
        {
            get { return (OverviewViewModel) base.ViewModel; }
        }

        protected override System.Windows.Input.ICommand RefreshCommand
        {
            get { return ViewModel.RefreshDataCommand; }
        }
    }
}