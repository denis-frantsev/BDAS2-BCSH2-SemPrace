using System;
using System.Collections.Generic;

namespace BDAS2_SemPrace.Models
{
    public partial class Zbozi
    {
        public Zbozi()
        {
            Polozky = new HashSet<Polozky>();
            SkladyZbozi = new HashSet<SkladyZbozi>();
            Pulty = new HashSet<Pulty>();
        }

        public decimal IdZbozi { get; set; }
        public short KodZbozi { get; set; }
        public string NazevZbozi { get; set; }
        public short IdKategorie { get; set; }
        public short IdZnacka { get; set; }
        public string Popis { get; set; }
        public decimal Cena { get; set; }
        public string Obrazek { get; set; }

        public virtual Kategorie IdKategorieNavigation { get; set; }
        public virtual Znacky IdZnackaNavigation { get; set; }
        public virtual ICollection<Polozky> Polozky { get; set; }
        public virtual ICollection<SkladyZbozi> SkladyZbozi { get; set; }

        public virtual ICollection<Pulty> Pulty { get; set; }
    }
}
