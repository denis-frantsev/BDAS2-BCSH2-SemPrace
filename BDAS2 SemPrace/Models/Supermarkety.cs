using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BDAS2_SemPrace.Models
{
    public partial class Supermarkety
    {
        public Supermarkety()
        {
            Platby = new HashSet<Platby>();
            Pokladny = new HashSet<Pokladny>();
            Pulty = new HashSet<Pulty>();
            Zamestnanci = new HashSet<Zamestnanci>();
        }

        public int IdSupermarket { get; set; }

        [Display(Name = "Název")]
        public string Nazev { get; set; }
        public int IdAdresa { get; set; }

        [Display(Name = "Adresa")]
        public virtual Adresy IdAdresaNavigation { get; set; }
        public virtual ICollection<Platby> Platby { get; set; }
        public virtual ICollection<Pokladny> Pokladny { get; set; }
        public virtual ICollection<Pulty> Pulty { get; set; }
        public virtual ICollection<Zamestnanci> Zamestnanci { get; set; }
    }
}
