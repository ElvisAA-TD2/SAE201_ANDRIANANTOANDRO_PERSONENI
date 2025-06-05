using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAE201_ANDRIANANTOANDRO_PERSONENI.Model
{
    public class ProduitCommande
    {
        private int quantiteCommande;
        private double prix;
        private Produit unProduit;

        public ProduitCommande(int quantiteCommande, double prix, Produit unProduit)
        {
            this.QuantiteCommande = quantiteCommande;
            this.Prix = prix;
            this.UnProduit = unProduit;
        }
        public ProduitCommande()
        {
        }

        public int QuantiteCommande
        {
            get
            {
                return quantiteCommande;
            }

            set
            {
                this.quantiteCommande = value;
            }
        }

        public double Prix
        {
            get
            {
                return prix;
            }

            set
            {
                this.prix = value;
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
