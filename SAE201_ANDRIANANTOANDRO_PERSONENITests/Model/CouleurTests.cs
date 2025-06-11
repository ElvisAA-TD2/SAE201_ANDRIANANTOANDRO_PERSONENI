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
    public class CouleurTests
    {
        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Couleur_Nom_Test()
        {
            Couleur c1 = new Couleur(2, "");
        }
    }
}