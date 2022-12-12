using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BDAS2_SemPrace.Models
{
    public partial class Kategorie
    {
        public Kategorie()
        {
            Zbozi = new HashSet<Zbozi>();
        }

        public short IdKategorie { get; set; }

        [Display(Name = "Název kategorie")]
        public string Nazev { get; set; }
        public string Popis { get; set; }

        public virtual ICollection<Zbozi> Zbozi { get; set; }
    }
}
