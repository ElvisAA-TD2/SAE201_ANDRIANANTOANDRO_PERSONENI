
﻿using System;
﻿using SAE201_ANDRIANANTOANDRO_PERSONENI.Model;
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




namespace SAE201_ANDRIANANTOANDRO_PERSONENI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public GestionPilot LaGestion { get; set; }
        public MainWindow()
        {
            //ChargeData();
            InitializeComponent();
            Authentification ucAuthentification = new Authentification();
            BarDeNavigation ucBarDeNavigation = new BarDeNavigation();

            ucBarDeNavigation.NavigationDemandee += BarDeNavigation_NavigationDemandee;

            /*conteneur_principal.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            conteneur_principal.RowDefinitions.Add(new RowDefinition { Height = new GridLength(2, GridUnitType.Star) });

            Grid.SetRow(ucBarDeNavigation, 0);
            Grid.SetRow(ucAuthentification, 1);
            conteneur_principal.Children.Add(ucBarDeNavigation);*/
            conteneur_haut.Children.Add(ucBarDeNavigation);
            conteneur_principal.Content = new AccueilCommercial();
        }

        private void BarDeNavigation_NavigationDemandee(object sender, Enum page)
        {
            switch (page)
            {
                case Navigation.ListeProduits:
                    conteneur_principal.Content = new AccueilCommercial();
                    break;

                case Navigation.CréationCommande:
                    conteneur_principal.Content = new CreationCommande();
                    break;
                case Navigation.MesCommandes:
                    conteneur_principal.Content = new Authentification();
                    break;
            }
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