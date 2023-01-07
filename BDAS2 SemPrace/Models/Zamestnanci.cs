using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BDAS2_SemPrace.Models
{
    public partial class Zamestnanci
    {
        public Zamestnanci()
        {
            InverseIdManazerNavigation = new HashSet<Zamestnanci>();
        }

        public int IdZamestnanec { get; set; }

        [Display(Name = "Jméno")]
        public string Jmeno { get; set; }

        [Display(Name = "Příjmení")]
        public string Prijmeni { get; set; }

        [Display(Name = "Telefonní číslo")]
        public int TelefonniCislo { get; set; }
        public string Email { get; set; }
        public int Mzda { get; set; }

        [Display(Name = "Pobočka")]
        public int? IdSupermarket { get; set; }

        [Display(Name = "Manažer")]
        public int? IdManazer { get; set; }

        [Display(Name = "Pozice")]
        public int IdMisto { get; set; }

        [Display(Name = "Sklad")]
        public int? IdSklad { get; set; }

        [Display(Name = "Slevový kód")]
        public string SlevovyKod { get; set; }

        [Display(Name = "Manažer")]
        public virtual Zamestnanci IdManazerNavigation { get; set; }

        [Display(Name = "Pozice")]
        public virtual PracovniMista IdMistoNavigation { get; set; }

        [Display(Name = "Sklad")]
        public virtual Sklady IdSkladNavigation { get; set; }

        [Display(Name = "Pobočka")]
        public virtual Supermarkety IdSupermarketNavigation { get; set; }
        
        public virtual ICollection<Zamestnanci> InverseIdManazerNavigation { get; set; }
        public string FullName => $"{Jmeno} {Prijmeni}";
    }
}
