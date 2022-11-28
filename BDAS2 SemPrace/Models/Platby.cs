using System;
using System.Collections.Generic;

namespace BDAS2_SemPrace.Models
{
    public partial class Platby
    {
        public Platby()
        {
            Prodeje = new HashSet<Prodeje>();
        }

        public int IdPlatba { get; set; }
        public DateTime Datum { get; set; }
        public decimal Castka { get; set; }
        public string Typ { get; set; }
        public long? CisloKarty { get; set; }
        public int? IdZakaznik { get; set; }
        public decimal IdSupermarket { get; set; }

        public virtual Supermarkety IdSupermarketNavigation { get; set; }
        public virtual Zakaznici IdZakaznikNavigation { get; set; }
        public virtual ICollection<Prodeje> Prodeje { get; set; }
    }
}
