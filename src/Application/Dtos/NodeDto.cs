using System;
using System.Collections.Generic;

namespace Application.Dtos
{
    public class NodeDto
    {
        public string clientId { get; set; }
        public List<ReadingDto> Readings { get; set; }
        public ReadingDto LatestReading { get; set; }
        public long ReadingsAvailable { get; set; }
        public bool IsConfigured { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}