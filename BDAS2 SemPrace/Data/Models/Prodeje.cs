using System;
using System.Collections.Generic;

namespace BDAS2_SemPrace.Models
{
    public partial class Prodeje
    {
        public Prodeje()
        {
            Polozky = new HashSet<Polozky>();
        }

        public int CisloProdeje { get; set; }
        public decimal Suma { get; set; }
        public DateTime Datum { get; set; }
        public int IdPlatba { get; set; }

        public virtual Platby IdPlatbaNavigation { get; set; }
        public virtual ICollection<Polozky> Polozky { get; set; }
    }
}
