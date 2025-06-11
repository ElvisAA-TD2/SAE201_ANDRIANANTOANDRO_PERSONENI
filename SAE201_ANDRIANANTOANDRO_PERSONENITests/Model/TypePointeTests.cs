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
    public class TypePointeTests
    {
        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TypePointes_Nom_Test()
        {
            TypePointe tp1 = new TypePointe(2,"");
        }
        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void TypePointe_FindAll_Test()
        {
            TypePointe tp = new TypePointe(-1,"roller");
            tp.FindAll();
        }
    }
}