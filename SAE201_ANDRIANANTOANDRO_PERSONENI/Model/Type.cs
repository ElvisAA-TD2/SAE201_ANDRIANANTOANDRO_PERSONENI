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
    public class Type
    {
        private int codeType;
        private string nomType;
        private Categorie uneCategorie;

        public Type(int codeType, string nomType, Categorie uneCategorie)
        {
            this.CodeType = codeType;
            this.NomType = nomType;
            this.UneCategorie = uneCategorie;
        }
        public Type()
        {
        }

        public int CodeType
        {
            get
            {
                return codeType;
            }

            set
            {
                //Check de nombre négatif déja fais dans la bd
                this.codeType = value;
            }
        }

        public string NomType
        {
            get
            {
                return nomType;
            }

            set
            {
                if (String.IsNullOrWhiteSpace(value)) { throw new ArgumentNullException("Nom type non valide"); }
                else
                    this.nomType = value;
            }
        }

        public Categorie UneCategorie
        {
            get
            {
                return this.uneCategorie;
            }

            set
            {
                this.uneCategorie = value;
            }
        }
        public List<Type> FindAll(GestionPilot gestionPilot)
        {
            try
            {
                List<Type> lesTypes = new List<Type>();
                using (NpgsqlCommand cmdSelect = new NpgsqlCommand("select * from type ;"))
                {
                    DataTable dt = DataAccess.Instance.ExecuteSelect(cmdSelect);
                    foreach (DataRow dr in dt.Rows)
                        lesTypes.Add(new Type((Int32)dr["numtype"], (String)dr["libelletype"],
                            gestionPilot.LesCategories.SingleOrDefault(c => c.CodeCategorie == (Int32)dr["numcategorie"])));
                }
                return lesTypes;
            }
            catch (Exception ex) { throw new ArgumentException("problème sur la requête"); }
        }
    }
}
