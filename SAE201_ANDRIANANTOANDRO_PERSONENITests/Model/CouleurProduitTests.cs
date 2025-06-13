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
    public class CouleurProduitTests
    {
        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void Couleur_FindAll_Test()
        {
            CouleurProduit c1 = new CouleurProduit(-1, -1);
            c1.FindAll();
        }
    }
}