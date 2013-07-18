namespace CodeCamp.Core.Messaging
{
    public interface IErrorReporter
    {
        void ReportError(string message);
    }
}
