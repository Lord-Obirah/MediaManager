using System;
using System.ComponentModel.DataAnnotations;

namespace MediaManager.Entities
{
    public abstract class AbstractFskRating
    {
        [Required]
        public Guid FskRatingId { get; set; }

        public FskRating FskRating { get; set; }
    }
}