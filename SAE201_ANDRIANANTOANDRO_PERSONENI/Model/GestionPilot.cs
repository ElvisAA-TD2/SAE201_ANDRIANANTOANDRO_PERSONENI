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
        private ObservableCollection<Type> lesType;
        private ObservableCollection<Categorie> lesCategories;
        private ObservableCollection<CouleurProduit> lesCouleurProduits;
        private ObservableCollection<Commande> lesCommandes;
        private ObservableCollection<Revendeur> lesRevendeurs;
        private ObservableCollection<Employe> lesEmploye;

        public GestionPilot(string nom)
        {
            this.LesCategories = new ObservableCollection<Categorie>(new Categorie().FindAll());
            this.LesTypePointes = new ObservableCollection<TypePointe>(new TypePointe().FindAll());
            this.LesType = new ObservableCollection<Type>(new Type().FindAll(this.lesCategories));
            this.LesCouleurProduits = new ObservableCollection<CouleurProduit>(new CouleurProduit().FindAll());
            this.LesProduits = new ObservableCollection<Produit>(new Produit().FindAll(this.LesTypePointes,this.LesType,this.LesCouleurProduits));
            this.LesCommandes = lesCommandes;
            this.LesRevendeurs = lesRevendeurs;
            this.LesEmploye = lesEmploye;
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

        public ObservableCollection<Type> LesType
        {
            get
            {
                return lesType;
            }

            set
            {
                lesType = value;
            }
        }

        public ObservableCollection<CouleurProduit> LesCouleurProduits
        {
            get
            {
                return this.lesCouleurProduits;
            }

            set
            {
                this.lesCouleurProduits = value;
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
    }
}
