using Cirrious.CrossCore;
using Cirrious.MvvmCross.Touch.Platform;
using Cirrious.MvvmCross.Touch.Views.Presenters;
using Cirrious.MvvmCross.ViewModels;
using CodeCamp.Core.Messaging;

namespace CodeCamp.App.iOS
{
    public class Setup : MvxTouchSetup
    {
        public Setup(MvxApplicationDelegate appDelegate, IMvxTouchViewPresenter presenter)
            : base(appDelegate, presenter)
        {
        }

        protected override IMvxApplication CreateApp()
        {
            return new CodeCamp.Core.App();
        }

        protected override void InitializeIoC()
        {
            base.InitializeIoC();

            Mvx.ConstructAndRegisterSingleton<IErrorReporter, ErrorReporter>();
        }
    }
}