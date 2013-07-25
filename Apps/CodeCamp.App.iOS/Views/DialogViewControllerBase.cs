using Cirrious.MvvmCross.Dialog.Touch;
using MonoTouch.UIKit;
using CodeCamp.App.iOS.Extensions;

namespace CodeCamp.App.iOS.Views
{
    public abstract class DialogViewControllerBase : MvxDialogViewController
    {
        protected DialogViewControllerBase(UITableViewStyle style)
            : base(style, pushing: true)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            TableView.TableFooterView = GetFooterView();
            TableView.BackgroundView = null;
            TableView.BackgroundColor = BackgroundColor;
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

        protected virtual UIColor BackgroundColor
        {
            get { return AppStyles.DefaultBackgroundImage; }
        }
    }   
}