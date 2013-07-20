using System.Drawing;
using System.Linq;
using Cirrious.MvvmCross.Dialog.Touch;
using MonoTouch.UIKit;
using SlidingPanels.Lib;
using SlidingPanels.Lib.PanelContainers;
using CodeCamp.App.iOS.Extensions;

namespace CodeCamp.App.iOS.Views
{
    public abstract class DialogViewControllerBase : MvxDialogViewController
    {
        private UIButton _menuButton;

        protected DialogViewControllerBase(UITableViewStyle style)
            : base(style, pushing: true)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            TableView.TableFooterView = GetFooterView();

            if (NavigationController != null && NavigationController.ViewControllers.Count() == 1)
            {
                _menuButton = new UIButton(new RectangleF(0, 0, 40f, 40f));
                _menuButton.SetBackgroundImage(UIImage.FromFile("menu.png"), UIControlState.Normal);
                _menuButton.TouchUpInside += delegate
                {
                    ((SlidingPanelsNavigationViewController)NavigationController).TogglePanel(PanelType.LeftPanel);
                };

                NavigationItem.LeftBarButtonItem = new UIBarButtonItem(_menuButton);
            }
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear (animated);

            this.OnViewWillAppear(ViewModel, OnLoadingComplete);
        }

        protected virtual void OnLoadingComplete() { }

        protected virtual UIView GetFooterView()
        {
            return new UIView();
        }
    }   
}