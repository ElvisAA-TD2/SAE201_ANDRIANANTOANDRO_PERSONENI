using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAE201_ANDRIANANTOANDRO_PERSONENI.Model
{
    public class CouleurProduit
    {
        private Couleur uneCouleur;
        private Produit unProduit;

        public CouleurProduit(Couleur uneCouleur, Produit unProduit)
        {
            this.UneCouleur = uneCouleur;
            this.UnProduit = unProduit;
        }
        public CouleurProduit()
        { }

        public Couleur UneCouleur
        {
            get
            {
                return uneCouleur;
            }

            set
            {
                this.uneCouleur = value;
            }
        }

        public Produit UnProduit
        {
            get
            {
                return this.unProduit;
            }

            set
            {
                this.unProduit = value;
            }
        }
    }
}
