using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MediaManager.Entities
{
    public class MediaType : IID
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}