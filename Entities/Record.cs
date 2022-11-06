using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MediaManager.Entities
{
    public class Record : IID
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public int ReleaseYear { get; set; }

        [Required]
        public Guid BandId { get; set; }
        
        public Band Band { get; set; }

        public List<Track> Tracks { get; set; } = new();
    }
}