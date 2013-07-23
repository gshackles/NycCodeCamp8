using System.Collections.Generic;
using Cirrious.MvvmCross.Plugins.Email;

namespace CodeCamp.Core.Tests.Mocks
{
    public class MockComposeEmailTask : IMvxComposeEmailTask
    {
        public IList<string> ComposedEmailAddresses { get; set; }

        public MockComposeEmailTask()
        {
            ComposedEmailAddresses = new List<string>();
        }

        public void ComposeEmail(string to, string cc, string subject, string body, bool isHtml)
        {
            ComposedEmailAddresses.Add(to);
        }
    }
}