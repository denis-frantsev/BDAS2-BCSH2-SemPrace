using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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

        [Display(Name = "ID")]
        public int IdZbozi { get; set; }

        [Display(Name = "Kod zboží")]
        public short KodZbozi { get; set; }

        [Display(Name = "Název zboží")]
        public string NazevZbozi { get; set; }
        public short IdKategorie { get; set; }
        public short IdZnacka { get; set; }
        public string Popis { get; set; }
        public int Cena { get; set; }

        [Display(Name = "Obrázek")]
        public string Obrazek { get; set; }

        [Display(Name = "Kategorie")]
        public virtual Kategorie IdKategorieNavigation { get; set; }

        [Display(Name = "Značka")]
        public virtual Znacky IdZnackaNavigation { get; set; }
        public virtual ICollection<Polozky> Polozky { get; set; }
        public virtual ICollection<SkladyZbozi> SkladyZbozi { get; set; }
        public virtual ICollection<Pulty> Pulty { get; set; }
    }
}
