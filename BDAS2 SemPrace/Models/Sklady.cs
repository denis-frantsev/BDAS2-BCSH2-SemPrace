using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BDAS2_SemPrace.Models
{
    public partial class Sklady
    {
        public Sklady()
        {
            SkladyZbozi = new HashSet<SkladyZbozi>();
            Zamestnanci = new HashSet<Zamestnanci>();
        }

        public int IdSklad { get; set; }

        [Display(Name = "Název")]
        public string Nazev { get; set; }
        public int IdAdresa { get; set; }

        [Display(Name = "Adresa")]
        public virtual Adresy IdAdresaNavigation { get; set; }
        public virtual ICollection<SkladyZbozi> SkladyZbozi { get; set; }
        public virtual ICollection<Zamestnanci> Zamestnanci { get; set; }
    }
}
