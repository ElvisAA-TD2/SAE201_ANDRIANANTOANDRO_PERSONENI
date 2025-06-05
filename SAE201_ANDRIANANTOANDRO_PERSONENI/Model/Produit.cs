using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SAE201_ANDRIANANTOANDRO_PERSONENI.Model
{
    public class Produit
    {
        private string codeProduit, nomProduit, cheminImage;
        private double prixVente;
        private int qteStock;
        private bool disponible;
        private TypePointe unTypePointe;
        private Type unType;
        private List<CouleurProduit> lesCouleurProduit;

        public Produit()
        {
        }

        public Produit(string codeProduit, string nomProduit, string cheminImage, double prixVente, int qteStock, bool disponible, TypePointe unTypePointe, Type unType, List<CouleurProduit> lesCouleurProduit)
        {
            this.CodeProduit = codeProduit;
            this.NomProduit = nomProduit;
            this.CheminImage = cheminImage;
            this.PrixVente = prixVente;
            this.QteStock = qteStock;
            this.Disponible = disponible;
            this.UnTypePointe = unTypePointe;
            this.UnType = unType;
            this.LesCouleurProduit = lesCouleurProduit;
        }

        public string CodeProduit
        {
            get
            {
                return codeProduit;
            }

            set
            {
                this.codeProduit = value;
            }
        }

        public string NomProduit
        {
            get
            {
                return nomProduit;
            }

            set
            {
                this.nomProduit = value;
            }
        }

        public string CheminImage
        {
            get
            {
                return cheminImage;
            }

            set
            {
                this.cheminImage = value;
            }
        }

        public double PrixVente
        {
            get
            {
                return prixVente;
            }

            set
            {
                this.prixVente = value;
            }
        }

        public int QteStock
        {
            get
            {
                return qteStock;
            }

            set
            {
                this.qteStock = value;
            }
        }

        public bool Disponible
        {
            get
            {
                return this.disponible;
            }

            set
            {
                this.disponible = value;
            }
        }

        public TypePointe UnTypePointe
        {
            get
            {
                return unTypePointe;
            }

            set
            {
                this.unTypePointe = value;
            }
        }

        public Type UnType
        {
            get
            {
                return unType;
            }

            set
            {
                this.unType = value;
            }
        }

        public List<CouleurProduit> LesCouleurProduit
        {
            get
            {
                return this.lesCouleurProduit;
            }

            set
            {
                this.lesCouleurProduit = value;
            }
        }
    }
}
