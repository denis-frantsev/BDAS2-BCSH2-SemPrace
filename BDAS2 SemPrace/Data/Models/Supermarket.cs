using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BDAS2_SemPrace.Data.Models
{
    public class Supermarket
    {
        public int ID { get; set; }
        public string Nazev { get; set; }
        public int AdresaID { get; set; } //foreign key??
        public Adresa Adresa { get; set; }
    }
}
