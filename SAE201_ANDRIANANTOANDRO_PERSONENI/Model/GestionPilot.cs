using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAE201_ANDRIANANTOANDRO_PERSONENI.Model
{
    public class GestionPilot
    {
        private string nom;
        private ObservableCollection<Produit> lesProduits;
        private ObservableCollection<TypePointe> lesTypePointes;
        private ObservableCollection<Type> lesTypes;
        private ObservableCollection<Categorie> lesCategories;
        private ObservableCollection<Couleur> lesCouleurs;
        private ObservableCollection<Commande> lesCommandes;
        private ObservableCollection<Revendeur> lesRevendeurs;
        private ObservableCollection<Employe> lesEmploye;
        private ObservableCollection<ModeTransport> lesModeTransports;
        private ObservableCollection<ProduitCommande> lesProduitCommandes;
        private ObservableCollection<Role> lesRoles;
        private ObservableCollection<CouleurProduit> lesCouleursProduits;

        public GestionPilot(string nom, int numRoleEmploye, int numEmploye)
        {
            this.LesModeTransports = new ObservableCollection<ModeTransport>(new ModeTransport().FindAll());
            this.LesCategories = new ObservableCollection<Categorie>(new Categorie().FindAll());
            this.LesTypePointes = new ObservableCollection<TypePointe>(new TypePointe().FindAll());
            this.LesTypes = new ObservableCollection<Type>(new Type().FindAll(this));
            this.LesCouleurs = new ObservableCollection<Couleur>(new Couleur().FindAll());
            this.lesCouleursProduits = new ObservableCollection<CouleurProduit>(new CouleurProduit().FindAll());
            this.LesProduits = new ObservableCollection<Produit>(new Produit().FindAll(this, numRoleEmploye));
            this.LesRevendeurs = new ObservableCollection<Revendeur>(new Revendeur().FindAll());   
            this.LesRoles = new ObservableCollection<Role>(new Role().FindAll());
            this.LesEmploye = new ObservableCollection<Employe>(new Employe().FindAll(this));
            this.LesProduitCommandes = new ObservableCollection<ProduitCommande>(new ProduitCommande().FindAll(this));
            this.LesCommandes = new ObservableCollection<Commande>(new Commande().FindAll(this, numEmploye));
        }

        public ObservableCollection<Produit> LesProduits
        {
            get
            {
                return lesProduits;
            }

            set
            {
                lesProduits = value;
            }
        }

        public ObservableCollection<Commande> LesCommandes
        {
            get
            {
                return lesCommandes;
            }

            set
            {
                lesCommandes = value;
            }
        }

        public ObservableCollection<Revendeur> LesRevendeurs
        {
            get
            {
                return lesRevendeurs;
            }

            set
            {
                lesRevendeurs = value;
            }
        }

        public ObservableCollection<Employe> LesEmploye
        {
            get
            {
                return this.lesEmploye;
            }

            set
            {
                this.lesEmploye = value;
            }
        }

        public ObservableCollection<TypePointe> LesTypePointes
        {
            get
            {
                return this.lesTypePointes;
            }

            set
            {
                this.lesTypePointes = value;
            }
        }

        public ObservableCollection<Type> LesTypes
        {
            get
            {
                return lesTypes;
            }

            set
            {
                lesTypes = value;
            }
        }

        public string Nom
        {
            get
            {
                return this.nom;
            }

            set
            {
                this.nom = value;
            }
        }

        public ObservableCollection<Categorie> LesCategories
        {
            get
            {
                return this.lesCategories;
            }

            set
            {
                this.lesCategories = value;
            }
        }

        public ObservableCollection<ModeTransport> LesModeTransports
        {
            get
            {
                return this.lesModeTransports;
            }

            set
            {
                this.lesModeTransports = value;
            }
        }

        public ObservableCollection<ProduitCommande> LesProduitCommandes
        {
            get
            {
                return this.lesProduitCommandes;
            }

            set
            {
                this.lesProduitCommandes = value;
            }
        }

        public ObservableCollection<Role> LesRoles
        {
            get
            {
                return this.lesRoles;
            }

            set
            {
                this.lesRoles = value;
            }
        }

        public ObservableCollection<CouleurProduit> LesCouleursProduits
        {
            get
            {
                return this.lesCouleursProduits;
            }

            set
            {
                this.lesCouleursProduits = value;
            }
        }

        public ObservableCollection<Couleur> LesCouleurs
        {
            get
            {
                return this.lesCouleurs;
            }

            set
            {
                this.lesCouleurs = value;
            }
        }
    }
}
