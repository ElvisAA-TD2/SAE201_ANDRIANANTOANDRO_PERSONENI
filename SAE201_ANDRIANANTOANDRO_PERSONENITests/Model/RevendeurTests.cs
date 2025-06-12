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
    public class RevendeurTests
    {
        public GestionPilot LaGestion {  get; set; }
        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Revendeur_RaisonSociale_test()
        {
            Revendeur r1 = new Revendeur(1, "", "rue de l'arc en ciel", "74000", "Annecy");
        }
        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Revendeur_AdresseRue_test()
        {
            Revendeur r1 = new Revendeur(1, "Papeterie test", "", "74000", "Annecy");
        }
        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Revendeur_AdresseCP_test()
        {
            Revendeur r1 = new Revendeur(1, "Papeterie test", "rue de l'arc en ciel", "", "Annecy");
        }
        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Revendeur_AdresseVille_test()
        {
            Revendeur r1 = new Revendeur(1, "Papeterie test", "rue de l'arc en ciel", "74000", "");

        }
        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void Revendeur_Create_test()
        {
            Revendeur r1 = new Revendeur(-1, "Papeterie test", "rue de l'arc en ciel", "74000", "Annecy");
            r1.Create();
        }
        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void Revendeur_Read_test()
        {
            Revendeur r1 = new Revendeur(-1, "Papeterie test", "rue de l'arc en ciel", "74000", "Annecy");
            r1.Read();
        }
        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void Revendeur_Update_test()
        {
            Revendeur r1 = new Revendeur(-1, "Papeterie test", "rue de l'arc en ciel", "74000", "Annecy");
            r1.Update();
        }
    }
}