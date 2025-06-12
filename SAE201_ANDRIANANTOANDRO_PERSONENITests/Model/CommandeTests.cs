using Microsoft.VisualStudio.TestTools.UnitTesting;
using SAE201_ANDRIANANTOANDRO_PERSONENI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAE201_ANDRIANANTOANDRO_PERSONENI.Model.Tests
{
    [TestClass()]
    public class CommandeTests
    {
        public GestionPilot LaGestion { get; set; }
        private Role r1;
        private TypePointe tp1;
        private Categorie c1;
        private Type t1;
        private Couleur cou1;
        private List<Couleur> couleurs;
        private Produit p1;
        private ProduitCommande pc;
        private List<ProduitCommande> lesProduits;
        private ModeTransport m1;
        private Employe e1;
        private Revendeur re1;
        [TestInitialize()]
        public void init()
        {
            r1 = new Role(1, "test");
            tp1 = new TypePointe(1, "TypePointeTest");
            c1 = new Categorie(1, "CategorieTest");
            t1 = new Type(1, "Typetest", c1);
            cou1 = new Couleur(1, "noir");
            couleurs = new List<Couleur>();
            couleurs.Add(cou1);
            p1 = new Produit(1, "C101", "stylo", 12, 50, true, tp1, t1, couleurs, "../img.png");
            pc = new ProduitCommande(1, p1, 20);
            lesProduits = new List<ProduitCommande>();
            lesProduits.Add(pc);
            m1 = new ModeTransport(1, "Chronopost");
            e1 = new Employe(1, "Personeni", "Nathan", "password", "login", r1);
            re1 = new Revendeur(1, "Papeterie test", "rue de l'arc en ciel", "74000", "Annecy");
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void PrixTotalNegatif_Test()
        {

            Commande commande1 = new Commande(1,e1,re1,m1,DateTime.Today,new DateTime(2025,07,01),lesProduits,-10);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void DateLivraison_Test()
        {
            Commande commande1 = new Commande(1, e1, re1, m1, DateTime.Today, new DateTime(2025,05,02), lesProduits, 10);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Create_Test()
        {
            Commande commande1 = new Commande(-1, e1, re1, m1, DateTime.Today, new DateTime(2025, 07, 01), lesProduits, 100);
            commande1.Create();         
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Delete_Test()
        {
            Commande commande1 = new Commande(-1, e1, re1, m1, DateTime.Today, new DateTime(2025, 07, 01), lesProduits, 100);
            commande1.Delete();
        }
        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void Commannde_FindAll_Test()
        {
            Commande c1 = new Commande(-2, e1,re1,m1, DateTime.Today, new DateTime(2025, 07, 01), lesProduits,100);
            c1.FindAll(LaGestion);
        }
        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void Commannde_Update_Test()
        {
            Commande c1 = new Commande(-2, e1, re1, m1, DateTime.Today, new DateTime(2025, 07, 01), lesProduits, 100);
            c1.Update();
        }
    }
}