using System;
using System.Collections.Generic;

namespace BDAS2_SemPrace.Models
{
    public partial class Pulty
    {
        public Pulty()
        {
            ZboziIdZbozis = new HashSet<Zbozi>();
        }

        public decimal CisloPultu { get; set; }
        public decimal IdSupermarket { get; set; }
        public string Nazev { get; set; }

        public virtual Supermarkety IdSupermarketNavigation { get; set; }
        public virtual NazvyPultu NazevNavigation { get; set; }

        public virtual ICollection<Zbozi> ZboziIdZbozis { get; set; }
    }
}
