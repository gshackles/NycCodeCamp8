using Android.Content;
using Android.Widget;
using CodeCamp.Core.Messaging;

namespace CodeCamp.App.Droid
{
    public class ErrorReporter : IErrorReporter
    {
        private readonly Context _context;

        public ErrorReporter(Context context)
        {
            _context = context;
        }

        public void ReportError(string message)
        {
            Toast.MakeText(_context, message, ToastLength.Short);
        }
    }
}