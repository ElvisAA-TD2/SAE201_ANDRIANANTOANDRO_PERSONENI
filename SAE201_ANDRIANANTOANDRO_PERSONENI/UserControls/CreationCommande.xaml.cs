using SAE201_ANDRIANANTOANDRO_PERSONENI.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public partial class CreationCommande : UserControl
    {
        public event EventHandler<bool> CreationCommandeValidation;

        private ObservableCollection<Produit> LesProduitsSelectionnes { get; set; } = new ObservableCollection<Produit>();

        public ModeTransport ModeTransportSelectionne
        {
            get
            {
                return this.modeTransportSelectionne;
            }

            set
            {
                this.modeTransportSelectionne = value;
            }
        }

        private ModeTransport modeTransportSelectionne;
        public CreationCommande()
        {
            InitializeComponent();
            dg_produits_trouvés.Items.Filter += RechercheProduit;
            dg_produitsSelectionnes.ItemsSource = LesProduitsSelectionnes;
        }

        private void CreationCommande_Click(object sender, RoutedEventArgs e)
        {
            CreationCommandeValidation?.Invoke(this, true);
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void SelectionProduit_Click(object sender, RoutedEventArgs e)
        {
            Produit unProduit = (Produit)((Button)sender)?.Tag;

            if (unProduit != null && !LesProduitsSelectionnes.Contains(unProduit))
                LesProduitsSelectionnes.Add(unProduit); 
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

            return motClefOk && typeOk && typePointeOk && categorieOk;
        }
    }
}
