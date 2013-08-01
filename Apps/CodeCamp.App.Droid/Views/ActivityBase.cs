using System;
using Cirrious.MvvmCross.Droid.Views;
using CodeCamp.App.Droid.Extensions;
using CodeCamp.Core.ViewModels;

namespace CodeCamp.App.Droid.Views
{
    public abstract class ActivityBase : MvxActivity
    {
        protected override void OnViewModelSet()
        {
            base.OnViewModelSet();

            ((ViewModelBase) ViewModel).BindLoadingMessage(this, vm => vm.IsLoading, "Loading...");
        }
    }
}