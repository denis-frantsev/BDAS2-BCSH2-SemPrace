using System;
using System.Collections.Generic;

namespace BDAS2_SemPrace.Models
{
    public partial class Supermarkety
    {
        public Supermarkety()
        {
            Platbies = new HashSet<Platby>();
            Pokladnies = new HashSet<Pokladny>();
            Pulties = new HashSet<Pulty>();
            Zamestnancis = new HashSet<Zamestnanci>();
        }

        public decimal IdSupermarket { get; set; }
        public string Nazev { get; set; }
        public int IdAdresa { get; set; }

        public virtual Adresy IdAdresaNavigation { get; set; }
        public virtual ICollection<Platby> Platbies { get; set; }
        public virtual ICollection<Pokladny> Pokladnies { get; set; }
        public virtual ICollection<Pulty> Pulties { get; set; }
        public virtual ICollection<Zamestnanci> Zamestnancis { get; set; }
    }
}
