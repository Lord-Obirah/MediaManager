using System;
using System.ComponentModel.DataAnnotations;

namespace MediaManager.Entities
{
    public class Track : IID
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        public Guid RecordId { get; set; }
    }
}