using System;
using System.Collections.Generic;

namespace BDAS2_SemPrace.Models
{
    public partial class Adresy
    {
        public Adresy()
        {
            Skladies = new HashSet<Sklady>();
            Supermarketies = new HashSet<Supermarkety>();
        }

        public int IdAdresa { get; set; }
        public string Ulice { get; set; }
        public string Mesto { get; set; }
        public short Psc { get; set; }

        public virtual ICollection<Sklady> Skladies { get; set; }
        public virtual ICollection<Supermarkety> Supermarketies { get; set; }
    }
}
