using System;
using System.Collections.Generic;

namespace BDAS2_SemPrace.Models
{
    public partial class NazvyPultu
    {
        public NazvyPultu()
        {
            Pulty = new HashSet<Pulty>();
        }

        public string IdPult { get; set; }
        public string Nazev { get; set; }

        public virtual ICollection<Pulty> Pulty { get; set; }
    }
}
