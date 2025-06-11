using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAE201_ANDRIANANTOANDRO_PERSONENI.Model
{
    public class ModeTransport
    {
        private int numModeTransport;
        private string nomModeTransport;

        public ModeTransport()
        {
        }

        public ModeTransport(int numModeTransport, string nomModeTransport)
        {
            this.NumModeTransport = numModeTransport;
            this.NomModeTransport = nomModeTransport;
        }

        public int NumModeTransport
        {
            get
            {
                return numModeTransport;
            }

            set
            {
                this.numModeTransport = value;
            }
        }

        public string NomModeTransport
        {
            get
            {
                return this.nomModeTransport;
            }

            set
            {
                if (String.IsNullOrEmpty(value)) { throw new ArgumentNullException("value"); }
                else
                    this.nomModeTransport = value;
            }
        }
        public List<ModeTransport> FindAll()
        {
            try
            {
                List<ModeTransport> lesModesDeTransport = new List<ModeTransport>();
                using (NpgsqlCommand cmdSelect = new NpgsqlCommand("select * from modetransport ;"))
                {
                    DataTable dt = DataAccess.Instance.ExecuteSelect(cmdSelect);
                    foreach (DataRow dr in dt.Rows)
                        lesModesDeTransport.Add(new ModeTransport((Int32)dr["numtransport"], (String)dr["libelletransport"]));
                }
                return lesModesDeTransport;
            }
            catch (Exception ex) { throw new ArgumentException("problème sur la requête"); }

        }
    }
}
