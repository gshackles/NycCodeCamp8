using System.Linq;
using CodeCamp.Core.ViewModels;
using NUnit.Framework;

namespace CodeCamp.Core.Tests.ViewModelTests
{
    public class MenuViewModelTests : ViewModelTestsBase
    {
        [Test]
        public void ShowSessionsCommand_Executed_ClearsAndNavigatesToSessions()
        {
            var viewModel = new MenuViewModel(Messenger);

            viewModel.ShowSessionsCommand.Execute(null);

            Assert.AreEqual(1, Dispatcher.ShowViewModelRequests.Count);
            var request = Dispatcher.ShowViewModelRequests.First();
            Assert.AreEqual(request.ViewModelType, typeof(SessionsViewModel));
            Assert.That(request.PresentationValues.ContainsKey(PresentationBundleFlagKeys.ClearStack));
        }

        [Test]
        public void ShowSpeakersCommand_Executed_ClearsAndNavigatesToSpeakers()
        {
            var viewModel = new MenuViewModel(Messenger);

            viewModel.ShowSpeakersCommand.Execute(null);

            Assert.AreEqual(1, Dispatcher.ShowViewModelRequests.Count);
            var request = Dispatcher.ShowViewModelRequests.First();
            Assert.AreEqual(request.ViewModelType, typeof(SpeakersViewModel));
            Assert.That(request.PresentationValues.ContainsKey(PresentationBundleFlagKeys.ClearStack));
        }

        [Test]
        public void ShowMapCommand_Executed_ClearsAndNavigatesToMap()
        {
            var viewModel = new MenuViewModel(Messenger);

            viewModel.ShowMapCommand.Execute(null);

            Assert.AreEqual(1, Dispatcher.ShowViewModelRequests.Count);
            var request = Dispatcher.ShowViewModelRequests.First();
            Assert.AreEqual(request.ViewModelType, typeof(MapViewModel));
            Assert.That(request.PresentationValues.ContainsKey(PresentationBundleFlagKeys.ClearStack));
        }

        [Test]
        public void ShowSponsorsCommand_Executed_ClearsAndNavigatesToSponsors()
        {
            var viewModel = new MenuViewModel(Messenger);

            viewModel.ShowSponsorsCommand.Execute(null);

            Assert.AreEqual(1, Dispatcher.ShowViewModelRequests.Count);
            var request = Dispatcher.ShowViewModelRequests.First();
            Assert.AreEqual(request.ViewModelType, typeof(SponsorsViewModel));
            Assert.That(request.PresentationValues.ContainsKey(PresentationBundleFlagKeys.ClearStack));
        }
    }
}