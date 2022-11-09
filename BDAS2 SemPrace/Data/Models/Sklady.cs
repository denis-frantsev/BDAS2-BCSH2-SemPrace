﻿using System;
using System.Collections.Generic;

namespace BDAS2_SemPrace.Models
{
    public partial class Sklady
    {
        public Sklady()
        {
            SkladyZbozi = new HashSet<SkladyZbozi>();
            Zamestnanci = new HashSet<Zamestnanci>();
        }

        public decimal IdSklad { get; set; }
        public string Nazev { get; set; }
        public short IdAdresa { get; set; }

        public virtual Adresy IdAdresaNavigation { get; set; }
        public virtual ICollection<SkladyZbozi> SkladyZbozi { get; set; }
        public virtual ICollection<Zamestnanci> Zamestnanci { get; set; }
    }
}
