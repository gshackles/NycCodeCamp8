using Cirrious.CrossCore;
using Cirrious.MvvmCross.Touch.Views;
using Cirrious.MvvmCross.Touch.Views.Presenters;
using Cirrious.MvvmCross.ViewModels;
using CodeCamp.Core.Extensions;
using CodeCamp.Core.ViewModels;
using MonoTouch.UIKit;
using SlidingPanels.Lib;
using SlidingPanels.Lib.PanelContainers;
using CodeCamp.App.iOS.Annotations;

namespace CodeCamp.App.iOS
{
    public class CampViewPresenter : MvxTouchViewPresenter 
    {
        private readonly UIWindow _window;
        private LeftPanelContainer _menuPanelContainer;
        private UIViewController _rootViewController;
        private SlidingPanelsNavigationViewController _slidingPanelsController;
        private IMvxTouchViewCreator _viewCreator;

        protected IMvxTouchViewCreator ViewCreator
        {
            get { return _viewCreator ?? (_viewCreator = Mvx.Resolve<IMvxTouchViewCreator>()); }
        }

        public CampViewPresenter(UIApplicationDelegate applicationDelegate, UIWindow window) :
            base(applicationDelegate, window)
        {
            _window = window;
        }

        protected override void ShowFirstView(UIViewController viewController)
        {
            base.ShowFirstView(viewController);

            _slidingPanelsController = (SlidingPanelsNavigationViewController)MasterNavigationController;

            _rootViewController.AddChildViewController(_slidingPanelsController);
            _rootViewController.View.AddSubview(_slidingPanelsController.View);

            var menuRequest = new MvxViewModelRequest<MenuViewModel>(new MvxBundle(null), null, MvxRequestedBy.UserAction);
            var menuView = (UIViewController)ViewCreator.CreateView(menuRequest);
            _menuPanelContainer = new LeftPanelContainer(menuView) { EdgeTolerance = 100f };
            _slidingPanelsController.InsertPanel(_menuPanelContainer);
        }

        protected override UINavigationController CreateNavigationController(UIViewController viewController)
        {
            var navController = new SlidingPanelsNavigationViewController(viewController);
            navController.CanSwipeToShowPanel =
                _ => !navController.TopViewController.HasAttribute<DisableMenuGestureAttribute>();
            navController.View.Layer.ShadowRadius = 0;

            _rootViewController = new UIViewController();

            return navController;
        }

        protected override void SetWindowRootViewController(UIViewController controller)
        {
            _window.AddSubview(_rootViewController.View);
            _window.RootViewController = _rootViewController;
        }

        public override void Show(MvxViewModelRequest request)
        {
            if (_menuPanelContainer != null && _menuPanelContainer.IsVisible)
                _slidingPanelsController.TogglePanel(PanelType.LeftPanel);

            if (request.PresentationValues != null)
            {
                if (request.PresentationValues.ContainsKey(PresentationBundleFlagKeys.ClearStack))
                {
                    clearStackAndNavigate(request);

                    return;
                }
            }

            base.Show(request);
        }

        private void clearStackAndNavigate(MvxViewModelRequest request)
        {
            var nextViewController = (UIViewController)ViewCreator.CreateView(request);

            if (MasterNavigationController.TopViewController.GetType() != nextViewController.GetType())
            {
                MasterNavigationController.PopToRootViewController(false);
                MasterNavigationController.ViewControllers = new UIViewController[] { nextViewController };
            }
        }
    }
}

