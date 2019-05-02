using System;
using System.Collections.Generic;
using System.Text;

namespace UpravljanjeDokumentacijomWebApp.Models
{
    public class BinderViewModel
    {
        public string InititatorType { get; set; } = "";
        public OutputOperationsViewModel Operation { get; set; }
        public List<BindedDocumentVM> Outputs { get; set; } = new List<BindedDocumentVM>();
    }

    public enum STATEVM
    {
        CREATED,
        CHANGED
    }

    public class BindedDocumentVM
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public STATEVM State { get; set; }
    }
}
