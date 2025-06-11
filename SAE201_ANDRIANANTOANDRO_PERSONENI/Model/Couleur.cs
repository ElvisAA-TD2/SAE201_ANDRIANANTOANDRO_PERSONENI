using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAE201_ANDRIANANTOANDRO_PERSONENI.Model
{
    public class Couleur
    {
        private int numCouleur;
        private string nomCouleur;

        public Couleur(int numCouleur, string nomCouleur)
        {
            this.NumCouleur = numCouleur;
            this.NomCouleur = nomCouleur;
        }
        public Couleur()
        {
        }

        public int NumCouleur
        {
            get
            {
                return numCouleur;
            }

            set
            {
                this.numCouleur = value;
            }
        }

        public string NomCouleur
        {
            get
            {
                return this.nomCouleur;
            }

            set
            {
                if (String.IsNullOrWhiteSpace(value)) { throw new ArgumentNullException("Nom Couleur null"); }
                else
                    this.nomCouleur = value;
            }
        }
        public List<Couleur> FindAll()
        {
            List<Couleur> lesCouleurs = new List<Couleur>();
            using (NpgsqlCommand cmdSelect = new NpgsqlCommand("select * from couleur ;"))
            {
                DataTable dt = DataAccess.Instance.ExecuteSelect(cmdSelect);
                foreach (DataRow dr in dt.Rows)
                    lesCouleurs.Add(new Couleur((Int32)dr["numcouleur"], (String)dr["libellecouleur"]));
            }
            return lesCouleurs;
        }
    }
}
