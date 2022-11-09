using System;
using System.Collections.Generic;

namespace BDAS2_SemPrace.Models
{
    public partial class Znacky
    {
        public Znacky()
        {
            Zbozi = new HashSet<Zbozi>();
        }

        public short IdZnacka { get; set; }
        public string Nazev { get; set; }

        public virtual ICollection<Zbozi> Zbozi { get; set; }
    }
}
