using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAE201_ANDRIANANTOANDRO_PERSONENI.Model
{
    public class Commande
    {
        private int numCommande;
        private DateTime dateCommande, dateLivraion;
        private Employe unEmploye;
        private ModeTransport unModeTransport;
        private Revendeur unRevendeur;
        private List<ProduitCommande> lesProduitCommande;

        public Commande(int numCommande, DateTime dateCommande, DateTime dateLivraion, Employe unEmploye, ModeTransport unModeTransport, Revendeur unRevendeur, List<ProduitCommande> lesProduitCommande)
        {
            this.NumCommande = numCommande;
            this.DateCommande = dateCommande;
            this.DateLivraion = dateLivraion;
            this.UnEmploye = unEmploye;
            this.UnModeTransport = unModeTransport;
            this.UnRevendeur = unRevendeur;
            this.lesProduitCommande = lesProduitCommande;
        }
        public Commande()
        {
        }

        public int NumCommande
        {
            get
            {
                return numCommande;
            }

            set
            {
                this.numCommande = value;
            }
        }

        public DateTime DateCommande
        {
            get
            {
                return dateCommande;
            }

            set
            {
                this.dateCommande = value;
            }
        }

        public DateTime DateLivraion
        {
            get
            {
                return dateLivraion;
            }

            set
            {
                this.dateLivraion = value;
            }
        }

        public Employe UnEmploye
        {
            get
            {
                return unEmploye;
            }

            set
            {
                this.unEmploye = value;
            }
        }

        public ModeTransport UnModeTransport
        {
            get
            {
                return unModeTransport;
            }

            set
            {
                this.unModeTransport = value;
            }
        }

        public Revendeur UnRevendeur
        {
            get
            {
                return unRevendeur;
            }

            set
            {
                this.unRevendeur = value;
            }
        }

        public List<ProduitCommande> LesProduitCommande
        {
            get
            {
                return this.lesProduitCommande;
            }

            set
            {
                this.lesProduitCommande = value;
            }
        }
    }
}
