using SAE201_ANDRIANANTOANDRO_PERSONENI.Windows;
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

namespace SAE201_ANDRIANANTOANDRO_PERSONENI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Authentification ucAuthentification = new Authentification();
            BarDeNavigation ucBarDeNavigation = new BarDeNavigation();

            conteneur_principal.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            conteneur_principal.RowDefinitions.Add(new RowDefinition { Height = new GridLength(2, GridUnitType.Star) });

            Grid.SetRow(ucBarDeNavigation, 0);
            Grid.SetRow(ucAuthentification, 1);
            conteneur_principal.Children.Add(ucBarDeNavigation);
            conteneur_principal.Children.Add(ucAuthentification);
        }
    }
}