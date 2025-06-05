using System;
using System.Collections.Generic;
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
                this.nomCategorie = value;
            }
        }
    }
}
