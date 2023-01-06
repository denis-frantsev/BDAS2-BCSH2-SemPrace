using System;
using System.Collections.Generic;

namespace BDAS2_SemPrace.Models
{
    public partial class PracovniMistum
    {
        public PracovniMistum()
        {
            Zamestnancis = new HashSet<Zamestnanci>();
        }

        public decimal IdMisto { get; set; }
        public string Nazev { get; set; }
        public string Popis { get; set; }
        public int MinPlat { get; set; }

        public virtual ICollection<Zamestnanci> Zamestnancis { get; set; }
    }
}
