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
    /// Logique d'interaction pour CreationRevendeur.xaml
    /// </summary>
    public partial class FormulaireRevendeur : UserControl
    {
        public event EventHandler<Revendeur> ActionRevendeurEffectuee;
        public FormulaireRevendeur()
        {
            InitializeComponent();
        }

        private void CreationRevendeur_Click(object sender, RoutedEventArgs e)
        {
                Revendeur leRevendeur = new Revendeur(0, tb_raisonSociale.Text, tb_adresseRue.Text, tb_adresseCP.Text, tb_adresseVille.Text);
                ActionRevendeurEffectuee?.Invoke(this, leRevendeur); 
        }
    }
}
