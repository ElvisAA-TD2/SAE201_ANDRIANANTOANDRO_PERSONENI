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
    public class ProduitCommandeTests
    {
        public GestionPilot LaGestion { get; set; }
        private TypePointe tp1;
        private Categorie c1;
        private Type t1;
        private Couleur cou1;
        private List<Couleur> couleurs;
        private Produit p1;
        private Produit p2;
        private ProduitCommande pc;
        [TestInitialize()]
        public void init()
        {
            tp1 = new TypePointe(1, "TypePointeTest");
            c1 = new Categorie(1, "CategorieTest");
            t1 = new Type(1, "Typetest", c1);
            cou1 = new Couleur(1, "noir");
            couleurs = new List<Couleur>();
            couleurs.Add(cou1);
            p1 = new Produit(1, "C101", "stylo", 12, 500, true, tp1, t1, couleurs);
            p2 = new Produit(1, "C101", "stylo", 5, 500, true, tp1, t1, couleurs);
        }
        [TestMethod()]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ProduitCommande_QteCommandee_Test()
        {
            pc = new ProduitCommande(1, p1, -20);
        }
        [TestMethod()]
        public void ProduitCommande_Prix_Test()
        {
            pc = new ProduitCommande(1, p1, 20);
            decimal prix = pc.Prix;
            Assert.AreEqual(240, prix, "Une commande de 20 produit à 12€ l'unité");
            pc = new ProduitCommande(1, p2,35);
            decimal prix2 = pc.Prix;
            Assert.AreEqual(175, prix2, "Une commande de 35 produit à 5€ l'unité");
        }
        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void ProduitCommande_Create_Test()
        {
            pc = new ProduitCommande(-1, p1, 20);
            pc.Create(1);
        }
        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void ProduitCommande_FindAll_Test()
        {
            ProduitCommande pc = new ProduitCommande(-1, p1,10);
            pc.FindAll(LaGestion);
        }
    }
}