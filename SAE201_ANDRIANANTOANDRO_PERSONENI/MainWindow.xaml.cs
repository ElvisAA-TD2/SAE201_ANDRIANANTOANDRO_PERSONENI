
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
        private SelectionClient UcSelectionClient { get; set; }
        private Authentification UcAuthentification { get; set; }

        public MainWindow()
        {
            ChargeData();
            InitializeComponent();
            this.UcAuthentification = new Authentification();
            this.UcBarDeNavigation = new BarDeNavigation();
            this.UcCreationCommande = new CreationCommande();
            this.UcSelectionClient = new SelectionClient();
            this.UcAccueilCommercial = new AccueilCommercial();


            this.UcAuthentification.AuthentificationReussi += SeConnecter_Reussi;
            this.UcBarDeNavigation.NavigationDemandee += BarDeNavigation_NavigationDemandee;
            this.UcCreationCommande.CreationCommandeValidation += CreationCommande_VersSelectionClient;


            conteneur_authentification.Content = this.UcAuthentification;
        }

        private void BarDeNavigation_NavigationDemandee(object sender, Enum page)
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
                conteneur_principal.Content = this.UcSelectionClient;
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
    }
}