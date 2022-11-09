using System.ComponentModel.DataAnnotations;

namespace BDAS2_SemPrace.Models
{
    public class User
    {
        public Permision Permision { get; set; }

        [Required]
        public string Password { get; set; }
        [Required]
        public string Email { get; set; }

        public int ID { get; set; }
        public User()
        {
        }
    }
}
