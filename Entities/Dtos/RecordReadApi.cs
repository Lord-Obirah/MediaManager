using System;
using System.Collections.Generic;

namespace MediaManager.Entities
{
    public class RecordReadApi
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public Guid BandId { get; set; }
        public string BandName { get; set; }
        public List<Track> Tracks{ get; set; }
    }
}