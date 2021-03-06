using Cirrious.CrossCore;
using Cirrious.MvvmCross.Touch.Platform;
using Cirrious.MvvmCross.ViewModels;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace CodeCamp.App.iOS
{
    [Register ("AppDelegate")]
    public partial class AppDelegate : MvxApplicationDelegate
    {
        private UIWindow _window;

        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            _window = new UIWindow(UIScreen.MainScreen.Bounds);
			
            var presenter = new CampViewPresenter(this, _window);
            var setup = new Setup(this, presenter);
            setup.Initialize();

            var startup = Mvx.Resolve<IMvxAppStart>();
            startup.Start();

            _window.MakeKeyAndVisible();

            //UINavigationBar.Appearance.TintColor = AppStyles.NavigationBarTintColor;
            UINavigationBar.Appearance.BackgroundColor = UIColor.Black;
            UINavigationBar.Appearance.TintColor = UIColor.Clear;

            return true;
        }
    }
}