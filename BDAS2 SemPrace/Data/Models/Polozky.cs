using System;
using System.Collections.Generic;

namespace BDAS2_SemPrace.Models
{
    public partial class Polozky
    {
        public decimal IdZbozi { get; set; }
        public string NazevZbozi { get; set; }
        public decimal Mnozstvi { get; set; }
        public int CisloProdeje { get; set; }

        public virtual Prodeje CisloProdejeNavigation { get; set; }
        public virtual Zbozi IdZboziNavigation { get; set; }
    }
}
