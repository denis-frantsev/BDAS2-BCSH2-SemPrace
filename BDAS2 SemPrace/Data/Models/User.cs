namespace BDAS2_SemPrace.Data.Models
{
    public class User
    {
        public Permision Permision { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }

        public User()
        {
        }
    }
}
