
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
using static SAE201_ANDRIANANTOANDRO_PERSONENI.UserControls.RecapitulatifCommande;
using static SAE201_ANDRIANANTOANDRO_PERSONENI.UserControls.Authentification;
using System.ComponentModel;




namespace SAE201_ANDRIANANTOANDRO_PERSONENI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private string connectionString;
        Commande CommandeACree { get; set; }
        public GestionPilot LaGestion { get; set; }
        private BarDeNavigation UcBarDeNavigation { get; set; }
        private CreationCommande UcCreationCommande { get; set; }
        private AccueilCommercial UcAccueilCommercial { get; set; }
        private SelectionRevendeur UcSelectionRevendeur { get; set; }
        private Authentification UcAuthentification { get; set; }
        private FormulaireRevendeur UcFormulaireRevendeur { get; set; }
        private DetailsProduit UcDetailsProduit { get; set; }
        private MesCommandes UcMesCommandes { get; set; }
        private RecapitulatifCommande UcRecapitulatifCommande { get; set; }
        private FormulaireProduit UcFormulaireProduit { get; set; }

        public string ConnectionString
        {
            get
            {
                return this.connectionString;
            }

            set
            {
                this.connectionString = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ConnectionString)));
            }
        }

        Employe UtilisateurConnecte = null;

        
        

        public event PropertyChangedEventHandler? PropertyChanged;


        public MainWindow()
        {
            //ChargeData();
            InitializeComponent();
            this.UcAuthentification = new Authentification();
            this.UcBarDeNavigation = new BarDeNavigation();
            this.UcCreationCommande = new CreationCommande();
            this.UcSelectionRevendeur = new SelectionRevendeur();
            this.UcAccueilCommercial = new AccueilCommercial();
            this.UcFormulaireRevendeur = new FormulaireRevendeur();
            this.UcDetailsProduit = new DetailsProduit();
            this.UcMesCommandes = new MesCommandes();
            this.UcRecapitulatifCommande = new RecapitulatifCommande();
            this.UcRecapitulatifCommande = new RecapitulatifCommande();
            this.UcFormulaireProduit = new FormulaireProduit(); 


            this.UcAccueilCommercial.VoirDetailProduitDemande += AfficherDetailsProduit;
            this.UcAuthentification.AuthentificationReussiAvecInformationConnexion += SeConnecter_Reussi;
            this.UcBarDeNavigation.NavigationDemandee += BarDeNavigation_NavigationDemandee;
            //this.UcCreationCommande.CreationCommandeValidation += CreationCommande_VersSelectionClient;
            this.UcSelectionRevendeur.RevendeurActionNecessaire += SelectionRevendeur_VersActionRevendeur;
            this.UcFormulaireRevendeur.ActionRevendeurEffectuee += ActionRevendeur;
            this.UcFormulaireRevendeur.AnnulationActionRevendeur += AnnulationActionRevendeur;
            this.UcDetailsProduit.RevenirEnArrièreDemandee += RetourEnArrièreVenantVenantDeDétails;
            this.UcMesCommandes.VoirDetailsCommandes += DetailsCommandeDemandee;
            this.UcRecapitulatifCommande.ActionCommandeDemandee += ActionCommande;
            this.UcDetailsProduit.RendreIndisponible += RendreIndisponibleDemandee;
            this.UcCreationCommande.ChoixModeDeLivraison += ChoixModeDeLivraisonDemandee;
            this.UcSelectionRevendeur.RevendeurSelectionne += SelectionRevendeurDemandee;

            conteneur_authentification.Content = this.UcAuthentification;
        }

        private void ChoixModeDeLivraisonDemandee(object? sender, ModeTransport modeTransportSelectionnee)
        {
            this.CommandeACree.UnModeTransport = modeTransportSelectionnee;
        }

        private void SelectionRevendeurDemandee(object? sender, Revendeur revendeurSelectionnee)
        {
           // revendeurSelectionnee.
        }

        private void RendreIndisponibleDemandee(object sender, Produit produitSelectionnee)
        {
            produitSelectionnee.RendreIndisponible();
            conteneur_principal.Content = this.UcAccueilCommercial;
        }

        private void DetailsCommandeDemandee(object sender, Commande commande)
        {
            this.UcRecapitulatifCommande.FindCommandeByNumCommande(commande.NumCommande, this.LaGestion);

            conteneur_principal.Content = this.UcRecapitulatifCommande;
        }

        private void ActionCommande(object sender, RecapitulatifCommandeEventArgs commande)
        {
            if (commande.UneAction == UserControls.ActionCommande.Annuler && commande.UneCommande == null)
                conteneur_principal.Content = this.UcMesCommandes;
            else if (commande.UneAction == UserControls.ActionCommande.Supprimer && commande.UneCommande != null)
            {
                commande.UneCommande.Delete();
                this.LaGestion.LesCommandes.Remove(commande.UneCommande);
                conteneur_principal.Content = this.UcMesCommandes;
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
                this.UcDetailsProduit.FindProduitByNum(leProduitADetaille.NumProduit, this.LaGestion);
                AfficherImage("ImagesProduits/StyloBleu.jpg");

                conteneur_principal.Content = this.UcDetailsProduit;
            }    
        }

        private void AnnulationActionRevendeur(object sender, bool reponse)
        {
            if(reponse)
                conteneur_principal.Content = this.UcSelectionRevendeur;
        }

        public bool ChargeData()
        {
            try
            {
                LaGestion = new GestionPilot("gestion pilot");
                this.DataContext = LaGestion;
                return true;
            }
            catch (Exception ex)
            {
                LogError.Log(ex, "Erreur SQL");
                MessageBox.Show("Login ou mot de passe invalide", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;

                //Application.Current.Shutdown();
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
                    this.UcCreationCommande.LesProduitsSelectionnes.Clear();
                    break;
                case Navigation.MesCommandes:
                    conteneur_principal.Content = this.UcMesCommandes;
                    break;
                /*case Navigation.Deconnexion:
                    conteneur_haut.Visibility = Visibility.Collapsed;
                    conteneur_principal.Visibility = Visibility.Collapsed;
                    conteneur_haut.Children.Clear();
                    conteneur_principal.Content = null;

                    // S'assurer que les champs sont bien réinitialisés
                    this.UcAuthentification.tb_login.Text = "";
                    this.UcAuthentification.tb_mdp.Password = "";

                    conteneur_authentification.Content = this.UcAuthentification;
                    conteneur_authentification.Visibility = Visibility.Visible;
                    conteneur_authentification.IsEnabled = true;
                    conteneur_authentification.Focusable = true;
                    conteneur_authentification.Focus();
                    break;*/

            }
        }

        private void SeConnecter_Reussi (object sender, InformationConnexion informationConnexion)
        {
            this.ConnectionString = $"Host=srv-peda-new;Port=5433;Username={informationConnexion.Login};Password={informationConnexion.MotDePasse};Database=andriane_pilot;Options='-c search_path=andriane'";
            bool chargeDataOk = ChargeData();
            if (chargeDataOk)
            {
                this.UtilisateurConnecte = this.LaGestion.LesEmploye.FirstOrDefault(e => e.Login == informationConnexion.Login);

                if (informationConnexion.ConnexionReussi)
                {
                    conteneur_authentification.Visibility = Visibility.Collapsed;
                    conteneur_authentification.Content = null;

                    conteneur_haut.Visibility = Visibility.Visible;
                    scrollViewer_conteneur_principal.Visibility = Visibility.Visible;

                    this.UcBarDeNavigation.lb_loginUser.Content = this.UtilisateurConnecte.Login;
                    this.UcBarDeNavigation.lb_roleUser.Content = this.UtilisateurConnecte.UnRole.NomRole;
                    conteneur_haut.Children.Add(this.UcBarDeNavigation);
                    conteneur_principal.Content = this.UcAccueilCommercial;



                }
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