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
        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Employe_Nom_Test()
        {
            Role r1 = new Role(1, "test");
            Employe e1 = new Employe(1, "", "Nathan", "password", "login", r1);
        }
        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Employe_Prenom_Test()
        {
            Role r1 = new Role(1, "test");
            Employe e1 = new Employe(1, "Personeni", "", "password", "login", r1);
        }
        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Employe_Password_Test()
        {
            Role r1 = new Role(1, "test");
            Employe e1 = new Employe(1, "Personeni", "Nathan", "", "login", r1);
        }
        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Employe_Login_Test()
        {
            Role r1 = new Role(1, "test");
            Employe e1 = new Employe(1, "Personeni", "Nathan", "password", "", r1);
        }
    }
}