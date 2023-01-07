using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace BDAS2_SemPrace.Models
{
    public partial class Obrazky
    {
        public int IdObrazek { get; set; }
        public byte[] Data { get; set; }
        public string Popis { get; set; }
        public string Nazev { get; set; }

        [Display(Name = "Přípona")]
        public string Pripona { get; set; }

        public virtual ICollection<User> Users { get; set; }

    }
}
