using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UpravljanjeDokumentacijomWebApp.Models
{
    public class ActivityViewModel
    {
        public string Status { get; set; }
        public Guid CorrelationId { get; set; }
        public IEnumerable<DocumentViewModel> InputDocuments { get; set; }
        public IEnumerable<DocumentViewModel> OutputDocuments { get; set; }
    }
    
    public enum InputOperationsViewModel
    {
        Receive, Request,
    }

    public enum OutputOperationsViewModel
    {
        Create, Send, Update
    }
}
