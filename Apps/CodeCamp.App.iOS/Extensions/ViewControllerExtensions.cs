using System;
using System.Drawing;
using System.Linq;
using Cirrious.MvvmCross.Binding.BindingContext;
using CodeCamp.Core.ViewModels;
using MonoTouch.UIKit;
using SlidingPanels.Lib;
using SlidingPanels.Lib.PanelContainers;

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

            if (controller.NavigationController != null && controller.NavigationController.ViewControllers.Count() == 1 && controller.NavigationItem.LeftBarButtonItem == null)
            {
                var menuButton = new UIButton(new RectangleF(0, 0, 40f, 40f));
                menuButton.SetBackgroundImage(UIImage.FromFile("menu.png"), UIControlState.Normal);
                menuButton.TouchUpInside += delegate
                {
                    ((SlidingPanelsNavigationViewController)controller.NavigationController).TogglePanel(PanelType.LeftPanel);
                };

                controller.NavigationItem.LeftBarButtonItem = new UIBarButtonItem(menuButton);
            }
        }
    }
}