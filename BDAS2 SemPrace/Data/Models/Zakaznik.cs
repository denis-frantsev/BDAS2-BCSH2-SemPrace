using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BDAS2_SemPrace.Data.Models
{
    public class Zakaznik
    {
        public int ID { get; set; }
        public string Jmeno { get; set; }
        public string Prijmeni { get; set; }
        public int TelCislo { get; set; }
        public string Email { get; set; }
    }
}
