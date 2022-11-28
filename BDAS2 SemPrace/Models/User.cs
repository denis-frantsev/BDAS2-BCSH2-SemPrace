using System.ComponentModel.DataAnnotations;

namespace BDAS2_SemPrace.Models
{
    public class User
    {
        public Role Role { get; set; }

        [Required(ErrorMessage = "Zadejte heslo")]
        [RegularExpression("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9]).{8,}$", ErrorMessage = "Heslo musí obsahovat aspoň 8 znaků, 1 velké a malé písmeno a 1 číslici.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Zadejte email")]
        public string Email { get; set; }

        public int ID { get; set; }
        public User()
        {
        }
    }
}
