using System.Collections.Generic;
using Cirrious.MvvmCross.Plugins.WebBrowser;

namespace CodeCamp.Core.Tests.Mocks
{
    public class MockWebBrowserTask : IMvxWebBrowserTask
    {
        public IList<string> UrlRequests { get; set; }

        public MockWebBrowserTask()
        {
            UrlRequests = new List<string>();
        }

        public void ShowWebPage(string url)
        {
            UrlRequests.Add(url);
        }
    }
}