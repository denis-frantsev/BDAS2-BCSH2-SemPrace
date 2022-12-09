using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BDAS2_SemPrace.Models
{
    public partial class Adresy
    {
        public Adresy()
        {
            Sklady = new HashSet<Sklady>();
            Supermarkety = new HashSet<Supermarkety>();
        }
        public int IdAdresa { get; set; }
        public string Ulice { get; set; }

        [Display(Name ="Město")]
        public string Mesto { get; set; }

        [Display(Name ="PSČ")]
        public int Psc { get; set; }

        public virtual ICollection<Sklady> Sklady { get; set; }
        public virtual ICollection<Supermarkety> Supermarkety { get; set; }

        public override string ToString()
        {
            return $"{Ulice}, {Mesto} {Psc}";
        }
    }
}
