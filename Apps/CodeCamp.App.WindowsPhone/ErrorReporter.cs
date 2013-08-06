using System.Windows;
using CodeCamp.Core.Messaging;

namespace CodeCamp.App.WindowsPhone
{
    public class ErrorReporter : IErrorReporter
    {
        public void ReportError(string message)
        {
            MessageBox.Show(message);
        }
    }
}
