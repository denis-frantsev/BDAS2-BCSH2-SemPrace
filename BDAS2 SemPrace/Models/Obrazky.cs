using System;
using System.Collections.Generic;

namespace BDAS2_SemPrace.Models
{
    public partial class Obrazky
    {
        public int IdObrazek { get; set; }
        public byte[] Data { get; set; }
        public string Popis { get; set; }
        public string Nazev { get; set; }
        public string Pripona { get; set; }

        public virtual ICollection<User> Users { get; set; }

    }
}
