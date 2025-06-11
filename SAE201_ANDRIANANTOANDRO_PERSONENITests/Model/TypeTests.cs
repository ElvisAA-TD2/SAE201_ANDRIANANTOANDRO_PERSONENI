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
    public class TypeTests
    {
        public GestionPilot LaGestion { get; set; }
        private Categorie c1;
        [TestInitialize]
        public void init()
        {
            c1 = new Categorie(1, "test");
        }
        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Employe_Nom_Test()
        {
            Type t1 = new Type(1,"",c1);
        }
        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void TypePointe_FindAll_Test()
        {
            Type t = new Type(-1,"stylo",c1);
            t.FindAll(LaGestion);
        }
    }
}