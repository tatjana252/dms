using System;
using System.Collections.Generic;


namespace DTO.Models
{

    public class ActivityDTO
    {
        public string Status { get; set; }
        public Guid CorrelationId { get; set; }
        public IEnumerable<DocumentDTO> InputDocuments { get; set; }
        public IEnumerable<DocumentDTO> OutputDocuments { get; set; }
    }

}
