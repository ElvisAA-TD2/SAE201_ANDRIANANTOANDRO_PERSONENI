
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
using Microsoft.VisualBasic;




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
        private AccueilEmploye UcAccueilEmploye { get; set; }
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

        public Employe UtilisateurConnecte { get; set; }

        
        

        public event PropertyChangedEventHandler? PropertyChanged;


        public MainWindow()
        {
            //ChargeData();
            InitializeComponent();
            this.UtilisateurConnecte = new Employe();
            this.CommandeACree = new Commande();


            this.UcAuthentification = new Authentification();
            this.UcBarDeNavigation = new BarDeNavigation();
            this.UcCreationCommande = new CreationCommande();
            this.UcSelectionRevendeur = new SelectionRevendeur();
            this.UcAccueilEmploye = new AccueilEmploye();
            this.UcFormulaireRevendeur = new FormulaireRevendeur();
            this.UcDetailsProduit = new DetailsProduit();
            this.UcMesCommandes = new MesCommandes();
            this.UcRecapitulatifCommande = new RecapitulatifCommande();
            this.UcRecapitulatifCommande = new RecapitulatifCommande();
            this.UcFormulaireProduit = new FormulaireProduit();


            this.UcAuthentification.AuthentificationReussiAvecInformationConnexion += SeConnecter_Reussi;

            this.UcAccueilEmploye.VoirDetailProduitDemande += AfficherDetailsProduit;
            this.UcAccueilEmploye.AjouterProduitDemande += AfficherAjoutProduit;

            this.UcBarDeNavigation.NavigationDemandee += BarDeNavigation_NavigationDemandee;

            this.UcCreationCommande.CreationCommandeValidation += CreationCommande_VersSelectionClient;

            this.UcSelectionRevendeur.RevendeurActionNecessaire += SelectionRevendeur_VersActionRevendeur;
            this.UcSelectionRevendeur.RevendeurSelectionne += SelectionRevendeurDemandee;

            this.UcFormulaireRevendeur.ActionRevendeurEffectuee += ActionRevendeur;
            this.UcFormulaireRevendeur.AnnulationActionRevendeur += AnnulationActionRevendeur;

            this.UcDetailsProduit.RevenirEnArrièreDemandee += RetourEnArrièreVenantVenantDeDétails;
            this.UcDetailsProduit.ModificationRendreInsponibleDemandee += ActionProduitDemandee;

            this.UcMesCommandes.VoirDetailsCommandes += DetailsCommandeDemandee;

            this.UcRecapitulatifCommande.ActionCommandeDemandee += ActionCommande;


            conteneur_authentification.Content = this.UcAuthentification;
        }

        private void ActionProduitDemandee(object sender, DetailsProduit.InformationProduitEventArgs uneInformationPorduit)
        {
            if (! uneInformationPorduit.RendreIndisponible)
            {
                this.UcFormulaireProduit.label_titre.Content = "Modification de produit";
                this.UcFormulaireProduit.btn_valider.Content = ActionProduitEffectue.Modifier.ToString();
                this.UcFormulaireProduit.btn_modifieImage.Content = "Modifier l'image";

                this.UcFormulaireProduit.ProduitAModifier = uneInformationPorduit.UnProduit;

                /*this.UcFormulaireProduit.tb_nomProduit.Text = uneInformationPorduit.UnProduit.NomProduit;


                
                
                this.UcFormulaireProduit.tb_prix.Text = uneInformationPorduit.UnProduit.PrixVente.ToString();
                this.UcFormulaireProduit.tb_qteStock.Text = uneInformationPorduit.UnProduit.QteStock.ToString();*/


                //this.UcFormulaireProduit.tb_couleur.Text = uneInformationPorduit.UnProduit.NomCouleurConcatene;
                //this.UcFormulaireProduit.tb_categorie.Text = uneInformationPorduit.UnProduit.UnType.UneCategorie.NomCategorie;
                //this.UcFormulaireProduit.tb_type.Text = uneInformationPorduit.UnProduit.UnType.NomType;
                //this.UcFormulaireProduit.tb_typePointe.Text = uneInformationPorduit.UnProduit.UnTypePointe.NomTypePointe;

                conteneur_principal.Content = this.UcFormulaireProduit;
            }
            else
            {
                uneInformationPorduit.UnProduit.RendreIndisponible();
                this.LaGestion.LesProduits.FirstOrDefault(p => p.NumProduit == uneInformationPorduit.UnProduit.NumProduit).Disponible = false;
                conteneur_principal.Content = this.UcAccueilEmploye;
            }

        }

        private void AfficherAjoutProduit(object sender, bool reponse)
        {
            this.UcFormulaireProduit.label_titre.Content = "Ajout de produit";
            this.UcFormulaireProduit.btn_valider.Content = ActionProduitEffectue.Créer.ToString();
            this.UcFormulaireProduit.btn_modifieImage.Content = "Ajouter une image";

            this.UcFormulaireProduit.tb_nomProduit.Text = "";
            
            
            this.UcFormulaireProduit.tb_prix.Text = "";
            this.UcFormulaireProduit.tb_qteStock.Text = "";

            //this.UcFormulaireProduit.tb_couleur.Text = "";
            //this.UcFormulaireProduit.tb_categorie.Text = "";
            //this.UcFormulaireProduit.tb_type.Text = "";
            //this.UcFormulaireProduit.tb_typePointe.Text = "";

            conteneur_principal.Content = this.UcFormulaireProduit;
        }

        private void SelectionRevendeurDemandee(object? sender, Revendeur revendeurSelectionnee)
        {
            this.CommandeACree.UnRevendeur = revendeurSelectionnee;
            this.CommandeACree.NumCommande = this.CommandeACree.Create();

            this.LaGestion.LesCommandes.Add(this.CommandeACree);
            foreach (ProduitCommande unProduitCommande in this.CommandeACree.LesProduitCommande)
            {
                unProduitCommande.Create(this.CommandeACree.NumCommande);
                this.LaGestion.LesProduitCommandes.Add(unProduitCommande);
            }
                

            MessageBox.Show(this.CommandeACree.ToString());
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
                conteneur_principal.Content = this.UcAccueilEmploye;
        }

        private void AfficherDetailsProduit(object sender, Produit leProduitADetaille)
        {
            this.UcDetailsProduit.FindProduitByNum(leProduitADetaille.NumProduit, this.LaGestion);
            AfficherImage("ImagesProduits/StyloBleu.jpg");
            //A décommenté quand on aura fini de faire les privilège
            /*if(this.UtilisateurConnecte.UnRole.NomRole == "Commercial") 
            {
                this.UcDetailsProduit.btn_rendreIndisponible.Visibility = Visibility.Collapsed;
            }
                
            else this.UcDetailsProduit.btn_rendreIndisponible.Visibility = Visibility.Visible;*/

            conteneur_principal.Content = this.UcDetailsProduit;  
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
                    conteneur_principal.Content = this.UcAccueilEmploye;
                    break;

                case Navigation.CréationCommande:
                    conteneur_principal.Content = this.UcCreationCommande;
                    this.UcCreationCommande.LesProduitsSelectionnes.Clear();
                    this.UcCreationCommande.lb_cout_commande.Content = "0,0 €";
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
            //this.ConnectionString = $"Host=localhost;Port=5432;Username=postgres;Password=Anniversaire1906$;Database=andriane_pilot";
            this.ConnectionString = $"Host=srv-peda-new;Port=5433;Username={informationConnexion.Login};Password={informationConnexion.MotDePasse};Database=andriane_pilot;Options='-c search_path=andriane'";
            bool chargeDataOk = ChargeData();
            if (chargeDataOk)
            {
                this.UtilisateurConnecte = this.LaGestion.LesEmploye.FirstOrDefault(e => e.Login == "andriane");

                if (informationConnexion.ConnexionReussi)
                {
                    conteneur_authentification.Visibility = Visibility.Collapsed;
                    conteneur_authentification.Content = null;

                    conteneur_haut.Visibility = Visibility.Visible;
                    scrollViewer_conteneur_principal.Visibility = Visibility.Visible;

                    this.UcBarDeNavigation.lb_loginUser.Content = this.UtilisateurConnecte.Login;
                    this.UcBarDeNavigation.lb_roleUser.Content = this.UtilisateurConnecte.UnRole.NomRole;
                    conteneur_haut.Children.Add(this.UcBarDeNavigation);
                    conteneur_principal.Content = this.UcAccueilEmploye;



                }
            }
        }

        private void CreationCommande_VersSelectionClient(object sender, Commande uneCommande)
        {
            this.CommandeACree.UnEmploye = this.UtilisateurConnecte;
            this.CommandeACree.DateCommande = uneCommande.DateCommande;
            this.CommandeACree.DateLivraison = uneCommande.DateLivraison;
            this.CommandeACree.LesProduitCommande = uneCommande.LesProduitCommande;
            this.CommandeACree.PrixTotal = this.UcCreationCommande.PrixTotal;
            this.CommandeACree.UnModeTransport = uneCommande.UnModeTransport;

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