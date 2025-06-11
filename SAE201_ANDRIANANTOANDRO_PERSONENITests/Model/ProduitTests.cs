using Microsoft.VisualStudio.TestTools.UnitTesting;
using SAE201_ANDRIANANTOANDRO_PERSONENI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;

namespace SAE201_ANDRIANANTOANDRO_PERSONENI.Model.Tests
{
    [TestClass()]
    public class ProduitTests
    {
        private TypePointe tp1;
        private Categorie c1;
        private Type t1;
        private Couleur cou1;
        private List<Couleur> couleurs;
        [TestInitialize]
        public void init()
        {
            tp1 = new TypePointe(1, "TypePointeTest");
            c1 = new Categorie(1, "CategorieTest");
            t1 = new Type(1, "Typetest", c1);
            cou1 = new Couleur(1, "noir");
            couleurs = new List<Couleur>();
            couleurs.Add(cou1);
        }
        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Produit_Code_Null_Test()
        {
            Produit p1 = new Produit(1, "", "stylo", 12, 50, true, tp1, t1, couleurs);
        }
        [TestMethod()]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Produit_Code_MauvaisFormat_Test()
        {
            Produit p1 = new Produit(1, "A101", "stylo", 12, 50, true, tp1, t1, couleurs);
        }
        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Produit_Nom_produit()
        {
            Produit p1 = new Produit(1, "C101", "", 12, 50, true, tp1, t1, couleurs);
        }
        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Produit_Nom_produi()
        {
            Produit p1 = new Produit(1, "C101", "", 12, 50, true, tp1, t1, couleurs);
        }
    }
}