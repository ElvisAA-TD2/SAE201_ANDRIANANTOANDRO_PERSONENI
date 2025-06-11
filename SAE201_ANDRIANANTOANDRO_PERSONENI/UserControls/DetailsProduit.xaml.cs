using Microsoft.Win32;
using SAE201_ANDRIANANTOANDRO_PERSONENI.Model;
using System;
using System.Collections.Generic;
using System.IO;
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
using static SAE201_ANDRIANANTOANDRO_PERSONENI.UserControls.RecapitulatifCommande;

namespace SAE201_ANDRIANANTOANDRO_PERSONENI.UserControls
{
    /// <summary>
    /// Logique d'interaction pour RecapitulatifProduit.xaml
    /// </summary>
    public partial class DetailsProduit : UserControl
    {
        public event EventHandler<bool> RevenirEnArrièreDemandee;
        public event EventHandler<InformationProduitEventArgs> ModificationRendreInsponibleDemandee;

        public Produit ProduitAAfficher {  get; set; }
        public DetailsProduit()
        {
            InitializeComponent();
        }

        private void RevenirEnArriere_Click(object sender, RoutedEventArgs e)
        {
            RevenirEnArrièreDemandee?.Invoke(this, true);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
            openFileDialog.Filter = "Images|*.jpg;*.png;*.jpeg";

            if (openFileDialog.ShowDialog() == true)
            {
                string imageSource = openFileDialog.FileName;
                string dossierImages = "ImagesProduits";

                if (!Directory.Exists(dossierImages))
                    Directory.CreateDirectory(dossierImages);

                string nouveauNom = System.IO.Path.GetFileName(imageSource);
                string destination = System.IO.Path.Combine(dossierImages, nouveauNom);

                File.Copy(imageSource, destination, true);

                //Devrait etre la mise à jour de la propriété dans ton produit mais je fais encore un test
                string cheminImage = System.IO.Path.Combine(dossierImages, nouveauNom);

                BitmapImage img = new BitmapImage();
                img.BeginInit();
                img.UriSource = new Uri(cheminImage, UriKind.Relative);
                img.CacheOption = BitmapCacheOption.OnLoad;
                img.EndInit();
                image_produit.Source = img;
            }
        }

        private void Btn_rendreIndisponible_Click(object sender, RoutedEventArgs e)
        {
            ModificationRendreInsponibleDemandee?.Invoke(this,new InformationProduitEventArgs(this.ProduitAAfficher, true));
        }

        public void FindProduitByNum(int numProduit, GestionPilot gestionPilot)
        {
            this.ProduitAAfficher = gestionPilot.LesProduits.SingleOrDefault(c => c.NumProduit == numProduit);
            lb_nomProduit.Content = this.ProduitAAfficher.NomProduit;
            lb_quantiteProduit.Content = this.ProduitAAfficher.QteStock;
            lb_typeProduit.Content = this.ProduitAAfficher.UnType.NomType;
            lb_typePointeProduit.Content = this.ProduitAAfficher.UnTypePointe.NomTypePointe;
            lb_prixProduit.Content = this.ProduitAAfficher.PrixVente + "€";
            lb_categorieProduit.Content = this.ProduitAAfficher.UnType.UneCategorie.NomCategorie;
        }

        private void ModifierProduit_Click(object sender, RoutedEventArgs e)
        {
            ModificationRendreInsponibleDemandee?.Invoke(this, new InformationProduitEventArgs(this.ProduitAAfficher, false));
        }

        //classe pour envoyer une info supplémentaire pour voir quel bouton a envoyé l'évènement
        public class InformationProduitEventArgs : EventArgs
        {
            public InformationProduitEventArgs(Produit unProduit, bool rendreIndisponible)
            {
                this.UnProduit = unProduit;
                this.RendreIndisponible = rendreIndisponible;
            }

            public Produit UnProduit { get; set; }
            public bool RendreIndisponible {  get; set; }
        }
    }
}
