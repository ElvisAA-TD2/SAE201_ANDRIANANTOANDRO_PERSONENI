
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


            this.UcAuthentification.AuthentificationReussi += SeConnecter_Reussi;
            this.UcBarDeNavigation.NavigationDemandee += BarDeNavigation_NavigationDemandee;
            this.UcCreationCommande.CreationCommandeValidation += CreationCommande_VersSelectionClient;
            this.UcSelectionRevendeur.RevendeurActionNecessaire += SelectionRevendeur_VersActionRevendeur;
            this.UcFormulaireRevendeur.ActionRevendeurEffectuee += AjoutRevendeur;


            conteneur_authentification.Content = this.UcAuthentification;
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
                case ActionRevendeur.Ajouter:
                    this.UcFormulaireRevendeur.label_titre.Content = "Création revendeur";
                    this.UcFormulaireRevendeur.btn_validation.Content = "Créer";
                    break;

                case ActionRevendeur.Modifier:
                    this.UcFormulaireRevendeur.label_titre.Content = "Modification revendeur";
                    this.UcFormulaireRevendeur.btn_validation.Content = "Modifier";

                    this.UcFormulaireRevendeur.tb_raisonSociale.Text = actionRevendeur.Revendeur.RaisonSociale;
                    this.UcFormulaireRevendeur.tb_adresseRue.Text = actionRevendeur.Revendeur.AdresseRue;
                    this.UcFormulaireRevendeur.tb_adresseCP.Text = actionRevendeur.Revendeur.AdresseCP;
                    this.UcFormulaireRevendeur.tb_adresseVille.Text = actionRevendeur.Revendeur.AdresseVille;
                    break;
                    
            };

            conteneur_principal.Content = this.UcFormulaireRevendeur;
        }

        private void AjoutRevendeur (object sender, Revendeur unRevendeur)
        {
            unRevendeur.NumRevendeur = unRevendeur.Create();

            this.LaGestion.LesRevendeurs.Add(unRevendeur);

            MessageBox.Show($"{unRevendeur.RaisonSociale}, {unRevendeur.AdresseRue}, {unRevendeur.AdresseCP}, {unRevendeur.AdresseVille}");
        }
        
    }
}