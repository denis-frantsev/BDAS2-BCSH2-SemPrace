
using System.Collections.Generic;
using System.Linq;

namespace BDAS2_SemPrace.Models
{
    public class UserCustomerViewModel
    {
        private User _user;
        private Zakaznici _zakaznik;
        private Obrazky _obrazek;
        public UserCustomerViewModel(User user, ModelContext context)
        {
            _user = user;
            _zakaznik = context.Zakaznici.Single(s => s.Email == _user.Email);
            Platby = context.Platby.Where(p => p.IdZakaznik == _zakaznik.IdZakaznik);
            _obrazek = context.Obrazky.Find(user.IdObrazek);
        }

        public int ID => _zakaznik.IdZakaznik;

        public Role Role { get => _user.Role; set => _user.Role = value; }

        public string FullName => $"{_zakaznik.Jmeno} {_zakaznik.Prijmeni}";

        public string Name
        {
            get => _zakaznik.Jmeno;
            set => _zakaznik.Jmeno = value;
        }

        public string LastName
        {
            get => _zakaznik.Prijmeni;
            set => _zakaznik.Prijmeni = value;
        }

        public string TelefonniCislo
        {
            get => ((int)_zakaznik.TelefonniCislo).ToString();
            set => _zakaznik.TelefonniCislo = int.Parse(value);
        }

        public string Email
        {
            get => _user.Email;
            set
            {
                _user.Email = value;
                _zakaznik.Email = value;
            }
        }

        public string Password
        {
            get => _user.Password;
            set => _user.Password = value;
        }

        public IEnumerable<Platby> Platby
        {
            get;
        }

        public Obrazky ProfilePic
        {
            get
            {
                if (_obrazek != null)
                    return _obrazek;
                else return null;
            }
        }
    }
}
