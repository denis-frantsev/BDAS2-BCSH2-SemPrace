using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace BDAS2_SemPrace.Models
{
    public partial class LogTable
    {
        public int IdZaznam { get; set; }
        public string Tabulka { get; set; }
        public string Operace { get; set; }

        [Display(Name = "Čas")]
        public DateTime? Cas { get; set; }
    }
}
