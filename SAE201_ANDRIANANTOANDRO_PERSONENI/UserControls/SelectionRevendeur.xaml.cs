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
    public enum ActionRevendeur { Ajouter, Modifier };
    public partial class SelectionRevendeur : UserControl
    {
        public event EventHandler<Revendeur> RevendeurSelectionne;
        public event EventHandler<RevendeurEventArgs> RevendeurActionNecessaire;

        
        public SelectionRevendeur()
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

        private void SelectionnerRevendeur_Click(object sender, RoutedEventArgs e)
        {
            Revendeur unRevendeur = (Revendeur)((Button)sender)?.Tag;
            RevendeurSelectionne?.Invoke(this, unRevendeur);
        }

        private void ModifierRevendeur_Click(object sender, RoutedEventArgs e)
        {
            Revendeur unRevendeur = (Revendeur)(((Button)sender)?.Tag);
            RevendeurActionNecessaire?.Invoke(this, new RevendeurEventArgs(ActionRevendeur.Modifier, unRevendeur));
        }

        private void CreationRevendeur_Click(object sender, RoutedEventArgs e)
        {
            RevendeurActionNecessaire?.Invoke(this, new RevendeurEventArgs(ActionRevendeur.Ajouter, null));
        }

        //classe pour transférer deux informations à la mainWindow (l'action et le revendeur associé)
        public class RevendeurEventArgs : EventArgs
        {
            public Revendeur Revendeur { get; }
            public ActionRevendeur Action { get; }

            public RevendeurEventArgs(ActionRevendeur action, Revendeur revendeur)
            {
                Action = action;
                Revendeur = revendeur;
            }
        }
    }
}
