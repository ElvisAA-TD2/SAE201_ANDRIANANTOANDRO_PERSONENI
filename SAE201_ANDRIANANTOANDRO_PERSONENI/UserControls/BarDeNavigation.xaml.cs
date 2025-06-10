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
    public enum Navigation { ListeProduits, CréationCommande, MesCommandes , Deconnexion};
    public partial class BarDeNavigation : UserControl
    {
        public event EventHandler<Navigation> NavigationDemandee;
        public static readonly string FOND_BOUTON_SELECTIONNE = "#FF3B5D68";
        public static readonly string FOND_BOUTON_NON_SELECTIONNE = "#FFADC9DC";
        public static readonly string COULEUR_TEXTE_BOUTON_SELECTIONNE = "#FFFFFFFF";
        public static readonly string COULEUR_TEXTE_BOUTON_NONSELECTIONNE = "#FF3B5D68";
        public BarDeNavigation()
        {
            InitializeComponent();
        }

        private void ListeDesProduits_Click(object sender, RoutedEventArgs e)
        {
            BarDeNavigation.ChangementFondEtCouleurBouton(listeDesProduits_btn, BarDeNavigation.FOND_BOUTON_SELECTIONNE, BarDeNavigation.COULEUR_TEXTE_BOUTON_SELECTIONNE);
            BarDeNavigation.ChangementFondEtCouleurBouton(mesCommandes_btn, BarDeNavigation.FOND_BOUTON_NON_SELECTIONNE, BarDeNavigation.COULEUR_TEXTE_BOUTON_NONSELECTIONNE);
            BarDeNavigation.ChangementFondEtCouleurBouton(creationCommande_btn, BarDeNavigation.FOND_BOUTON_NON_SELECTIONNE, BarDeNavigation.COULEUR_TEXTE_BOUTON_NONSELECTIONNE);


            NavigationDemandee?.Invoke(this, Navigation.ListeProduits);
        }

        private void CreationCommande_Click(object sender, RoutedEventArgs e)
        {
            BarDeNavigation.ChangementFondEtCouleurBouton(listeDesProduits_btn, BarDeNavigation.FOND_BOUTON_NON_SELECTIONNE, BarDeNavigation.COULEUR_TEXTE_BOUTON_NONSELECTIONNE);
            BarDeNavigation.ChangementFondEtCouleurBouton(mesCommandes_btn, BarDeNavigation.FOND_BOUTON_NON_SELECTIONNE, BarDeNavigation.COULEUR_TEXTE_BOUTON_NONSELECTIONNE);
            BarDeNavigation.ChangementFondEtCouleurBouton(creationCommande_btn, BarDeNavigation.FOND_BOUTON_SELECTIONNE, BarDeNavigation.COULEUR_TEXTE_BOUTON_SELECTIONNE);


            NavigationDemandee?.Invoke(this, Navigation.CréationCommande);
        }

        private void MesCommandes_Click(object sender, RoutedEventArgs e)
        {
            BarDeNavigation.ChangementFondEtCouleurBouton(listeDesProduits_btn, BarDeNavigation.FOND_BOUTON_NON_SELECTIONNE, BarDeNavigation.COULEUR_TEXTE_BOUTON_NONSELECTIONNE);
            BarDeNavigation.ChangementFondEtCouleurBouton(mesCommandes_btn, BarDeNavigation.FOND_BOUTON_SELECTIONNE, BarDeNavigation.COULEUR_TEXTE_BOUTON_SELECTIONNE);
            BarDeNavigation.ChangementFondEtCouleurBouton(creationCommande_btn, BarDeNavigation.FOND_BOUTON_NON_SELECTIONNE, BarDeNavigation.COULEUR_TEXTE_BOUTON_NONSELECTIONNE);


            NavigationDemandee?.Invoke(this, Navigation.MesCommandes);
        }

        public static void ChangementFondEtCouleurBouton (Button button, string couleurFond, string couleurTexte)
        {
            button.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom(couleurTexte));
            button.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom(couleurFond));
        }

        private void DeconnexionBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationDemandee?.Invoke(this, Navigation.Deconnexion);
        }
    }
}
