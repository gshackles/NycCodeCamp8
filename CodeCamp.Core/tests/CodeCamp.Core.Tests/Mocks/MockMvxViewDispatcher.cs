using System;
using System.Collections.Generic;
using Cirrious.CrossCore.Core;
using Cirrious.MvvmCross.ViewModels;
using Cirrious.MvvmCross.Views;

namespace CodeCamp.Core.Tests.Mocks
{
    public class MockMvxViewDispatcher : MvxMainThreadDispatcher, IMvxViewDispatcher
    {
        public IList<MvxViewModelRequest> ShowViewModelRequests = new List<MvxViewModelRequest>();
        public IList<MvxClosePresentationHint> CloseRequests = new List<MvxClosePresentationHint>();

        public bool ShowViewModel(MvxViewModelRequest request)
        {
            ShowViewModelRequests.Add(request);

            return true;
        }

        public bool ChangePresentation(MvxPresentationHint hint)
        {
            if (hint is MvxClosePresentationHint)
                CloseRequests.Add((MvxClosePresentationHint)hint);

            return true;
        }

        public bool RequestMainThreadAction(Action action)
        {
            action();

            return true;
        }
    }
}
