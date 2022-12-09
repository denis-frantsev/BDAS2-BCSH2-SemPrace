using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BDAS2_SemPrace.Models
{
    public partial class Polozky
    {
        public int IdZbozi { get; set; }

        [Display(Name = "Zboží")]
        public string NazevZbozi { get; set; }

        [Display(Name = "Množství")]
        public int Mnozstvi { get; set; }

        [Display(Name = "Číslo prodeje")]
        public int CisloProdeje { get; set; }

        [Display(Name = "Číslo prodeje")]
        public virtual Prodeje CisloProdejeNavigation { get; set; }

        [Display(Name = "Zboží")]
        public virtual Zbozi IdZboziNavigation { get; set; }
    }
}
