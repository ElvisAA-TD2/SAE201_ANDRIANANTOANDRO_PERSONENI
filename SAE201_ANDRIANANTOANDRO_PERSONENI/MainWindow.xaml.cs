
﻿using System;
using SAE201_ANDRIANANTOANDRO_PERSONENI.UserControls;

using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


using static SAE201_ANDRIANANTOANDRO_PERSONENI.UserControls.BarDeNavigation;
using SAE201_ANDRIANANTOANDRO_PERSONENI.Model;
using System.Runtime.CompilerServices;
using System.IO;




namespace SAE201_ANDRIANANTOANDRO_PERSONENI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public GestionPilot LaGestion { get; set; }
        private BarDeNavigation UcBarDeNavigation { get; set; }
        private CreationCommande UcCreationCommande { get; set; }
        private AccueilCommercial UcAccueilCommercial { get; set; }
        private SelectionRevendeur UcSelectionRevendeur { get; set; }
        private Authentification UcAuthentification { get; set; }
        private FormulaireRevendeur UcFormulaireRevendeur { get; set; }
        private DetailsProduit UcDetailsProduit { get; set; }

        public MainWindow()
        {
            ChargeData();
            InitializeComponent();
            this.UcAuthentification = new Authentification();
            this.UcBarDeNavigation = new BarDeNavigation();
            this.UcCreationCommande = new CreationCommande();
            this.UcSelectionRevendeur = new SelectionRevendeur();
            this.UcAccueilCommercial = new AccueilCommercial();
            this.UcFormulaireRevendeur = new FormulaireRevendeur();
            this.UcDetailsProduit = new DetailsProduit();


            this.UcAccueilCommercial.VoirDetailProduitDemande += AfficherDetailsProduit;
            this.UcAuthentification.AuthentificationReussi += SeConnecter_Reussi;
            this.UcBarDeNavigation.NavigationDemandee += BarDeNavigation_NavigationDemandee;
            this.UcCreationCommande.CreationCommandeValidation += CreationCommande_VersSelectionClient;
            this.UcSelectionRevendeur.RevendeurActionNecessaire += SelectionRevendeur_VersActionRevendeur;
            this.UcFormulaireRevendeur.ActionRevendeurEffectuee += ActionRevendeur;
            this.UcFormulaireRevendeur.AnnulationActionRevendeur += AnnulationActionRevendeur;
            this.UcDetailsProduit.RevenirEnArrièreDemandee += RetourEnArrièreVenantVenantDeDétails;

            conteneur_authentification.Content = this.UcAuthentification;

            if (File.Exists("ImagesProduits/StyloBleu.jpg"))
            {
                var img = new BitmapImage();
                img.BeginInit();
                img.UriSource = new Uri("ImagesProduits/StyloBleu.jpg", UriKind.Relative);
                img.CacheOption = BitmapCacheOption.OnLoad;
                img.EndInit();
                this.UcDetailsProduit.image_produit.Source = img;
            }
            else
            {
                MessageBox.Show("Image introuvable !");
            }

        }

        private void RetourEnArrièreVenantVenantDeDétails(object sender, bool reponse)
        {
            if (reponse)
                conteneur_principal.Content = this.UcAccueilCommercial;
        }

        private void AfficherDetailsProduit(object sender, Produit leProduitADetaille)
        {
            if(leProduitADetaille != null)
            {
                this.UcDetailsProduit.lb_nomProduit.Content = leProduitADetaille.NomProduit;
                this.UcDetailsProduit.lb_categorieProduit.Content = leProduitADetaille.UnType.UneCategorie.NomCategorie;
                this.UcDetailsProduit.lb_typeProduit.Content = leProduitADetaille.UnType.NomType;
                this.UcDetailsProduit.lb_typePointeProduit.Content = leProduitADetaille.UnTypePointe.NomTypePointe;
                this.UcDetailsProduit.lb_prixProduit.Content = leProduitADetaille.PrixVente.ToString() + " €";
                //A modifier
                this.UcDetailsProduit.lb_couleurProduit.Content = leProduitADetaille.LesCouleurProduit.Count.ToString();
                this.UcDetailsProduit.lb_quantiteProduit.Content = leProduitADetaille.QteStock.ToString();
                AfficherImage("ImagesProduits/StyloBleu.jpg");
                


                conteneur_principal.Content = this.UcDetailsProduit;
            }    
        }

        private void AnnulationActionRevendeur(object sender, bool reponse)
        {
            if(reponse)
                conteneur_principal.Content = this.UcSelectionRevendeur;
        }

        public void ChargeData()
        {
            try
            {
                LaGestion = new GestionPilot("gestion pilot");
                this.DataContext = LaGestion;
            }
            catch (Exception ex)
            {
                LogError.Log(ex, "Erreur SQL");
                MessageBox.Show("Problème lors de récupération des données, veuillez consulter votre admin");
                Application.Current.Shutdown();
            }
        }

        private void BarDeNavigation_NavigationDemandee(object sender, Navigation page)
        {
            switch (page)
            {
                case Navigation.ListeProduits:
                    conteneur_principal.Content = this.UcAccueilCommercial;
                    break;

                case Navigation.CréationCommande:
                    conteneur_principal.Content = this.UcCreationCommande;
                    break;
                case Navigation.MesCommandes:
                    conteneur_principal.Content = this.UcAuthentification;
                    break;
            }
        }

        private void SeConnecter_Reussi (object sender, bool reponse)
        {
            if(reponse)
            {
                conteneur_authentification.Visibility = Visibility.Collapsed;
                conteneur_authentification.Content = null;

                conteneur_haut.Visibility = Visibility.Visible;
                scrollViewer_conteneur_principal.Visibility = Visibility.Visible;

                conteneur_haut.Children.Add(this.UcBarDeNavigation);
                conteneur_principal.Content = this.UcAccueilCommercial;

            }
                
        }

        private void CreationCommande_VersSelectionClient(object sender, bool creationCommande)
        {
            if (creationCommande)
                conteneur_principal.Content = this.UcSelectionRevendeur;
        }

        private void SelectionRevendeur_VersActionRevendeur (object sender, SelectionRevendeur.RevendeurEventArgs actionRevendeur)
        {
            switch (actionRevendeur.Action)
            {
                case UserControls.ActionRevendeur.Ajouter:
                    this.UcFormulaireRevendeur.label_titre.Content = "Création revendeur";
                    this.UcFormulaireRevendeur.btn_validation.Content = "Créer";

                    this.UcFormulaireRevendeur.tb_raisonSociale.Text = "";
                    this.UcFormulaireRevendeur.tb_adresseRue.Text = "";
                    this.UcFormulaireRevendeur.tb_adresseCP.Text = ""   ;
                    this.UcFormulaireRevendeur.tb_adresseVille.Text = "";
                    break;

                case UserControls.ActionRevendeur.Modifier:
                    this.UcFormulaireRevendeur.label_titre.Content = "Modification revendeur";
                    this.UcFormulaireRevendeur.btn_validation.Content = "Modifier";

                    this.UcFormulaireRevendeur.IdRevendeurAModifier = actionRevendeur.Revendeur.NumRevendeur;                  
                    this.UcFormulaireRevendeur.tb_raisonSociale.Text = actionRevendeur.Revendeur.RaisonSociale;
                    this.UcFormulaireRevendeur.tb_adresseRue.Text = actionRevendeur.Revendeur.AdresseRue;
                    this.UcFormulaireRevendeur.tb_adresseCP.Text = actionRevendeur.Revendeur.AdresseCP;
                    this.UcFormulaireRevendeur.tb_adresseVille.Text = actionRevendeur.Revendeur.AdresseVille;
                    break;
                    
            };

            conteneur_principal.Content = this.UcFormulaireRevendeur;
        }

        private void ActionRevendeur (object sender, Revendeur unRevendeur)
        {
            if( (((FormulaireRevendeur)sender).btn_validation.Content == "Créer"))
            {
                unRevendeur.NumRevendeur = unRevendeur.Create();

                this.LaGestion.LesRevendeurs.Add(unRevendeur);
            }
            else
            {
                Revendeur revendeurAModifie = this.LaGestion.LesRevendeurs.FirstOrDefault(r => r.NumRevendeur == unRevendeur.NumRevendeur );
                revendeurAModifie.RaisonSociale = unRevendeur.RaisonSociale;
                revendeurAModifie.AdresseRue = unRevendeur.AdresseRue;
                revendeurAModifie.AdresseCP = unRevendeur.AdresseCP;
                revendeurAModifie.AdresseVille = unRevendeur.AdresseVille;

                revendeurAModifie.Update();
            }
            conteneur_principal.Content = this.UcSelectionRevendeur;
            
        }

        private void AfficherImage (string cheminImage)
        {
            if (File.Exists(cheminImage))
            {
                var img = new BitmapImage();
                img.BeginInit();
                img.UriSource = new Uri(cheminImage, UriKind.Relative);
                img.CacheOption = BitmapCacheOption.OnLoad;
                img.EndInit();
                this.UcDetailsProduit.image_produit.Source = img;
            }
            else
            {
                MessageBox.Show("Image introuvable !");
            }
        }

    }
}