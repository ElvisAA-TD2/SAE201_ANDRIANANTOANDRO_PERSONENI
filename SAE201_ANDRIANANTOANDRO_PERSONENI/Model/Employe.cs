using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAE201_ANDRIANANTOANDRO_PERSONENI.Model
{
    public class Employe
    {
        private int numEmploye;
        private string nom, prenom, password, login;
        private Role unRole;

        public Employe(int numEmploye, string nom, string prenom, string password, string login, Role unRole)
        {
            this.NumEmploye = numEmploye;
            this.Nom = nom;
            this.Prenom = prenom;
            this.Password = password;
            this.Login = login;
            this.unRole = unRole;
        }
        public Employe()
        {
        }

        public int NumEmploye
        {
            get
            {
                return numEmploye;
            }

            set
            {
                //Check de nombre négatif déja fais dans la bd
                this.numEmploye = value;
            }
        }

        public string Nom
        {
            get
            {
                return nom;
            }

            set
            {
                if (String.IsNullOrWhiteSpace(value)) {throw new ArgumentNullException("Nom employer non valide");}
                else
                    this.nom = value;
            }
        }

        public string Prenom
        {
            get
            {
                return prenom;
            }

            set
            {
                if (String.IsNullOrWhiteSpace(value)) { throw new ArgumentNullException("Prénom employer non valide"); }
                else
                    this.prenom = value;
            }
        }

        public string Password
        {
            get
            {
                return password;
            }

            set
            {
                if (String.IsNullOrWhiteSpace(value)) { throw new ArgumentNullException("Password employer non valide"); }
                else
                    this.password = value;
            }
        }

        public string Login
        {
            get
            {
                return this.login;
            }

            set
            {
                if (String.IsNullOrWhiteSpace(value)) { throw new ArgumentNullException("Logo employer non valide"); }
                else
                    this.login = value;
            }
        }

        public Role UnRole
        {
            get
            {
                return this.unRole;
            }

            set
            {
                this.unRole = value;
            }
        }

        public Role Role
        {
            get
            {
                throw new System.NotImplementedException();
            }

            set
            {
            }
        }

        public List<Employe> FindAll(GestionPilot gestionPilot)
        {
            try
            {
                List<Employe> lesEmployes = new List<Employe>();
                using (NpgsqlCommand cmdSelect = new NpgsqlCommand("select * from employe ;"))
                {
                    DataTable dt = DataAccess.Instance.ExecuteSelect(cmdSelect);
                    foreach (DataRow dr in dt.Rows)
                        lesEmployes.Add(new Employe((Int32)dr["numemploye"], (String)dr["nom"], (String)dr["prenom"],
                            (String)dr["password"], (String)dr["login"], gestionPilot.LesRoles.SingleOrDefault(r => r.NumRole == (Int32)dr["numrole"])));
                }
                return lesEmployes;
            }
            catch (Exception ex) 
            {
                LogError.Log(ex, "Erreur");
                throw new ArgumentException("problème sur la requête"); 
            }
        }
    }
}
