using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAE201_ANDRIANANTOANDRO_PERSONENI.Model
{
    public class Role
    {
        private int numRole;
        private string nomRole;

        public Role(int numRole, string nomRole)
        {
            this.NumRole = numRole;
            this.NomRole = nomRole;
        }
        public Role()
        {
        }

        public int NumRole
        {
            get
            {
                return numRole;
            }

            set
            {
                //Check de nombre négatif déja fais dans la bd
                this.numRole = value;
            }
        }

        public string NomRole
        {
            get
            {
                return this.nomRole;
            }

            set
            {
                if (String.IsNullOrWhiteSpace(value)) { throw new ArgumentNullException("Nom Role non valide"); }
                else
                    this.nomRole = value;
            }
        }

        public List<Role> FindAll ()
        {
            try
            {
                List<Role> lesRoles = new List<Role>();
                using (NpgsqlCommand cmdSelect = new NpgsqlCommand("select * from role ;"))
                {
                    DataTable dt = DataAccess.Instance.ExecuteSelect(cmdSelect);
                    foreach (DataRow dr in dt.Rows)
                        lesRoles.Add(new Role((Int32)dr["numrole"], (String)dr["libellerole"]));
                }
                return lesRoles;
            }
            catch(Exception ex) 
            {
                LogError.Log(ex, "Erreur");
                throw new ArgumentException("problème sur la requête"); 
            }
        }
    }
}
