using System;

namespace MediaManager.Entities
{
    public class MovieReadApi
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public Guid MediaTypeId { get; set; }
    }
}