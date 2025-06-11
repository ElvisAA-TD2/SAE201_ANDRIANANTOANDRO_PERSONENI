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
    public class ProduitTests
    {
        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Produit_Nom_Test()
        {
            Categorie c1 = new Categorie(2, "");
        }
    }
}