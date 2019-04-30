using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Models
{
    public class FileWrapper
    {
        [NotMapped]
        public byte[] File { get; set; }
        public string Name { get; set; }
    }
}
