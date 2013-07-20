using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cirrious.MvvmCross.Plugins.Json;
using CodeCamp.Core.Data.Entities;
using CodeCamp.Core.Services;
using CodeCamp.Core.Tests.Mocks;
using NUnit.Framework;

namespace CodeCamp.Core.Tests.ServiceTests
{
    [TestFixture]
    public class CodeCampServiceTests
    {
        private MvxJsonConverter _jsonConverter;
        private MockCampDataClient _mockDataClient;
        private InMemoryFileManager _fileManager;

        [SetUp]
        public void SetUp()
        {
            _jsonConverter = new MvxJsonConverter();
            _mockDataClient = new MockCampDataClient();
            _fileManager = new InMemoryFileManager();
        }

        [Test]
        public async void GetData_FirstCall_FetchesData()
        {
            var campData = new CampData
                               {
                                   Sessions = new List<Session> { new Session { Title = "Best Session Ever", Id = 42 } }
                               };
            bool clientCalled = false;
            _mockDataClient.GetDataBody = () =>
                                              {
                                                  clientCalled = true;
                                                  return Task.FromResult(campData);
                                              };

            var service = new CodeCampService(_fileManager, _jsonConverter, _mockDataClient);

            var data = await service.GetData();

            Assert.True(clientCalled);
            Assert.AreEqual(campData, data);
        }

        [Test]
        public async void GetData_CalledTwice_UsesCacheSecondTime()
        {
            var campData = new CampData
            {
                Sessions = new List<Session> { new Session { Title = "Best Session Ever", Id = 42 } }
            };
            bool clientCalled = false;
            _mockDataClient.GetDataBody = () =>
            {
                clientCalled = true;
                return Task.FromResult(campData);
            };

            var service = new CodeCampService(_fileManager, _jsonConverter, _mockDataClient);

            var data = await service.GetData();

            Assert.True(clientCalled);
            Assert.AreEqual(campData, data);

            _mockDataClient.GetDataBody = delegate { throw new Exception(); };

            data = await service.GetData();

            Assert.AreEqual(campData, data);
        }

        [Test]
        public async void ListSessions_FetchesDataAndReturnsSessions()
        {
            var campData = new CampData
            {
                Sessions = new List<Session> { new Session { Title = "Best Session Ever", Id = 42 } }
            };
            _mockDataClient.GetDataBody = () => Task.FromResult(campData);

            var service = new CodeCampService(_fileManager, _jsonConverter, _mockDataClient);
            var sessions = await service.ListSessions();

            Assert.AreEqual(campData.Sessions, sessions);
        }

        [Test]
        public async void ListSessionsBySpeaker_ValidSpeakerId_ReturnsSessionsForSpeaker()
        {
            int speakerId = 1;
            var correctSession = new Session {Id = 2, SpeakerId = speakerId};
            var wrongSession = new Session {Id = 3, SpeakerId = speakerId + 1};
            var campData = new CampData {Sessions = new List<Session> {correctSession, wrongSession}};
            _mockDataClient.GetDataBody = () => Task.FromResult(campData);

            var service = new CodeCampService(_fileManager, _jsonConverter, _mockDataClient);

            var sessions = await service.ListSessionsBySpeaker(speakerId);

            Assert.AreEqual(1, sessions.Count);
            Assert.AreEqual(correctSession, sessions.First());
        }

        [Test]
        public async void GetSession_ValidSessionId_ReturnsSession()
        {
            var awesomeSession = new Session {Title = "Best Session Ever", Id = 42};
            var campData = new CampData
            {
                Sessions = new List<Session> { awesomeSession }
            };
            _mockDataClient.GetDataBody = () => Task.FromResult(campData);

            var service = new CodeCampService(_fileManager, _jsonConverter, _mockDataClient);

            var session = await service.GetSession(awesomeSession.Id);

            Assert.AreEqual(awesomeSession, session);
        }

        [Test]
        public async void ListSpeakers_FetchesDataAndReturnsSpeakers()
        {
            var campData = new CampData
            {
                Speakers = new List<Speaker> { new Speaker { Id = 42 } }
            };
            _mockDataClient.GetDataBody = () => Task.FromResult(campData);

            var service = new CodeCampService(_fileManager, _jsonConverter, _mockDataClient);
            var speakers = await service.ListSpeakers();

            Assert.AreEqual(campData.Speakers, speakers);
        }

        [Test]
        public async void GetSpeaker_ValidSpeakerId_ReturnsSpeaker()
        {
            var awesomeSpeaker = new Speaker { Id = 42 };
            var campData = new CampData
            {
                Speakers = new List<Speaker> { awesomeSpeaker }
            };
            _mockDataClient.GetDataBody = () => Task.FromResult(campData);

            var service = new CodeCampService(_fileManager, _jsonConverter, _mockDataClient);

            var speaker = await service.GetSpeaker(awesomeSpeaker.Id);

            Assert.AreEqual(awesomeSpeaker, speaker);
        }

        [Test]
        public async void ListSponsors_FetchesDataAndReturnsSponsors()
        {
            var campData = new CampData
            {
                Sponsors = new List<Sponsor> { new Sponsor { Id = 42 } }
            };
            _mockDataClient.GetDataBody = () => Task.FromResult(campData);

            var service = new CodeCampService(_fileManager, _jsonConverter, _mockDataClient);
            var sponsors = await service.ListSponsors();

            Assert.AreEqual(campData.Sponsors, sponsors);
        }

        [Test]
        public async void GetSponsor_ValidSponsorId_ReturnsSponsor()
        {
            var awesomeSponsor = new Sponsor {Id = 42};
            var campData = new CampData
            {
                Sponsors = new List<Sponsor> { awesomeSponsor }
            };
            _mockDataClient.GetDataBody = () => Task.FromResult(campData);

            var service = new CodeCampService(_fileManager, _jsonConverter, _mockDataClient);

            var sponsor = await service.GetSponsor(awesomeSponsor.Id);

            Assert.AreEqual(awesomeSponsor, sponsor);
        }
    }
}
