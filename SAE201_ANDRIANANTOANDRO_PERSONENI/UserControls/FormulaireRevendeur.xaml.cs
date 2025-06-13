using SAE201_ANDRIANANTOANDRO_PERSONENI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Transactions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static SAE201_ANDRIANANTOANDRO_PERSONENI.UserControls.CreationCommande;

namespace SAE201_ANDRIANANTOANDRO_PERSONENI.UserControls
{
    /// <summary>
    /// Logique d'interaction pour CreationRevendeur.xaml
    /// </summary>
    public partial class FormulaireRevendeur : UserControl
    {
        public event EventHandler<Revendeur> ActionRevendeurEffectuee;
        public event EventHandler<bool> AnnulationActionRevendeur;
        public int IdRevendeurAModifier { get; set; }
        public FormulaireRevendeur()
        {
            InitializeComponent();
        }

        private void ValidationActionRevendeur_Click(object sender, RoutedEventArgs e)
        {
            if(btn_validation.Content == "Créer")
            {
                bool infoOk = true, cpOk = false;
                if (!Regex.IsMatch(tb_adresseCP.Text, "^[0-9]{5}$"))
                    cpOk=true;
                if ((String.IsNullOrWhiteSpace(tb_raisonSociale.Text)) || (String.IsNullOrWhiteSpace(tb_adresseRue.Text) || (String.IsNullOrWhiteSpace(tb_adresseVille.Text) || cpOk)))
                {
                    MessageBox.Show("Des champs de saisie sont invalides", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                    infoOk = false;
                }
                if (infoOk)
                {
                    Revendeur leRevendeur = new Revendeur(0, tb_raisonSociale.Text, tb_adresseRue.Text, tb_adresseCP.Text, tb_adresseVille.Text);
                    ActionRevendeurEffectuee?.Invoke(this, leRevendeur);
                }
            }
            else
            {
                Revendeur leRevendeur = new Revendeur(this.IdRevendeurAModifier, tb_raisonSociale.Text, tb_adresseRue.Text, tb_adresseCP.Text, tb_adresseVille.Text);
                ActionRevendeurEffectuee?.Invoke(this, leRevendeur);
            }   
        }

        private void AnnulationActionRevendeur_Click(object sender, RoutedEventArgs e)
        {
            AnnulationActionRevendeur?.Invoke(this, true);
        }
    }
}
