using Microsoft.Win32;
using SAE201_ANDRIANANTOANDRO_PERSONENI.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace SAE201_ANDRIANANTOANDRO_PERSONENI.UserControls
{
    /// <summary>
    /// Logique d'interaction pour FormulaireProduit.xaml
    /// </summary>
    public enum ActionProduitEffectue { Modifier, Créer, Annuler}
    public partial class FormulaireProduit : UserControl, INotifyPropertyChanged
    {
        public static MainWindow laMainWindow = (MainWindow)Application.Current.MainWindow;

        public event EventHandler<Produit> ActionProduitDemande;

        public event PropertyChangedEventHandler? PropertyChanged;

        private int indexTypeSelectionne;
        private int indexTypePointeSelectionne;
        private int indexCategorieSelectionnee;
        private Produit produitAModifier;

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
            Produit produitAEnvoye = new Produit(this.ProduitAModifier.NumProduit, "CodeProduit", tb_nomProduit.Text, decimal.Parse(tb_prix.Text),
                int.Parse(tb_qteStock.Text), true,
                laMainWindow.LaGestion.LesTypePointes.FirstOrDefault(tp => tp.NomTypePointe == ((TypePointe)cb_typePointe.SelectedItem).NomTypePointe),
                laMainWindow.LaGestion.LesTypes.FirstOrDefault(t => t.NomType == ((Model.Type)cb_type.SelectedItem).NomType),
                this.ProduitAModifier.LesCouleurs, this.ProduitAModifier.CheminImage);

            if (btn_valider.Content.ToString() == ActionProduitEffectue.Créer.ToString())
            {
                produitAEnvoye.NumProduit = 0;
                produitAEnvoye.CodeProduit = Produit.GenererCodeProduit();
                ActionProduitDemande?.Invoke(this, produitAEnvoye);
            }  
            else
            {
                produitAEnvoye.CodeProduit = this.ProduitAModifier.CodeProduit;
                ActionProduitDemande?.Invoke(this, produitAEnvoye);
            }
                
        }

        private void CheckBox_Couleur_Checked(object sender, RoutedEventArgs e)
        {
            if(sender is CheckBox checkBox && checkBox.DataContext is Couleur couleur)
            {
                if (ProduitAModifier.LesCouleurs == null)
                    ProduitAModifier.LesCouleurs = new List<Couleur>();

                else if (ProduitAModifier != null && !ProduitAModifier.LesCouleurs.Contains(couleur))
                {
                    ProduitAModifier.LesCouleurs.Add(couleur);
                }
                
            }
        }

        private void CheckBox_Couleur_Unchecked(object sender, RoutedEventArgs e)
        {
            if (sender is CheckBox checkBox && checkBox.DataContext is Couleur couleur)
            {
                if (ProduitAModifier != null)
                {
                    ProduitAModifier.LesCouleurs.Remove(couleur);
                }
            }
        }

        private void ModifierImage_Click(object sender, RoutedEventArgs e)
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


                string cheminImage = System.IO.Path.Combine(dossierImages, nouveauNom);
                this.ProduitAModifier.CheminImage = cheminImage;

                BitmapImage img = new BitmapImage();
                img.BeginInit();
                img.UriSource = new Uri(cheminImage, UriKind.Relative);
                img.CacheOption = BitmapCacheOption.OnLoad;
                img.EndInit();
                image_produit.Source = img;
            }
        }



        public int IndexTypeSelectionne
        {
            get
            {
                return this.indexTypeSelectionne;
            }

            set
            {
                this.indexTypeSelectionne = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IndexTypeSelectionne)));
            }
        }

        public int IndexTypePointeSelectionne
        {
            get
            {
                return this.indexTypePointeSelectionne;
            }

            set
            {
                this.indexTypePointeSelectionne = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IndexTypePointeSelectionne)));
            }
        }

        public int IndexCategorieSelectionnee
        {
            get
            {
                return this.indexCategorieSelectionnee;
            }

            set
            {
                this.indexCategorieSelectionnee = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IndexCategorieSelectionnee)));
            }
        }

        public Produit ProduitAModifier
        {
            get
            {
                return this.produitAModifier;
            }

            set
            {
                this.produitAModifier = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ProduitAModifier)));
            }
        }

        private void Cb_TypeProduit_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cb_type.SelectedItem is Model.Type type)
            {
                ProduitAModifier.UnType = type;

                tb_categorieProduit.Text = type.UneCategorie.NomCategorie;
            }
        }
    }
}
