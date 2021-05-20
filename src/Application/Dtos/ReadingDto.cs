using System;
using System.Collections.Generic;

namespace Application.Dtos
{
    public class ReadingDto
    {
        public Dictionary<string, string> Values { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}