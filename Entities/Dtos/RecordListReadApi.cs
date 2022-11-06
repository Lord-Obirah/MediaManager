using System;

namespace MediaManager.Entities;

public class RecordListReadApi
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public Guid BandId { get; set; }
    public string BandName { get; set; }

}