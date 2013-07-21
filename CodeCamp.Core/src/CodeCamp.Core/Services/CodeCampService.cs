using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cirrious.CrossCore.Platform;
using CodeCamp.Core.Data;
using CodeCamp.Core.Data.Entities;
using CodeCamp.Core.Network;

namespace CodeCamp.Core.Services
{
    public interface ICodeCampService
    {
        Task<bool> RefreshData();
        Task<IList<TimeSlot>> ListSessions();
        Task<IList<Session>> ListSessionsBySpeaker(int speakerId); 
        Task<Session> GetSession(int sessionId);
        Task<IList<Speaker>> ListSpeakers();
        Task<Speaker> GetSpeaker(int speakerId);
        Task<IList<Sponsor>> ListSponsors();
        Task<Sponsor> GetSponsor(int sponsorId);
    }

    public class CodeCampService : ICodeCampService
    {
        private const string DataFileName = "CodeCampData.json";
        private readonly IFileManager _fileManager;
        private readonly IMvxJsonConverter _jsonConverter;
        private readonly ICampDataClient _client;
        private CampData _campData;

        public CodeCampService(IFileManager fileManager, IMvxJsonConverter jsonConverter, ICampDataClient client)
        {
            _fileManager = fileManager;
            _jsonConverter = jsonConverter;
            _client = client;
        }

        public async Task<bool> RefreshData()
        {
            try
            {
                var data = await _client.GetData();

                _fileManager.WriteFile(DataFileName, _jsonConverter.SerializeObject(data));

                _campData = data;

                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<IList<TimeSlot>> ListSessions()
        {
            return (from session in (await GetData()).Sessions
                    group session by new {session.StartTime, session.EndTime}
                    into slot
                    select new TimeSlot
                               {
                                   StartTime = slot.Key.StartTime,
                                   EndTime = slot.Key.EndTime,
                                   Sessions = slot.ToList()
                               }).ToList();
        }

        public async Task<IList<Session>> ListSessionsBySpeaker(int speakerId)
        {
            return (await GetData()).Sessions
                                    .Where(session => session.SpeakerId == speakerId)
                                    .ToList();
        }

        public async Task<Session> GetSession(int sessionId)
        {
            return (await GetData()).Sessions.First(session => session.Id == sessionId);
        }

        public async Task<IList<Speaker>> ListSpeakers()
        {
            return (await GetData()).Speakers;
        }

        public async Task<Speaker> GetSpeaker(int speakerId)
        {
            return (await GetData()).Speakers.First(speaker => speaker.Id == speakerId);
        }

        public async Task<IList<Sponsor>> ListSponsors()
        {
            return (await GetData()).Sponsors;
        }

        public async Task<Sponsor> GetSponsor(int sponsorId)
        {
            return (await GetData()).Sponsors.First(sponsor => sponsor.Id == sponsorId);
        }

        internal async Task<CampData> GetData()
        {
            return new CampData
            {
                Sessions = new List<Session>
                {
                    new Session
                    {
                        StartTime = DateTime.Now,
                        EndTime = DateTime.Now,
                        Id = 24,
                        Title = "Test Session 2",
                        SpeakerName = "Not Greg Shackles",
                        SpeakerId = 4

                    },
                    new Session
                    {
                        Id = 42,
                        Title = "Test Session",
                        SpeakerName = "Greg Shackles",
                        StartTime = DateTime.UtcNow,
                        EndTime = DateTime.UtcNow,
                        SpeakerId = 3
                    }
                },
                Speakers = new List<Speaker>
                {
                    new Speaker
                    {
                        Name = "Greg Shackles",
                        Id = 3, 
                        EmailAddress = "greg@gregshackles.com",
                        Bio = "This is a bio"
                    },
                    new Speaker
                    {
                        Name = "Not Greg Shackles",
                        Id = 4,
                        EmailAddress = "notgreg@gregshackles.com",
                        Bio = "This is another bio"
                    }
                },
                Sponsors = new List<Sponsor>
                {
                    new Sponsor
                    {
                        Name = "OLO",
                        Tier = "Gold",
                        Website = "http://olo.com",
                        Description = "This is a description"
                    },
                    new Sponsor
                    {
                        Name = "Google",
                        Tier = "Silver",
                        Website = "http://google.com",
                        Description = "It's Google. Enough Said."
                    }
                }
            };

            // TODO: lock around the body of this method?
            _campData = _campData ?? LoadCachedCampData();

            if (_campData != null)
                return _campData;

            if (await RefreshData())
                return _campData;

            throw new ApplicationException("There was a problem refreshing the data");
        }

        private CampData LoadCachedCampData()
        {
            if (!_fileManager.FileExists(DataFileName))
                return null;

            var campJson = _fileManager.ReadFile(DataFileName);

            if (string.IsNullOrEmpty(campJson))
                return null;

            try
            {
                return _jsonConverter.DeserializeObject<CampData>(campJson);
            }
            catch
            {
                // if something went wrong deserializing, just nuke the cached version
                _fileManager.DeleteFile(DataFileName);

                return null;
            }
        }
    }
}
