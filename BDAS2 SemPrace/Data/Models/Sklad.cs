using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BDAS2_SemPrace.Data.Models
{
    public class Sklad
    {
        public int ID { get; set; }
        public string Nazev { get; set; }
        public int AdresaID { get; set; }
        public Adresa Adresa { get; set; }
        
    }
}
