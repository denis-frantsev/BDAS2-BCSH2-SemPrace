using System.ComponentModel.DataAnnotations;

namespace BDAS2_SemPrace.Models
{
    public class TopZakazniciViewModel
    {
        [Display(Name = "Jméno a příjmení")]
        public string FullName { get; set; }
        [Display(Name = "Celkem utraceno")]
        public int Utratili { get; set; }
    }
}
