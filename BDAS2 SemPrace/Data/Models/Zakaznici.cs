using System;
using System.Collections.Generic;

namespace BDAS2_SemPrace.Models
{
    public partial class Zakaznici
    {
        public Zakaznici()
        {
            Platby = new HashSet<Platby>();
        }

        public int IdZakaznik { get; set; }
        public string Jmeno { get; set; }
        public string Prijmeni { get; set; }
        public decimal TelefonniCislo { get; set; }
        public string Email { get; set; }
        public virtual ICollection<Platby> Platby { get; set; }
    }
}
