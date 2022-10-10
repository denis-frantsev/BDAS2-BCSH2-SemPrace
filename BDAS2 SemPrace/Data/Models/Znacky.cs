using System;
using System.Collections.Generic;

namespace BDAS2_SemPrace.Models
{
    public partial class Znacky
    {
        public Znacky()
        {
            Zbozis = new HashSet<Zbozi>();
        }

        public byte IdZnacka { get; set; }
        public string Nazev { get; set; }

        public virtual ICollection<Zbozi> Zbozis { get; set; }
    }
}
