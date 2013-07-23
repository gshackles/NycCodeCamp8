using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cirrious.MvvmCross.Platform;
using CodeCamp.Core.Data.Entities;
using CodeCamp.Core.Messaging.Messages;
using CodeCamp.Core.ViewModels;
using NUnit.Framework;

namespace CodeCamp.Core.Tests.ViewModelTests
{
    public class SpeakerViewModelTests : ViewModelTestsBase
    {
        [Test]
        public async void Init_DataLoadsSuccessfully_LoadsSpeakerAndSessions()
        {
            var speaker = new Speaker {Id = 314};
            var session = new Session { Id = 42, SpeakerId = speaker.Id };
            var data = new CampData {Sessions = new List<Session> {session}, Speakers = new List<Speaker> {speaker}};
            DataClient.GetDataBody = () => Task.FromResult(data);
            var viewModel = new SpeakerViewModel(Messenger, CodeCampService, ComposeEmailTask);

            Assert.True(viewModel.IsLoading);

            await viewModel.Init(new SpeakerViewModel.NavigationParameters(speaker.Id));

            Assert.False(viewModel.IsLoading);
            Assert.AreEqual(speaker, viewModel.Speaker);
            Assert.AreEqual(1, viewModel.Sessions.Count);
            Assert.AreEqual(session, viewModel.Sessions.First());
        }

        [Test]
        public async void Init_ExceptionThrown_ReportsError()
        {
            DataClient.GetDataBody = delegate { throw new Exception(); };
            string errorMessage = null;
            Messenger.Subscribe<ErrorMessage>(msg => errorMessage = msg.Message);

            var viewModel = new SpeakerViewModel(Messenger, CodeCampService, ComposeEmailTask);

            await viewModel.Init(new SpeakerViewModel.NavigationParameters(42));

            Assert.NotNull(errorMessage);
            Assert.False(viewModel.IsLoading);
            Assert.Null(viewModel.Speaker);
            Assert.Null(viewModel.Sessions);
        }

        [Test]
        public void ViewSessionCommand_Executed_NavigatesToSession()
        {
            var session = new Session { Id = 42 };
            var viewModel = new SpeakerViewModel(Messenger, CodeCampService, ComposeEmailTask);

            viewModel.ViewSessionCommand.Execute(session);

            Assert.AreEqual(1, Dispatcher.ShowViewModelRequests.Count);

            var request = Dispatcher.ShowViewModelRequests.Single();
            var navParameters = request.ParameterValues.Read(typeof(SessionViewModel.NavigationParameters)) as SessionViewModel.NavigationParameters;
            Assert.NotNull(navParameters);
            Assert.AreEqual(session.Id, navParameters.Id);
        }

        [Test]
        public async void EmailSpeakerCommand_Executed_StartsComposingEmailToSpeaker()
        {
            var speaker = new Speaker { Id = 314, EmailAddress = "code@camp.com" };
            var data = new CampData { Speakers = new List<Speaker> { speaker } };
            DataClient.GetDataBody = () => Task.FromResult(data);
            var viewModel = new SpeakerViewModel(Messenger, CodeCampService, ComposeEmailTask);
            await viewModel.Init(new SpeakerViewModel.NavigationParameters(speaker.Id));

            viewModel.EmailSpeakerCommand.Execute(null);

            Assert.AreEqual(1, ComposeEmailTask.ComposedEmailAddresses.Count);
            Assert.AreEqual(speaker.EmailAddress, ComposeEmailTask.ComposedEmailAddresses.First());
        }
    }
}