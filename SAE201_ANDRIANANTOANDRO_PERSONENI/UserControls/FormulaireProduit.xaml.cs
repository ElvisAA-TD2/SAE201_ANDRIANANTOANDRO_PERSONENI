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
    /// Logique d'interaction pour FormulaireProduit.xaml
    /// </summary>
    public enum ActionProduitEffectue { Modifier, Créer, Annuler}
    public partial class FormulaireProduit : UserControl
    {
        public event EventHandler<Produit> ActionProduitDemande;
        public static MainWindow laMainWindow = (MainWindow)Application.Current.MainWindow;
        public Produit ProduitAModifier { get; set; }
        public FormulaireProduit()
        {
            InitializeComponent();
        }

        private void Annuler_Click(object sender, RoutedEventArgs e)
        {
            ActionProduitDemande?.Invoke(this, null);
        }

        private void Validation_Click(object sender, RoutedEventArgs e)
        {
           /* Produit produitAEnvoye = new Produit(this.ProduitAModifier.NumProduit, this.ProduitAModifier.CodeProduit,
                tb_nomProduit.Text, decimal.Parse(tb_prix.Text), int.Parse(tb_qteStock.Text), this.ProduitAModifier.Disponible,
                laMainWindow.LaGestion.LesTypePointes.FirstOrDefault(tp => tp.NomTypePointe == tb_typePointe.Text), 
                laMainWindow.LaGestion.LesTypes.FirstOrDefault(t => t.NomType == tb_type.Text), null);*/


            /*if (btn_valider.Content == ActionProduitEffectue.Créer.ToString())
                ActionProduitDemande?.Invoke(this, ActionProduitEffectue.Créer);
            else
                ActionProduitDemande?.Invoke(this, ActionProduitEffectue.Modifier);*/
        }
    }
}
