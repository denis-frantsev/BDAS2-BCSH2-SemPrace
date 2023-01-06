using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Helpers;

namespace BDAS2_SemPrace.Models
{
    public class UserEmployeeViewModel
    {
        private User _user;
        private Zamestnanci _zamestnanec;
        private string _pozice;
        private ModelContext _context;
        private Obrazky _obrazek;
        public UserEmployeeViewModel(User user, ModelContext context)
        {
            _context = context;
            _user = user;
            _zamestnanec = context.Zamestnanci.Single(s => s.Email == user.Email);
            _pozice = context.PracovniMista.Find(_zamestnanec.IdMisto).Nazev;
            _obrazek = context.Obrazky.Find(user.IdObrazek);
        }

        public int ID => _zamestnanec.IdZamestnanec;

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

        public string Pozice
        {
            get => _pozice;
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

        public List<Zamestnanci> Podrizeni => PodrizeniSeznam();

        private List<Zamestnanci> PodrizeniSeznam() {
            DataSet dataset = new DataSet();
            var connStr = "Data Source=(description=(address_list=(address = (protocol = TCP)(host = fei-sql3.upceucebny.cz)(port = 1521)))(connect_data=(service_name=BDAS.UPCEUCEBNY.CZ))\n);User ID=ST64102;Password=j8ex765gh;Persist Security Info=True";
            using (OracleConnection objConn = new OracleConnection(connStr))
            {
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = objConn;
                cmd.CommandText = "podrizeni";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("p_id_zamestnance", OracleDbType.Int32).Value = ID;

                cmd.Parameters.Add("cur", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

                try
                {
                    objConn.Open();
                    cmd.ExecuteNonQuery();
                    OracleDataAdapter da = new OracleDataAdapter(cmd);
                    da.Fill(dataset);
                }
                catch (Exception ex)
                {
                    System.Console.WriteLine("Exception: {0}", ex.ToString());
                }
                objConn.Close();
            }
            var data = dataset.Tables[0].AsEnumerable().Select(r => new Zamestnanci
            {
                IdZamestnanec = (int)r.Field<decimal>("id_zamestnanec"),
                Jmeno = r.Field<string>("jmeno"),
                Prijmeni = r.Field<string>("prijmeni"),
                Email = r.Field<string>("email"),
                TelefonniCislo = (int)r.Field<decimal>("telefonni_cislo"),
            });

            return data.ToList();
        }
    }
}
