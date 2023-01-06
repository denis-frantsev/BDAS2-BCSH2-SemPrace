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

        [Display(Name = "Jméno"), MaxLength(25, ErrorMessage = "Příliš dlouhé")]
        public string Jmeno { get; set; }

        [Display(Name = "Příjmení"), MaxLength(25, ErrorMessage = "Příliš dlouhé")]
        public string Prijmeni { get; set; }

        public string FullName => $"{Jmeno} {Prijmeni}";

        [Display(Name = "Telefonní číslo")]
        public int TelefonniCislo { get; set; }

        [Display(Name = "E-mail"), MaxLength(30, ErrorMessage = "Uvedena Vámi emailová adresa je příliš dlouhá")]
        public string Email { get; set; }
        public virtual ICollection<Platby> Platby { get; set; }
    }
}
