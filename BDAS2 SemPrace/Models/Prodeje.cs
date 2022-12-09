using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BDAS2_SemPrace.Models
{
    public partial class Prodeje
    {
        public Prodeje()
        {
            Polozky = new HashSet<Polozky>();
        }

        [Display(Name = "Číslo prodeje")]
        public int CisloProdeje { get; set; }
        public int Suma { get; set; }
        public DateTime Datum { get; set; }
        public int IdPlatba { get; set; }

        [Display(Name = "Platba")]
        public virtual Platby IdPlatbaNavigation { get; set; }
        public virtual ICollection<Polozky> Polozky { get; set; }
    }
}
