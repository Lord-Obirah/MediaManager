using System;

namespace MediaManager.Entities
{
    public class TrackWriteApi
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public Guid RecordId { get; set; }
    }
}