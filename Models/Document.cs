using Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    public class Document
    {
        public int? Id { get; set; }
        public string Type { get; set; }

        //?da li mo ovo uopste treba
        [NotMapped]
        public IDictionary<int, string> CorrelatedDocs { get; set; }
        public FileWrapper File { get; set; } = new FileWrapper();
        public InputOperations? InputOperation { get; set; }
        public OutputOperations? OutputOperation { get; set; }

        public STATE State { get; set; }
    }

    public enum STATE
    {
        CREATED,
        CHANGED
    }


}
