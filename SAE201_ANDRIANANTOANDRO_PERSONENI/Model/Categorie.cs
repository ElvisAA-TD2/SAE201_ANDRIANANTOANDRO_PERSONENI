using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAE201_ANDRIANANTOANDRO_PERSONENI.Model
{
    public class Categorie
    {
        private int codeCategorie;
        private string nomCategorie;

        public Categorie(int codeCategorie, string nomCategorie)
        {
            this.CodeCategorie = codeCategorie;
            this.NomCategorie = nomCategorie;
        }
        public Categorie()
        {
        }

        public int CodeCategorie
        {
            get
            {
                return codeCategorie;
            }

            set
            {
                this.codeCategorie = value;
            }
        }

        public string NomCategorie
        {
            get
            {
                return this.nomCategorie;
            }

            set
            {
                if (String.IsNullOrWhiteSpace(value)) { throw new ArgumentNullException("Nom catégorie null"); }
                else
                    this.nomCategorie = value;
            }
        }
        public List<Categorie> FindAll()
        {
            List<Categorie> lesCategories = new List<Categorie>();
            using (NpgsqlCommand cmdSelect = new NpgsqlCommand("select * from categorie ;"))
            {
                DataTable dt = DataAccess.Instance.ExecuteSelect(cmdSelect);
                foreach (DataRow dr in dt.Rows)
                    lesCategories.Add(new Categorie((Int32)dr["numcategorie"], (String)dr["libellecategorie"]));
            }
            return lesCategories;
        }
    }
}
