using BDAS2_SemPrace.Models;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BDAS2_SemPrace
{
    public static class DataBase
    {
        //private static string _conString = "USER ID=ST64102;DATA SOURCE=fei-sql1.upceucebny.cz:1521/IDAS.UPCEUCEBNY.CZ;Password=j8ex765gh";

        //public static void Execute(string command, out List<Zakaznici> seznam)
        //{

        //    OracleConnection con = new OracleConnection(_conString);
        //    OracleCommand cmd = new OracleCommand();
        //    cmd.CommandText = command;
        //    cmd.Connection = con;
        //    con.Open();
        //    OracleDataReader dr = cmd.ExecuteReader();
        //    List<Zakaznici> zakaznici = new List<Zakaznici>();
        //    while (dr.Read())
        //    {
        //        zakaznici.Add(new Zakaznici()
        //        {
        //            IdZakaznik = dr.GetInt32(0),
        //            Jmeno = dr.GetString(1),
        //            Prijmeni = dr.GetString(2),
        //            TelefonniCislo = dr.GetInt32(3),
        //            Email = dr.GetString(4)
        //        });
        //    }
        //    seznam = zakaznici;
        //    con.Close();
        //}
    }
}
