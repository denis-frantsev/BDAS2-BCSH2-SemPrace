using BDAS2_SemPrace.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BDAS2_SemPrace.Data.Interfaces
{
    public interface IZakaznik
    {
        public IEnumerable<Zakaznik> Zakaznici { get; }
        public Zakaznik DejZakaznika(int id);
    }
}
