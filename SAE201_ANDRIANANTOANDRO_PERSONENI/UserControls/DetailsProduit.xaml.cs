using Microsoft.Win32;
using SAE201_ANDRIANANTOANDRO_PERSONENI.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static SAE201_ANDRIANANTOANDRO_PERSONENI.UserControls.RecapitulatifCommande;

namespace SAE201_ANDRIANANTOANDRO_PERSONENI.UserControls
{
    /// <summary>
    /// Logique d'interaction pour RecapitulatifProduit.xaml
    /// </summary>
    public partial class DetailsProduit : UserControl, INotifyPropertyChanged
    {
        public event EventHandler<bool> RevenirEnArrièreDemandee;
        public event EventHandler<InformationProduitEventArgs> ModificationRendreInsponibleDemandee;

        public static readonly string COULEUR_DISPONIBLE = "#FF7ECF5D";
        public static readonly string COULEUR_INDISPONIBLE = "#FFC14347";

        public event PropertyChangedEventHandler? PropertyChanged;

        private Produit produitAAfficher;

        public Produit ProduitAAfficher
        {
            get
            {
                return this.produitAAfficher;
            }

            set
            {
                this.produitAAfficher = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ProduitAAfficher)));
            }
        }

        public DetailsProduit()
        {
            InitializeComponent();
        }

        private void RevenirEnArriere_Click(object sender, RoutedEventArgs e)
        {
            RevenirEnArrièreDemandee?.Invoke(this, true);
        }

        private void Btn_rendreIndisponible_Click(object sender, RoutedEventArgs e)
        {
            ModificationRendreInsponibleDemandee?.Invoke(this,new InformationProduitEventArgs(this.ProduitAAfficher, true));
        }

        public void FindProduitByNum(int numProduit, GestionPilot gestionPilot)
        {
            this.ProduitAAfficher = gestionPilot.LesProduits.SingleOrDefault(c => c.NumProduit == numProduit);
            lb_nomProduit.Content = this.ProduitAAfficher.NomProduit;
            lb_quantiteProduit.Content = this.ProduitAAfficher.QteStock;
            lb_typeProduit.Content = this.ProduitAAfficher.UnType.NomType;
            lb_typePointeProduit.Content = this.ProduitAAfficher.UnTypePointe.NomTypePointe;
            lb_prixProduit.Content = this.ProduitAAfficher.PrixVente + "€";
            lb_categorieProduit.Content = this.ProduitAAfficher.UnType.UneCategorie.NomCategorie;
            lb_couleurProduit.Content = this.ProduitAAfficher.NomCouleurConcatene;
            image_produit.Source = MainWindow.AfficherImage(this.ProduitAAfficher.CheminImage);
        }

        private void ModifierProduit_Click(object sender, RoutedEventArgs e)
        {
            ModificationRendreInsponibleDemandee?.Invoke(this, new InformationProduitEventArgs(this.ProduitAAfficher, false));
        }

        //classe pour envoyer une info supplémentaire pour voir quel bouton a envoyé l'évènement
        public class InformationProduitEventArgs : EventArgs
        {
            public InformationProduitEventArgs(Produit unProduit, bool rendreIndisponible)
            {
                this.UnProduit = unProduit;
                this.RendreIndisponible = rendreIndisponible;
            }

            public Produit UnProduit { get; set; }
            public bool RendreIndisponible {  get; set; }
        }
    }
}
