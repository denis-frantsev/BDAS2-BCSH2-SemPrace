using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BDAS2_SemPrace.Data.Models
{
    public class Adresa
    {
        public int ID { get; set; }
        public string Ulice { get; set; }
        public string Mesto { get; set; }
        public short PSC { get; set; }
    }
}
