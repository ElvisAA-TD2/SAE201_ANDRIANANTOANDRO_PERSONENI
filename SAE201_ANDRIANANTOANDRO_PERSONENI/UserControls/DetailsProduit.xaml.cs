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
    /// Logique d'interaction pour RecapitulatifProduit.xaml
    /// </summary>
    public partial class DetailsProduit : UserControl
    {
        public event EventHandler<bool> RevenirEnArrièreDemandee;
        public DetailsProduit()
        {
            InitializeComponent();
        }

        private void RevenirEnArriere_Click(object sender, RoutedEventArgs e)
        {
            RevenirEnArrièreDemandee?.Invoke(this, true);
        }
    }
}
