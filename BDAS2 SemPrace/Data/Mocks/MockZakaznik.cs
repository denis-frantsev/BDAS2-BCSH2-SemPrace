using BDAS2_SemPrace.Data.Interfaces;
using BDAS2_SemPrace.Data.Models;
using System;
using System.Collections.Generic;

namespace BDAS2_SemPrace.Data.Mocks
{
    class MockZakaznik : IZakaznik
    {
        public IEnumerable<Zakaznik> Zakaznici
        {
            get
            {

                return new List<Zakaznik>() {
                    new Zakaznik() {
                        Jmeno = "Denis",
                        Prijmeni = "Frantsev",
                        Email="denis.frantsev@gmail.com",
                        TelCislo = 776123286
                    }
                };

            }
        }
        public Zakaznik DejZakaznika(int id)
        {
            return null;
        }
    }
}
