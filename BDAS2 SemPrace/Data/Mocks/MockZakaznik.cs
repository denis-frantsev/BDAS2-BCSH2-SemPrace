using BDAS2_SemPrace.Data.Interfaces;
using BDAS2_SemPrace.Models;
using System;
using System.Collections.Generic;

namespace BDAS2_SemPrace.Data.Mocks
{
    class MockZakaznik : IZakaznik
    {
        private IEnumerable<Zakaznici> _zakaznici;
        public IEnumerable<Zakaznici> Zakaznici
        {
            get
            {
                List<Zakaznici> seznam;
                DataBase.Execute("select*from zakaznici", out seznam);
                _zakaznici = seznam;
                return _zakaznici;
            }
            set { _zakaznici = value; }
        }
        public Zakaznici DejZakaznika(int id)
        {
            return null;
        }
    }
}
