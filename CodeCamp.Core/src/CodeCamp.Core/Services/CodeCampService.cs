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
        Task<IList<SponsorTier>> ListSponsors();
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

                foreach (var session in data.Sessions)
                {
                    var speaker = data.Speakers.FirstOrDefault(s => s.Id == session.SpeakerId);

                    // shouldn't happen, but just in case...
                    if (speaker == null)
                        continue;

                    session.SpeakerName = speaker.Name;
                }

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
                                   Sessions = slot.OrderBy(session => session.Title).ToList()
                               }).ToList();
        }

        public async Task<IList<Session>> ListSessionsBySpeaker(int speakerId)
        {
            return (await GetData()).Sessions
                                    .Where(session => session.SpeakerId == speakerId)
                                    .OrderBy(session => session.Title)
                                    .ToList();
        }

        public async Task<Session> GetSession(int sessionId)
        {
            return (await GetData()).Sessions.First(session => session.Id == sessionId);
        }

        public async Task<IList<Speaker>> ListSpeakers()
        {
            return (await GetData()).Speakers.OrderBy(speaker => speaker.Name).ToList();
        }

        public async Task<Speaker> GetSpeaker(int speakerId)
        {
            return (await GetData()).Speakers.First(speaker => speaker.Id == speakerId);
        }

        static IList<string> SponsorTierSortOrder = new List<string> { "Marquee", "Platinum", "Gold", "Silver" }; 

        public async Task<IList<SponsorTier>> ListSponsors()
        {
            return (from sponsor in (await GetData()).Sponsors
                    group sponsor by sponsor.Tier
                    into tier
                    orderby SponsorTierSortOrder.Contains(tier.Key) ? SponsorTierSortOrder.IndexOf(tier.Key) : 99
                    select new SponsorTier
                               {
                                   Name = tier.Key,
                                   Sponsors = tier.OrderBy(sponsor => sponsor.Name).ToList()
                               }).ToList();
        }

        public async Task<Sponsor> GetSponsor(int sponsorId)
        {
            return (await GetData()).Sponsors.First(sponsor => sponsor.Id == sponsorId);
        }

        internal async Task<CampData> GetData()
        {
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
