using System;
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
using SAE201_ANDRIANANTOANDRO_PERSONENI.Model;
using SAE201_ANDRIANANTOANDRO_PERSONENI.UserControls;

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
            ChargeData();
            InitializeComponent();
            Authentification ucAuthentification = new Authentification();
            BarDeNavigation ucBarDeNavigation = new BarDeNavigation();
            AccueilCommercial ucAcceuilCommercial = new AccueilCommercial();

            /*conteneur_principal.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            conteneur_principal.RowDefinitions.Add(new RowDefinition { Height = new GridLength(2, GridUnitType.Star) });

            Grid.SetRow(ucBarDeNavigation, 0);
            Grid.SetRow(ucAuthentification, 1);
            conteneur_principal.Children.Add(ucBarDeNavigation);*/
            conteneur_principal.Children.Add(ucAcceuilCommercial);
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