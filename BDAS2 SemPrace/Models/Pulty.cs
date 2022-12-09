using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BDAS2_SemPrace.Models
{
    public partial class Pulty
    {
        public Pulty()
        {
            ZboziIdZbozi = new HashSet<Zbozi>();
        }

        [Display(Name = "Číslo pultu")]
        public int CisloPultu { get; set; }
        public int IdSupermarket { get; set; }
        public string Nazev { get; set; }

        [Display(Name = "Pobočka")]
        public virtual Supermarkety IdSupermarketNavigation { get; set; }

        [Display(Name = "Kategorie pultu")]
        public virtual NazvyPultu NazevNavigation { get; set; }

        public virtual ICollection<Zbozi> ZboziIdZbozi { get; set; }
    }
}
