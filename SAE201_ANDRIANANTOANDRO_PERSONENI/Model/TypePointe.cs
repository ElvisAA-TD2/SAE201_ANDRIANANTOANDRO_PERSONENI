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
    public class TypePointe
    {
        private int codeTypePointe;
        private string nomTypePointe;

        public TypePointe(int codeTypePointe, string nomTypePointe)
        {
            this.CodeTypePointe = codeTypePointe;
            this.NomTypePointe = nomTypePointe;
        }

        public TypePointe()
        {
        }

        public int CodeTypePointe
        {
            get
            {
                return codeTypePointe;
            }

            set
            {
                //Check de nombre négatif déja fais dans la bd
                this.codeTypePointe = value;
            }
        }

        public string NomTypePointe
        {
            get
            {
                return this.nomTypePointe;
            }

            set
            {
                if (String.IsNullOrEmpty(value)) { throw new ArgumentNullException("Nom typepointe non valide"); }
                else
                    this.nomTypePointe = value;
            }
        }
        public List<TypePointe> FindAll()
        {
            try
            {
                List<TypePointe> lesTypePointes = new List<TypePointe>();
                using (NpgsqlCommand cmdSelect = new NpgsqlCommand("select * from typepointe ;"))
                {
                    DataTable dt = DataAccess.Instance.ExecuteSelect(cmdSelect);
                    foreach (DataRow dr in dt.Rows)
                        lesTypePointes.Add(new TypePointe((Int32)dr["numtypepointe"], (String)dr["libelletypepointe"]));
                }
                return lesTypePointes;
            }
            catch (Exception ex) { throw new ArgumentException("problème sur la requête"); }
        }
    }
}
