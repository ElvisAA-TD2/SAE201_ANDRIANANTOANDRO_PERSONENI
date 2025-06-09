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
    /// Logique d'interaction pour MesCommandes.xaml
    /// </summary>
    public partial class MesCommandes : UserControl
    {
        public event EventHandler<Commande> VoirDetailsCommandes;
        public MesCommandes()
        {
            InitializeComponent();
        }

        private void VoirRecapitulatif_Click(object sender, RoutedEventArgs e)
        {
            VoirDetailsCommandes?.Invoke(this, (Commande)((Button)sender).Tag);
        }
    }
}
