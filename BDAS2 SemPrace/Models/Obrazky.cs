using System;
using System.Collections.Generic;

namespace BDAS2_SemPrace.Models
{
    public partial class Obrazky
    {
        public int IdObrazek { get; set; }
        public byte[] Data { get; set; }
        public string Popis { get; set; }
        public virtual ICollection<User> Users { get; set; }

    }
}
