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
        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Employe_Nom_Test()
        {
            Categorie  c1 = new Categorie(1,"test");
            Type t1 = new Type(1,"",c1);
        }
    }
}