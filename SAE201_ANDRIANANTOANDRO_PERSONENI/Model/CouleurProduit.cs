using Npgsql;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAE201_ANDRIANANTOANDRO_PERSONENI.Model
{
    public class CouleurProduit
    {
        private int codeCouleur;
        private int codeProduit;
        public CouleurProduit()
        { }

        public CouleurProduit(int codeCouleur, int codeProduit)
        {
            this.CodeCouleur = codeCouleur;
            this.CodeProduit = codeProduit;
        }

        public int CodeCouleur
        {
            get
            {
                return codeCouleur;
            }

            set
            {
                //Check de nombre négatif déja fais dans la bd
                this.codeCouleur = value;
            }
        }

        public int CodeProduit
        {
            get
            {
                return this.codeProduit;
            }

            set
            {
                //Check de nombre négatif déja fais dans la bd
                this.codeProduit = value;
            }
        }

        public List<CouleurProduit> FindAll()
        {
            List<CouleurProduit> lesCouleurProduits = new List<CouleurProduit>();
            using (NpgsqlCommand cmdSelect = new NpgsqlCommand("select * from couleurproduit ;"))
            {
                DataTable dt = DataAccess.Instance.ExecuteSelect(cmdSelect);
                foreach (DataRow dr in dt.Rows)
                    lesCouleurProduits.Add(new CouleurProduit((Int32)dr["numcouleur"], (Int32)dr["numproduit"]));
            }
            return lesCouleurProduits;
        }
    }
}
