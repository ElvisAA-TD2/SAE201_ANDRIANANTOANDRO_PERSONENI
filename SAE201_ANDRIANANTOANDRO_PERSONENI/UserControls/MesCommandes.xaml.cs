using SAE201_ANDRIANANTOANDRO_PERSONENI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
            dg_mesCommandes.Items.Filter = RechercheCommandes;
        }

        private bool RechercheCommandes(object obj)
        {
            if (obj is not Commande uneCommande)
                return false;

            string filtreRaisonSociale = tb_raisonSociale_revendeur.Text.Trim();
            string filtreDate = tb_dateCreation_commande.Text.Trim();

            if (String.IsNullOrWhiteSpace(filtreRaisonSociale) && String.IsNullOrWhiteSpace(filtreDate))
                return true;

            bool raisonSocialeOk = true;
            if (!string.IsNullOrWhiteSpace(filtreRaisonSociale))
                raisonSocialeOk = uneCommande.UnRevendeur.RaisonSociale.Contains(filtreRaisonSociale, StringComparison.OrdinalIgnoreCase);

            bool dateCommandeOk = true;
            if (!string.IsNullOrWhiteSpace(filtreDate))
            {
                if (Regex.IsMatch(filtreDate, "^[0-9]{4}$"))
                {
                    dateCommandeOk = uneCommande.DateCommande.Year.ToString() == filtreDate;
                }
                else if (Regex.IsMatch(filtreDate, "^[0-9]{2}/[0-9]{4}$"))
                {
                    string mois = uneCommande.DateCommande.Month.ToString("D2");
                    string annee = uneCommande.DateCommande.Year.ToString();
                    dateCommandeOk = $"{mois}/{annee}" == filtreDate;
                }
                else
                {
                    dateCommandeOk = uneCommande.DateCommande.ToShortDateString() == filtreDate;
                }
            }

            return raisonSocialeOk && dateCommandeOk;
        }


        private void VoirRecapitulatif_Click(object sender, RoutedEventArgs e)
        {
            VoirDetailsCommandes?.Invoke(this, (Commande)((Button)sender).Tag);
        }


        private void TextBox_Changed(object sender, TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(dg_mesCommandes.ItemsSource).Refresh();
        }
    }
}
