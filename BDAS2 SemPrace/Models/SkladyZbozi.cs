using System;
using System.Collections.Generic;

namespace BDAS2_SemPrace.Models
{
    public partial class SkladyZbozi
    {
        public int SkladIdSklad { get; set; }
        public int ZboziIdZbozi { get; set; }
        public int Pocet { get; set; }

        public virtual Sklady SkladIdSkladNavigation { get; set; }
        public virtual Zbozi ZboziIdZboziNavigation { get; set; }
    }
}
