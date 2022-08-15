using System;
using System.ComponentModel.DataAnnotations;

namespace MediaManager.Entities
{
    public class Movie : AbstractFskRating, IID
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public Guid MediaTypeId { get; set; }

        public MediaType MediaType { get; set; }
    }
}