using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UpravljanjeDokumentacijomWebApp.Models
{
    public class OperationViewModel
    {
        public List<DocumentViewModel> InputReceive { get; set; }
        public List<DocumentViewModel> InputByRequest { get; set; }
        public List<DocumentViewModel> Output { get; set; }
    }
}
