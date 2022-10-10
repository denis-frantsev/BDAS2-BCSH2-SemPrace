using System;
using System.Collections.Generic;

namespace BDAS2_SemPrace.Models
{
    public partial class Zbozi
    {
        public Zbozi()
        {
            Polozkies = new HashSet<Polozky>();
            SkladyZbozis = new HashSet<SkladyZbozi>();
            Pults = new HashSet<Pulty>();
        }

        public decimal IdZbozi { get; set; }
        public short KodZbozi { get; set; }
        public string NazevZbozi { get; set; }
        public byte IdKategorie { get; set; }
        public byte IdZnacka { get; set; }
        public string Popis { get; set; }
        public decimal Cena { get; set; }

        public virtual Kategorie IdKategorieNavigation { get; set; }
        public virtual Znacky IdZnackaNavigation { get; set; }
        public virtual ICollection<Polozky> Polozkies { get; set; }
        public virtual ICollection<SkladyZbozi> SkladyZbozis { get; set; }

        public virtual ICollection<Pulty> Pults { get; set; }
    }
}
