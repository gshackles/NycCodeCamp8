using System.Windows.Input;
using Android.App;
using Cirrious.MvvmCross.Binding.Droid.BindingContext;
using Cirrious.MvvmCross.Binding.Droid.Views;
using CodeCamp.App.Droid.Extensions;
using CodeCamp.App.Droid.Views.Adapters;
using CodeCamp.Core.ViewModels;

namespace CodeCamp.App.Droid.Views
{
    [Activity(Label = "Full Schedule")]
    public class SessionsView : ActivityBase
    {
        protected override void OnViewModelSet()
        {
            base.OnViewModelSet();

            SetContentView(Resource.Layout.Sessions);
            var list = FindViewById<MvxListView>(Resource.Id.SessionList);
            list.Adapter = new SessionListAdapter(this, (IMvxAndroidBindingContext)BindingContext, ViewModel.ViewSessionCommand);

            ViewModel.BindLoadingMessage(this, vm => vm.IsRefreshing, "Refreshing...");
        }

        private new SessionsViewModel ViewModel
        {
            get { return (SessionsViewModel)base.ViewModel; }
        }

        protected override ICommand RefreshCommand
        {
            get { return ViewModel.RefreshDataCommand; }
        }
    }
}