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
    public class EmployeTests
    {
        private Role r1;
        [TestInitialize()]
        public void init()
        {
            r1 = new Role(1, "test");
        }
            [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Employe_Nom_Test()
        {
            Employe e1 = new Employe(1, "", "Nathan", "password", "login", r1);
        }
        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Employe_Prenom_Test()
        {
            Employe e1 = new Employe(1, "Personeni", "", "password", "login", r1);
        }
        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Employe_Password_Test()
        {
            Employe e1 = new Employe(1, "Personeni", "Nathan", "", "login", r1);
        }
        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Employe_Login_Test()
        {
            Employe e1 = new Employe(1, "Personeni", "Nathan", "password", "", r1);
        }
    }
}