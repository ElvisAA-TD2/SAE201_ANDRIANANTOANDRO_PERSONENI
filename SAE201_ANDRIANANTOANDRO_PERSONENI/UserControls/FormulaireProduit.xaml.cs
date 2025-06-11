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
    /// Logique d'interaction pour FormulaireProduit.xaml
    /// </summary>
    public enum ActionProduitEffectue { Modifier, Créer, Annuler}
    public partial class FormulaireProduit : UserControl
    {
        public event EventHandler<ActionProduitEffectue> ActionProduitDemande;
        public int IdProduitAModifier { get; set; }
        public FormulaireProduit()
        {
            InitializeComponent();
        }

        private void Annuler_Click(object sender, RoutedEventArgs e)
        {
            ActionProduitDemande?.Invoke(this, ActionProduitEffectue.Annuler);
        }

        private void Validation_Click(object sender, RoutedEventArgs e)
        {
            if (btn_valider.Content == ActionProduitEffectue.Créer.ToString())
                ActionProduitDemande?.Invoke(this, ActionProduitEffectue.Créer);
            else 
                ActionProduitDemande?.Invoke(this, ActionProduitEffectue.Modifier);
        }
    }
}
