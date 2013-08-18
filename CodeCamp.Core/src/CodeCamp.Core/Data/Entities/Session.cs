using System;

namespace CodeCamp.Core.Data.Entities
{
    public class Session
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Abstract { get; set; }
        public string RoomName { get; set; }
        public int? SpeakerId { get; set; }
        public string SpeakerName { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}
