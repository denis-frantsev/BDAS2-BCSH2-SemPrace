using BDAS2_SemPrace.Data.Interfaces;
using BDAS2_SemPrace.Models;
using System.Collections.Generic;

namespace BDAS2_SemPrace.Data.Repository
{
    public class ZakaznikRepository : IZakaznik
    {

        //private readonly DBContent dBContent;

        //public ZakaznikRepository(DBContent dBContent)
        //{
        //    this.dBContent = dBContent;
        //}

        public IEnumerable<Zakaznici> Zakaznici => null; /*dBContent.Zakaznik.Include(c => c.ID);*/

        public Zakaznici DejZakaznika(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}
