using System;
using System.Collections.Generic;

namespace BDAS2_SemPrace.Models
{
    public partial class Kategorie
    {
        public Kategorie()
        {
            Zbozis = new HashSet<Zbozi>();
        }

        public short IdKategorie { get; set; }
        public string Nazev { get; set; }
        public string Popis { get; set; }

        public virtual ICollection<Zbozi> Zbozis { get; set; }
    }
}
