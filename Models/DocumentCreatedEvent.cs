using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class DocumentCreatedEvent
    {
        public IDictionary<int, string> CorrelatedDocs { get; set; }
        public int? Id { get; set; }
        public string Type { get; set; }
    }
}
