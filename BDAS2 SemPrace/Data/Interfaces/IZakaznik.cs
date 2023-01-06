
using BDAS2_SemPrace.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BDAS2_SemPrace.Data.Interfaces
{
    public interface IZakaznik
    {
        public IEnumerable<Zakaznici> Zakaznici { get; }
        public Zakaznici DejZakaznika(int id);
    }
}
