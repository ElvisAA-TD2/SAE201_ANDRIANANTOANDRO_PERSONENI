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
    /// Logique d'interaction pour SelectionClient.xaml
    /// </summary>
    public partial class SelectionClient : UserControl
    {
        public SelectionClient()
        {
            InitializeComponent();
            listeRevendeurs.Items.Filter += RechercheRevendeur;
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(listeRevendeurs.ItemsSource).Refresh();
        }

        private bool RechercheRevendeur(object obj)
        {
            if(String.IsNullOrWhiteSpace(tb_rechercheMotClef.Text))
                return true;

            return ((Revendeur)obj).RaisonSociale.Contains(tb_rechercheMotClef.Text, StringComparison.OrdinalIgnoreCase);
        }
    }
}
