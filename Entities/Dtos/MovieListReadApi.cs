using System;

namespace MediaManager.Entities;

public class MovieListReadApi
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string MediaType { get; set; }
}