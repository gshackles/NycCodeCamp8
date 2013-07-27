using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodeCamp.Core.Data.Entities;
using CodeCamp.Core.ViewModels;
using NUnit.Framework;

namespace CodeCamp.Core.Tests.ViewModelTests
{
    public class OverviewViewModelTests : ViewModelTestsBase
    {
        [Test]
        public void ViewFullScheduleCommand_Executed_NavigatesToSessionsView()
        {
            var viewModel = new OverviewViewModel(Messenger, CodeCampService);

            viewModel.ViewFullScheduleCommand.Execute(null);

            Assert.AreEqual(1, Dispatcher.ShowViewModelRequests.Count);
            Assert.AreEqual(typeof(SessionsViewModel), Dispatcher.ShowViewModelRequests.First().ViewModelType);
        }

        [Test]
        public async void Init_SomeSessionsHaveEnded_LoadsNextUnfinishedSlots()
        {
            var speaker = new Speaker {Id = 1};
            var pastSession = new Session { StartTime = DateTime.UtcNow.AddHours(-1), EndTime = DateTime.UtcNow.AddHours(-1), Id = 42, SpeakerId = speaker.Id};
            var inProgressSession = new Session { StartTime = DateTime.UtcNow.AddMinutes(-10), EndTime = DateTime.UtcNow.AddHours(1), Id = 24 };
            var data = new CampData
                           {
                               Sessions = new List<Session> {inProgressSession, pastSession},
                               Speakers = new List<Speaker> {speaker}
                           };
            DataClient.GetDataBody = () => Task.FromResult(data);
            var viewModel = new OverviewViewModel(Messenger, CodeCampService);

            await viewModel.Init();

            Assert.AreEqual(1, viewModel.TimeSlots.Count);
            Assert.AreEqual(inProgressSession, viewModel.TimeSlots.First().Sessions.Single());
        }
    }
}