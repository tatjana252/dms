using DTO.Models;
using System.Collections.Generic;

namespace DTO.Models
{
    public class OperationDTO
    {
        public IEnumerable<DocumentDTO> Received { get; set; }
        public IEnumerable<DocumentDTO> Requested { get; set; }
        public IEnumerable<DocumentDTO> OutputDocuments { get; set; }

    }
}