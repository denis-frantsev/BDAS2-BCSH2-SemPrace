using System;
using System.Collections.Generic;

namespace BDAS2_SemPrace.Models
{
    public partial class Zamestnanci
    {
        public Zamestnanci()
        {
            InverseIdManazerNavigation = new HashSet<Zamestnanci>();
        }

        public decimal IdZamestnanec { get; set; }
        public string Jmeno { get; set; }
        public string Prijmeni { get; set; }
        public decimal TelefonniCislo { get; set; }
        public string Email { get; set; }
        public decimal Mzda { get; set; }
        public decimal? IdSupermarket { get; set; }
        public decimal? IdManazer { get; set; }
        public decimal IdMisto { get; set; }
        public decimal? IdSklad { get; set; }

        public virtual Zamestnanci IdManazerNavigation { get; set; }
        public virtual PracovniMista IdMistoNavigation { get; set; }
        public virtual Sklady IdSkladNavigation { get; set; }
        public virtual Supermarkety IdSupermarketNavigation { get; set; }
        public virtual ICollection<Zamestnanci> InverseIdManazerNavigation { get; set; }
    }
}
