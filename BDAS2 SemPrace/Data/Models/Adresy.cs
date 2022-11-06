using System;
using System.Collections.Generic;

namespace BDAS2_SemPrace.Models
{
    public partial class Adresy
    {
        public Adresy()
        {
            Sklady = new HashSet<Sklady>();
            Supermarkety = new HashSet<Supermarkety>();
        }

        public short IdAdresa { get; set; }
        public string Ulice { get; set; }
        public string Mesto { get; set; }
        public short Psc { get; set; }

        public virtual ICollection<Sklady> Sklady { get; set; }
        public virtual ICollection<Supermarkety> Supermarkety { get; set; }
    }
}
