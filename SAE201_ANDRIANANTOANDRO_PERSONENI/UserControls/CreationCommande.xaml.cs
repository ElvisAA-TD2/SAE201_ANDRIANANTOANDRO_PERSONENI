using SAE201_ANDRIANANTOANDRO_PERSONENI.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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

namespace SAE201_ANDRIANANTOANDRO_PERSONENI.UserControls
{
    /// <summary>
    /// Logique d'interaction pour CreationCommande.xaml
    /// </summary>
    public partial class CreationCommande : UserControl, INotifyPropertyChanged
    {
        public event EventHandler<Commande> CreationCommandeValidation;
        public event PropertyChangedEventHandler? PropertyChanged;

        public ObservableCollection<ProduitACommande> LesProduitsSelectionnes { get; set; } = new ObservableCollection<ProduitACommande>();

        private ModeTransport modeTransportSelectionne;
        private decimal prixTotal;

        public ModeTransport ModeTransportSelectionne
        {
            get
            {
                return this.modeTransportSelectionne;
            }

            set
            {
                this.modeTransportSelectionne = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ModeTransportSelectionne)));
            }
        }

        public decimal PrixTotal
        {
            get
            {
                return this.prixTotal;
            }

            set
            {
                this.prixTotal = value;
            }
        }

        
        public CreationCommande()
        {
            InitializeComponent();
            dg_produits_trouvés.Items.Filter += RechercheProduit;
            dg_produitsSelectionnes.ItemsSource = LesProduitsSelectionnes;
        }

        private void CreationCommande_Click(object sender, RoutedEventArgs e)
        {
            List<ProduitCommande> lesProduitsCommandes = new List<ProduitCommande>();
            foreach (ProduitACommande unProduitACommande in this.LesProduitsSelectionnes)
                lesProduitsCommandes.Add(new ProduitCommande(0, unProduitACommande.UnProduit, unProduitACommande.QuantiteCommandee));

            Commande commandeACree = new Commande(null, null, this.ModeTransportSelectionne, DateTime.Now, DateTime.Now, lesProduitsCommandes, this.PrixTotal);
            CreationCommandeValidation?.Invoke(this, commandeACree);
        }


        private void SelectionProduit_Click(object sender, RoutedEventArgs e)
        {
            Produit unProduit = (Produit)((Button)sender)?.Tag;

            if (unProduit != null && !LesProduitsSelectionnes.Any(ps => ps.UnProduit == unProduit))
            {
                LesProduitsSelectionnes.Add(new ProduitACommande(unProduit, 0));
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(dg_produits_trouvés.ItemsSource).Refresh();
        }

        private bool RechercheProduit(object obj)
        {
            Produit unProduit = (Produit)obj;

            bool motClefOk = string.IsNullOrWhiteSpace(tb_rechercheParMotClef.Text) ||
                             unProduit.CodeProduit.Contains(tb_rechercheParMotClef.Text, StringComparison.OrdinalIgnoreCase) ||
                             unProduit.NomProduit.Contains(tb_rechercheParMotClef.Text, StringComparison.OrdinalIgnoreCase);

            bool typeOk = string.IsNullOrWhiteSpace(tb_type.Text) ||
                          unProduit.UnType.NomType.Contains(tb_type.Text, StringComparison.OrdinalIgnoreCase);

            bool typePointeOk = string.IsNullOrWhiteSpace(tb_typePointe.Text) ||
                                unProduit.UnTypePointe.NomTypePointe.Contains(tb_typePointe.Text, StringComparison.OrdinalIgnoreCase);

            bool categorieOk = string.IsNullOrWhiteSpace(tb_categorie.Text) ||
                               unProduit.UnType.UneCategorie.NomCategorie.Contains(tb_categorie.Text, StringComparison.OrdinalIgnoreCase);
            
            bool couleurOk = string.IsNullOrEmpty(tb_couleur.Text)
                || unProduit.LesCouleurs.Any(c => c.NomCouleur.IndexOf(tb_couleur.Text, StringComparison.OrdinalIgnoreCase) >= 0);

            return motClefOk && typeOk && typePointeOk && categorieOk && couleurOk;
        }

        private decimal MettreAJourPrixTotal ()
        {
            decimal prixTotal = 0;
            foreach (ProduitACommande unProduitACommande in this.LesProduitsSelectionnes)
                prixTotal += unProduitACommande.SousTotal;
            
            lb_cout_commande.Content = prixTotal.ToString() + " €";
            return prixTotal;
        }


        //class pour avoir les info nécessaire
        public class ProduitACommande : INotifyPropertyChanged
        {
            public Produit UnProduit { get; set; }

            private int quantiteCommandee;

            public ProduitACommande(Produit unProduit, int quantiteCommandee)
            {
                this.UnProduit = unProduit;
                this.QuantiteCommandee = quantiteCommandee;
            }

            public event PropertyChangedEventHandler? PropertyChanged;

            public int QuantiteCommandee
            {
                get 
                { 
                    return this.quantiteCommandee; 
                }
                set
                {
                    this.quantiteCommandee = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(QuantiteCommandee)));

                }
            }

            public decimal SousTotal
            {
                get
                {
                    return this.UnProduit.PrixVente * this.QuantiteCommandee;
                }
            }
        }

        private void Tb_QteCommande_Changed(object sender, TextChangedEventArgs e)
        {
            this.PrixTotal = MettreAJourPrixTotal();
        }

        private void ModeLivraison_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.ModeTransportSelectionne = (ModeTransport)cb_ModeLivraison.SelectedItem;
        }
    }
}
