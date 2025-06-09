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
    /// Logique d'interaction pour RecapitulatifCommande.xaml
    /// </summary>
    public enum ActionCommande { Supprimer, Annuler}
    public partial class RecapitulatifCommande : UserControl
    {
        public event EventHandler<RecapitulatifCommandeEventArgs> ActionCommandeDemandee;
        public Commande CommandeAAfficher {  get; set; }
        public RecapitulatifCommande()
        {
            InitializeComponent();
            
        }

        private void RevenirEnArriere_Click(object sender, RoutedEventArgs e)
        {
            ActionCommandeDemandee?.Invoke(this, new RecapitulatifCommandeEventArgs(ActionCommande.Annuler, null));
        }

        private void SupprimerCommande_Click(object sender, RoutedEventArgs e)
        {
            ActionCommandeDemandee?.Invoke(this, new RecapitulatifCommandeEventArgs(ActionCommande.Supprimer, this.CommandeAAfficher));
        }

        public void FindCommandeByNumCommande (int numCommande, GestionPilot gestionPilot)
        {
            this.CommandeAAfficher = gestionPilot.LesCommandes.SingleOrDefault(c => c.NumCommande == numCommande);
            dg_produitCommandes.ItemsSource = this.CommandeAAfficher.LesProduitCommande;
            lb_raisonSociale_revendeur.Content = this.CommandeAAfficher.UnRevendeur.RaisonSociale;
            lb_dateCreation_commande.Content = this.CommandeAAfficher.DateCommande.ToLongDateString().Substring(0, 1).ToUpper() + CommandeAAfficher.DateCommande.ToLongDateString().Substring(1).ToLower();
            lb_modeTransport_commande.Content = this.CommandeAAfficher.UnModeTransport.NomModeTransport;
            lb_prixTotal_commande.Content = this.CommandeAAfficher.PrixTotal.ToString()+ " €";

        }

        //classe pour transférer deux informations à la mainWindow (l'action et la commande associé)
        public class RecapitulatifCommandeEventArgs : EventArgs
        {
            public Commande UneCommande { get; }
            public ActionCommande UneAction { get; }

            public RecapitulatifCommandeEventArgs(ActionCommande action, Commande commande)
            {
                UneAction = action;
                UneCommande = commande;
            }
        }
    }
}
