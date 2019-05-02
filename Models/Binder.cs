using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class Binder
    {
        public string InititatorType { get; set; } = "";
        public Dictionary<string, STATE> Outputs { get; set; } = new Dictionary<string, STATE>();
    }
}
