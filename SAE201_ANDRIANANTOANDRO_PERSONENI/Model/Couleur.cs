using System;
using System.Collections.Generic;
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
                this.nomCouleur = value;
            }
        }
    }
}
