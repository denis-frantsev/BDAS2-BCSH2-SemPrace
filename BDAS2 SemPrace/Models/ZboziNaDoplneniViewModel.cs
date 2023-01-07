using System.ComponentModel.DataAnnotations;

namespace BDAS2_SemPrace.Models
{
    public class ZboziNaDoplneniViewModel
    {
        public string Sklad { get; set; }

        [Display(Name = "Název zboží")]
        public string NazevZbozi { get; set; }

        [Display(Name = "Počet")]
        public int Pocet { get; set; }
    }
}
