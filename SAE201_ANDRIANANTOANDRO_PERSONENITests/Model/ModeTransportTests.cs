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
    public class ModeTransportTests
    {
        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ModeTransport_Nom_Test()
        {
            ModeTransport mt1 = new ModeTransport(1,"");
        }
    }
}