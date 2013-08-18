using Android.Content;
using Cirrious.CrossCore;
using Cirrious.MvvmCross.Droid.Platform;
using Cirrious.MvvmCross.Droid.Views;
using Cirrious.MvvmCross.ViewModels;
using CodeCamp.Core.Converters;
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

        protected override void FillValueConverters(Cirrious.CrossCore.Converters.IMvxValueConverterRegistry registry)
        {
            base.FillValueConverters(registry);

            registry.AddOrOverwrite("SessionDetails", new SessionDetailsConverter());
            registry.AddOrOverwrite("MultiLine", new MultiLineTextValueConverter());
            registry.AddOrOverwrite("StringFormat", new StringFormatValueConverter());
            registry.AddOrOverwrite("Time", new TimeValueConverter());
        }
    }
}