using System;
using System.Collections.Generic;

namespace BDAS2_SemPrace.Models
{
    public partial class SkladyZbozi
    {
        public decimal SkladIdSklad { get; set; }
        public decimal ZboziIdZbozi { get; set; }
        public decimal Pocet { get; set; }

        public virtual Sklady SkladIdSkladNavigation { get; set; }
        public virtual Zbozi ZboziIdZboziNavigation { get; set; }
    }
}
