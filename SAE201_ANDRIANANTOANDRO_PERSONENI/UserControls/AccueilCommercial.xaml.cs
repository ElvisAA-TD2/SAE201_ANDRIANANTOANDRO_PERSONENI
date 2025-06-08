using SAE201_ANDRIANANTOANDRO_PERSONENI.Model;
using System;
using System.Collections.Generic;
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
    /// Logique d'interaction pour AccueilCommercial.xaml
    /// </summary>
    public partial class AccueilCommercial : UserControl
    {
        public AccueilCommercial()
        {
            InitializeComponent();
            dg_produits.Items.Filter += RechercheProduit;
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(dg_produits.ItemsSource).Refresh();
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
