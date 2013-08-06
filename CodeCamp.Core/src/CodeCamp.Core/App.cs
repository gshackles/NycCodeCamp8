using Cirrious.CrossCore;
using Cirrious.CrossCore.IoC;
using Cirrious.CrossCore.Platform;
using Cirrious.MvvmCross.Plugins.Messenger;
using Cirrious.MvvmCross.ViewModels;
using CodeCamp.Core.Data;
using CodeCamp.Core.Messaging;
using CodeCamp.Core.Messaging.Messages;
using CodeCamp.Core.Network;
using CodeCamp.Core.ViewModels;

namespace CodeCamp.Core
{
    public class App : MvxApplication
    {
        private MvxSubscriptionToken _errorToken;

        public override void Initialize()
        {
            base.Initialize();

            CreatableTypes()
                .EndingWith("Service")
                .AsInterfaces()
                .RegisterAsLazySingleton();

            Mvx.RegisterSingleton<IFileManager>(() => new IsolatedStorageFileManager());
            Mvx.RegisterSingleton<ICampDataClient>(() => new CampDataClient(Mvx.Resolve<IMvxJsonConverter>()));

            var messenger = Mvx.Resolve<IMvxMessenger>();
            var errorReporter = Mvx.Resolve<IErrorReporter>();

            _errorToken = messenger.Subscribe<ErrorMessage>(error => errorReporter.ReportError(error.Message));

#if !WINDOWS_PHONE
            RegisterAppStart<OverviewViewModel>();
#else
            RegisterAppStart<CodeCamp.Core.WindowsPhone.ViewModels.PivotViewModel>();
#endif
        }
    }
}
