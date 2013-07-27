using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodeCamp.Core.Data.Entities;
using CodeCamp.Core.Messaging.Messages;
using CodeCamp.Core.ViewModels;
using NUnit.Framework;

namespace CodeCamp.Core.Tests.ViewModelTests
{
    public class SessionViewModelTests : ViewModelTestsBase
    {
        [Test]
        public async void Init_DataLoadsSuccessfully_LoadsSession()
        {
            var session = new Session { Id = 42, SpeakerId = 1 };
            var data = new CampData
                           {
                               Sessions = new List<Session> {session},
                               Speakers = new List<Speaker> {new Speaker {Id = 1}},
                           };
            DataClient.GetDataBody = () => Task.FromResult(data);
            var viewModel = new SessionViewModel(Messenger, CodeCampService);

            Assert.True(viewModel.IsLoading);

            await viewModel.Init(new SessionViewModel.NavigationParameters(session.Id));

            Assert.False(viewModel.IsLoading);
            Assert.AreEqual(session, viewModel.Session);
        }

        [Test]
        public async void Init_ExceptionThrown_ReportsError()
        {
            DataClient.GetDataBody = delegate { throw new Exception(); };
            string errorMessage = null;
            Messenger.Subscribe<ErrorMessage>(msg => errorMessage = msg.Message);

            var viewModel = new SessionViewModel(Messenger, CodeCampService);

            await viewModel.Init(new SessionViewModel.NavigationParameters(42));

            Assert.NotNull(errorMessage);
            Assert.False(viewModel.IsLoading);
            Assert.Null(viewModel.Session);
        }
    }
}