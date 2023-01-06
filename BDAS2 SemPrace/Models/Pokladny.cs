using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BDAS2_SemPrace.Models
{
    public partial class Pokladny
    {
        [Display(Name = "Pobočka")]
        public int IdSupermarket { get; set; }

        [Display(Name = "Číslo pokladny")]
        public int CisloPokladny { get; set; }

        [Display(Name = "Pobočka")]
        public virtual Supermarkety IdSupermarketNavigation { get; set; }
    }
}
