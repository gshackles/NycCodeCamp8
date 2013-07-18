using Cirrious.CrossCore;
using Cirrious.CrossCore.IoC;
using Cirrious.MvvmCross.Plugins.Messenger;
using Cirrious.MvvmCross.ViewModels;
using CodeCamp.Core.Messaging;
using CodeCamp.Core.Messaging.Messages;
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

            var messenger = Mvx.Resolve<IMvxMessenger>();
            var errorReporter = Mvx.Resolve<IErrorReporter>();

            _errorToken = messenger.Subscribe<ErrorMessage>(error => errorReporter.ReportError(error.Message));

            RegisterAppStart<SessionsViewModel>();
        }
    }
}
