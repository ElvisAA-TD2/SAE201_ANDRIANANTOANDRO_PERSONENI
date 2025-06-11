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
            Role r1 = new Role(1, "test");
            TypePointe tp1 = new TypePointe(1, "TypePointeTest");
            Categorie c1 = new Categorie(1, "CategorieTest");
            Type t1 = new Type(1, "Typetest", c1);
            Couleur cou1 = new Couleur(1, "noir");
            List<Couleur> couleurs = new List<Couleur>();
            couleurs.Add(cou1);
            Produit p1 = new Produit(1, "c101", "stylo", 12, 50, true, tp1, t1, couleurs);
            ProduitCommande pc = new ProduitCommande(1, p1, 20, 10);
            List<ProduitCommande> lesProduits = new List<ProduitCommande>();
            lesProduits.Add(pc);
            ModeTransport m1 = new ModeTransport(1, "Chronopost");
            Employe e1 = new Employe(1, "Personeni", "Nathan", "password", "login", r1);
            Revendeur re1 = new Revendeur(1, "Papeterie test", "rue de l'arc en ciel", "74000", "Annecy");
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void PrixTotalNegatif_Test()
        {

            Commande commande1 = new Commande(1,e1,re1,m1,DateTime.Today,DateTime.Today,lesProduits,-10);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Create_Test()
        {
            Commande commande1 = new Commande(1, e1, re1, m1, DateTime.Today, DateTime.Today, lesProduits, 100);

        }

    }
}