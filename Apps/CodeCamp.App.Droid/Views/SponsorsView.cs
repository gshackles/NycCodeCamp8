using System.Windows.Input;
using Android.App;
using Cirrious.MvvmCross.Binding.Droid.BindingContext;
using Cirrious.MvvmCross.Binding.Droid.Views;
using CodeCamp.App.Droid.Extensions;
using CodeCamp.App.Droid.Views.Adapters;
using CodeCamp.Core.ViewModels;

namespace CodeCamp.App.Droid.Views
{
    [Activity(Label = "Sponsors")]
    public class SponsorsView : ActivityBase
    {
        protected override void OnViewModelSet()
        {
            base.OnViewModelSet();

            SetContentView(Resource.Layout.Sponsors);
            var list = FindViewById<MvxListView>(Resource.Id.SponsorList);
            list.Adapter = new SponsorListAdapter(this, (IMvxAndroidBindingContext)BindingContext, ViewModel.ViewSponsorCommand);

            ViewModel.BindLoadingMessage(this, vm => vm.IsRefreshing, "Refreshing...");
        }

        private new SponsorsViewModel ViewModel
        {
            get { return (SponsorsViewModel)base.ViewModel; }
        }

        protected override ICommand RefreshCommand
        {
            get { return ViewModel.RefreshDataCommand; }
        }
    }
}