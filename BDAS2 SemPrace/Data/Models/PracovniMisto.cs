using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BDAS2_SemPrace.Data.Models
{
    public class PracovniMisto
    {
        public int ID{ get; set; }
        public string Nazev { get; set; }
        public string Popis { get; set; }
        public ushort MinPlat { get; set; }
    }
}
