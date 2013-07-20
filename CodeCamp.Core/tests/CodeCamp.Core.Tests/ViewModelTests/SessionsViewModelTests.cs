﻿using System;
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
    public class SessionsViewModelTests : ViewModelTestsBase
    {
        [Test]
        public async void Init_DataLoadsSuccessfully_LoadsSessionList()
        {
            var data = new CampData { Sessions = new List<Session>()};
            DataClient.GetDataBody = () => Task.FromResult(data);
            var viewModel = new SessionsViewModel(Messenger, CodeCampService);

            Assert.True(viewModel.IsLoading);

            await viewModel.Init();

            Assert.AreEqual(data.Sessions, viewModel.Sessions);
            Assert.False(viewModel.IsLoading);
        }

        [Test]
        public async void Init_ExceptionThrown_ReportsError()
        {
            DataClient.GetDataBody = delegate { throw new Exception(); };
            string errorMessage = null;
            Messenger.Subscribe<ErrorMessage>(msg => errorMessage = msg.Message);

            var viewModel = new SessionsViewModel(Messenger, CodeCampService);
            await viewModel.Init();

            Assert.NotNull(errorMessage);
            Assert.False(viewModel.IsLoading);
            Assert.Null(viewModel.Sessions);
        }

        [Test]
        public void ViewSessionCommand_NavigatesToSession()
        {
            var session = new Session {Id = 42};
            var viewModel = new SessionsViewModel(Messenger, CodeCampService);

            viewModel.ViewSessionCommand.Execute(session);

            Assert.AreEqual(1, Dispatcher.ShowViewModelRequests.Count);

            var request = Dispatcher.ShowViewModelRequests.Single();
            var navParameters = request.ParameterValues.Read(typeof(SessionViewModel.NavigationParameters)) as SessionViewModel.NavigationParameters;
            Assert.NotNull(navParameters);
            Assert.AreEqual(session.Id, navParameters.Id);
        }
    }
}
