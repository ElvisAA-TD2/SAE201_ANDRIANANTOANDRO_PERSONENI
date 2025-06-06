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
    /// Logique d'interaction pour BarDeNavigation.xaml
    /// </summary>
    public partial class BarDeNavigation : UserControl
    {
        public enum Navigation { ListeProduits, CréationCommande, MesCommandes};

        public event EventHandler<Enum> NavigationDemandee;
        public BarDeNavigation()
        {
            InitializeComponent();
        }

        private void ListeDesProduits_Click(object sender, RoutedEventArgs e)
        {
            NavigationDemandee?.Invoke(this, Navigation.ListeProduits);
        }

        private void CreationCommande_Click(object sender, RoutedEventArgs e)
        {
            NavigationDemandee?.Invoke(this, Navigation.CréationCommande);
        }

        private void MesCommandes_Click(object sender, RoutedEventArgs e)
        {
            NavigationDemandee?.Invoke(this, Navigation.MesCommandes);
        }
    }
}
