using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PassiveBee.test
{
    [TestClass]
    class WufiModelTest
    {
        [TestMethod]
        public void TestWriteXml()
        {
            //Assemble
            var wufiModel = new WufiModel();
            //var old = new List<int>() { 1, 2, 3 };

            //Act
            var ifXmlExist = File.Exists(@"C:\WUFI\test.xml");
            //old.Reverse();

            //Assert
            var acturalResult = true;
            var expectedResult = ifXmlExist;
            Assert.AreEqual(expectedResult, acturalResult);
        }
    }
}
