using System;
using System.Collections.Generic;

namespace BDAS2_SemPrace.Models
{
    public partial class PracovniMista
    {
        public PracovniMista()
        {
            Zamestnanci = new HashSet<Zamestnanci>();
        }

        public int IdMisto { get; set; }
        public string Nazev { get; set; }
        public string Popis { get; set; }
        public int MinPlat { get; set; }

        public virtual ICollection<Zamestnanci> Zamestnanci { get; set; }
    }
}
