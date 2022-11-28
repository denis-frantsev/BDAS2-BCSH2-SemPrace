using System.Linq;

namespace BDAS2_SemPrace.Models
{
    public class UserEmployeeViewModel
    {
        private User _user;
        private Zamestnanci _zamestnanec;

        public UserEmployeeViewModel(User user, ModelContext context)
        {
            _user = user;
            _zamestnanec = context.Zamestnanci.Single(s => s.Email == user.Email);
        }

        public Role Role { get => _user.Role; set => _user.Role = value; }

        public string FullName => $"{_zamestnanec.Jmeno} {_zamestnanec.Prijmeni}";

        public string Name
        {
            get => _zamestnanec.Jmeno;
            set => _zamestnanec.Jmeno = value;
        }

        public string LastName
        {
            get => _zamestnanec.Prijmeni;
            set => _zamestnanec.Prijmeni = value;
        }

        public string TelefonniCislo
        {
            get => ((int)_zamestnanec.TelefonniCislo).ToString();
            set => _zamestnanec.TelefonniCislo = int.Parse(value);
        }

        public string Email
        {
            get => _user.Email;
            set
            {
                _user.Email = value;
                _zamestnanec.Email = value;
            }
        }

        public string Password
        {
            get => _user.Password;
            set => _user.Password = value;
        }

    }
}
