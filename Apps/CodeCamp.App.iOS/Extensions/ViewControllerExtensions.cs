using System;
using MonoTouch.UIKit;
using Cirrious.MvvmCross.Binding.BindingContext;
using CodeCamp.Core.ViewModels;

namespace CodeCamp.App.iOS.Extensions
{
    public static class ViewControllerExtensions
    {
        public static void OnViewWillAppear<TController> (this TController controller, object viewModel, Action loadedCallback)
            where TController : UIViewController, IMvxBindingContextOwner
        {
            var appViewModel = (ViewModelBase)viewModel;

            if (!appViewModel.IsLoading) 
            {
                controller.InvokeOnMainThread(() => loadedCallback());
            } 
            else 
            {
                appViewModel.BindLoadingMessage (controller.View, model => model.IsLoading, "Loading...");
                appViewModel.LoadingComplete += (s, e) => controller.InvokeOnMainThread(() => loadedCallback());
            }
        }
    }
}