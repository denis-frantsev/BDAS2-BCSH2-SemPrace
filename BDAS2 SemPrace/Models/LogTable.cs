using System;
using System.Collections.Generic;

namespace BDAS2_SemPrace.Models
{
    public partial class LogTable
    {
        public int IdZaznam { get; set; }
        public string Tabulka { get; set; }
        public string Operace { get; set; }
        public DateTime Cas { get; set; }
    }
}
