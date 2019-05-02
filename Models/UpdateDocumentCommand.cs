using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
   public class UpdateDocumentCommand : Document
    {
        public bool OnlyStatus { get; set; }
    }
}
