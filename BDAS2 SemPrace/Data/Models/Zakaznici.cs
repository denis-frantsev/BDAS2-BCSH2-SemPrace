using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BDAS2_SemPrace.Models
{
    public partial class Zakaznici
    {
        public Zakaznici()
        {
            Platby = new HashSet<Platby>();
        }

        [Display(Name = "ID")]
        public int IdZakaznik { get; set; }

        [Display(Name = "Zákazník")]
        public string Jmeno { get; set; }

        [Display(Name = "Příjmení")]
        public string Prijmeni { get; set; }

        [Display(Name = "Telefonní číslo")]
        public decimal TelefonniCislo { get; set; }

        [Display(Name = "E-mail")]
        public string Email { get; set; }
        public virtual ICollection<Platby> Platby { get; set; }
    }
}
