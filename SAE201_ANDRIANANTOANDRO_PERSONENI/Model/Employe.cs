using System;
using System.Collections.Generic;
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
    }
}
