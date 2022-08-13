using System;
using System.ComponentModel.DataAnnotations;

namespace MediaManager.Entities
{
    public class Game : IID
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public Guid PlatformId { get; set; }
        public Platform Platform { get; set; }
    }
}