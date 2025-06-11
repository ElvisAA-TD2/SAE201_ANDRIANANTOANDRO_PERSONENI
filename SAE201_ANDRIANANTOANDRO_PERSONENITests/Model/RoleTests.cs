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
    public class RoleTests
    {
        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Role_Nom_Test()
        {
            Role r1 = new Role(1, "");
        }
        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void Role_FindAll_Test()
        {
            Role r = new Role(-2, "test");
            r.FindAll();
        }
    }
}