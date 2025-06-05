using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAE201_ANDRIANANTOANDRO_PERSONENI.Model
{
    public class Type
    {
        private int codeCategorie;
        private string nomType;
        private Categorie uneCategorie;

        public Type(int codeCategorie, string nomType, Categorie uneCategorie)
        {
            this.CodeCategorie = codeCategorie;
            this.NomType = nomType;
            this.UneCategorie = uneCategorie;
        }
        public Type()
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

        public string NomType
        {
            get
            {
                return nomType;
            }

            set
            {
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
    }
}
