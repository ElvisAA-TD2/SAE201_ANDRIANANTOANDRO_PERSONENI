using SAE201_ANDRIANANTOANDRO_PERSONENI.Model;
using System;
using System.Collections.Generic;
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
    /// Logique d'interaction pour Authentification.xaml
    /// </summary>
    public partial class Authentification : UserControl
    {
        public event EventHandler<InformationConnexion> AuthentificationReussiAvecInformationConnexion;
        public Authentification()
        {
            InitializeComponent();
        }

        private void SeConnecter_Click(object sender, RoutedEventArgs e)
        {
            AuthentificationReussiAvecInformationConnexion?.Invoke(this, new InformationConnexion(tb_login.Text, tb_mdp.Password, true));
        }

        //classe pour transférer deux informations à la mainWindow (l'action et la commande associé)
        public class InformationConnexion : EventArgs
        {
            public string Login { get; }
            public string MotDePasse { get; }
            public bool ConnexionReussi { get; }

            public InformationConnexion(string login, string motDePasse, bool connexionReussi)
            {
                this.Login = login;
                this.MotDePasse = motDePasse;
                this.ConnexionReussi = connexionReussi;
            }
        }
    }
}
