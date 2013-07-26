using Cirrious.CrossCore;
using Cirrious.MvvmCross.Touch.Platform;
using Cirrious.MvvmCross.Touch.Views.Presenters;
using Cirrious.MvvmCross.ViewModels;
using CodeCamp.Core.Messaging;
using CodeCamp.Core.Converters;

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

        protected override void FillValueConverters(Cirrious.CrossCore.Converters.IMvxValueConverterRegistry registry)
        {
            base.FillValueConverters(registry);

            registry.AddOrOverwrite("StringFormat", new StringFormatValueConverter());
            registry.AddOrOverwrite("Time", new TimeValueConverter());
            registry.AddOrOverwrite("SessionDetails", new SessionDetailsConverter());
        }
    }
}