using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MediaManager.Entities
{
    public class RecordWriteApi
    {
        [Required]
        public string Title { get; set; }
        public Guid BandId { get; set; }
        public List<TrackWriteApi> Tracks { get; set; }
    }
}