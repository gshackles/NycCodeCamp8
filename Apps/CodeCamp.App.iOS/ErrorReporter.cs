using BigTed;
using CodeCamp.Core.Messaging;
using MonoTouch.UIKit;

namespace CodeCamp.App.iOS
{
    public class ErrorReporter : IErrorReporter
    {
        public void ReportError(string message)
        {
            UIApplication.SharedApplication.InvokeOnMainThread(
                () => BTProgressHUD.Shared.ShowToast(message, timeoutMs: 1500));
        }
    }
}