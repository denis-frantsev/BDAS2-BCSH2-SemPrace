using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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

        [Display(Name = "Částka")]
        public int Castka { get; set; }
        public string Typ { get; set; }

        [Display(Name = "Číslo karty")]
        public long? CisloKarty { get; set; }
        public int? IdZakaznik { get; set; }
        public int IdSupermarket { get; set; }

        [Display(Name = "Pobočka")]
        public virtual Supermarkety IdSupermarketNavigation { get; set; }

        [Display(Name = "Zákazník")]
        public virtual Zakaznici IdZakaznikNavigation { get; set; }
        public virtual ICollection<Prodeje> Prodeje { get; set; }
    }
}
