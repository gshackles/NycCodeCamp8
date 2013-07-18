using Cirrious.MvvmCross.Plugins.Messenger;

namespace CodeCamp.Core.Messaging.Messages
{
    public class ErrorMessage : MvxMessage
    {
        public string Message { get; private set; }

        public ErrorMessage(object sender, string message)
            : base(sender)
        {
            Message = message;
        }
    }
}
