using Cirrious.CrossCore;
using Cirrious.MvvmCross.ViewModels;
using Cirrious.MvvmCross.WindowsPhone.Platform;
using CodeCamp.Core.Messaging;
using Microsoft.Phone.Controls;

namespace CodeCamp.App.WindowsPhone
{
    class Setup : MvxPhoneSetup
    {
        public Setup(PhoneApplicationFrame rootFrame) : base(rootFrame)
        {
        }

        protected override void InitializeIoC()
        {
            base.InitializeIoC();

            Mvx.RegisterSingleton<IErrorReporter>(new ErrorReporter());
        }

        protected override IMvxApplication CreateApp()
        {
            return new Core.App();
        }
    }
}
