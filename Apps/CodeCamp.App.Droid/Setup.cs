using Android.Content;
using Cirrious.CrossCore;
using Cirrious.MvvmCross.Droid.Platform;
using Cirrious.MvvmCross.Droid.Views;
using Cirrious.MvvmCross.ViewModels;
using CodeCamp.Core.Messaging;

namespace CodeCamp.App.Droid
{
    public class Setup : MvxAndroidSetup
    {
        public Setup(Context applicationContext)
            : base(applicationContext)
        {
        }

        protected override IMvxApplication CreateApp()
        {
            return new Core.App();
        }

        protected override void InitializeIoC()
        {
            base.InitializeIoC();

            Mvx.RegisterSingleton<IErrorReporter>(new ErrorReporter(ApplicationContext));
        }

        protected override IMvxAndroidViewPresenter CreateViewPresenter()
        {
            return new CampViewPresenter();
        }
    }
}