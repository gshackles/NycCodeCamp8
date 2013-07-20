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
    public class SpeakersViewModelTests : ViewModelTestsBase
    {
        [Test]
        public async void Init_DataLoadsSuccessfully_LoadsSpeakerList()
        {
            var data = new CampData { Speakers = new List<Speaker>() };
            DataClient.GetDataBody = () => Task.FromResult(data);
            var viewModel = new SpeakersViewModel(Messenger, CodeCampService);

            Assert.True(viewModel.IsLoading);

            await viewModel.Init();

            Assert.AreEqual(data.Speakers, viewModel.Speakers);
            Assert.False(viewModel.IsLoading);
        }

        [Test]
        public async void Init_ExceptionThrown_ReportsError()
        {
            DataClient.GetDataBody = delegate { throw new Exception(); };
            string errorMessage = null;
            Messenger.Subscribe<ErrorMessage>(msg => errorMessage = msg.Message);

            var viewModel = new SpeakersViewModel(Messenger, CodeCampService);
            await viewModel.Init();

            Assert.NotNull(errorMessage);
            Assert.False(viewModel.IsLoading);
            Assert.Null(viewModel.Speakers);
        }

        [Test]
        public void ViewSpeakerCommand_NavigatesToSpeaker()
        {
            var speaker = new Speaker { Id = 42 };
            var viewModel = new SpeakersViewModel(Messenger, CodeCampService);

            viewModel.ViewSpeakerCommand.Execute(speaker);

            Assert.AreEqual(1, Dispatcher.ShowViewModelRequests.Count);

            var request = Dispatcher.ShowViewModelRequests.Single();
            var navParameters = request.ParameterValues.Read(typeof(SpeakerViewModel.NavigationParameters)) as SpeakerViewModel.NavigationParameters;
            Assert.NotNull(navParameters);
            Assert.AreEqual(speaker.Id, navParameters.Id);
        }
    }
}