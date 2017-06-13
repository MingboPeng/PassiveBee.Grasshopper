using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using IronPython;
using PassiveBee;
using System.IO;
using IronPython.Hosting;
using Microsoft.Scripting;
using Microsoft.Scripting.Hosting;
using System.Dynamic;
using System.Collections.Generic;

namespace PassiveBee.test
{
    [TestClass]
    public class HB2PB
    {

        public dynamic setPyObject(string name, string type)
        {
            var engine = Python.CreateEngine();
            var scope = engine.CreateScope();
            string variableName = "myObject";
            dynamic myObject = new ExpandoObject();
            myObject.name = name;
            myObject.objectType = type;
            myObject.ID = (new Guid()).ToString();

            scope.SetVariable(variableName, myObject);
            dynamic PyHBObject = scope.GetVariable(variableName);

            return PyHBObject;
        }

        [TestMethod]
        public void TestHBObjectName()
        {
            string objName = "zone_1";
            string objType = "HBZone";
            var PyHBObject = setPyObject(objName, objType);


            var HBObject = new HBZone(PyHBObject);
            Assert.AreEqual("zone_1", HBObject.Name);
        }

        [TestMethod]
        public void TestHBObjectType_HBZone()
        {
            //Assemble
            string objName = "zone_1";
            string objType = "HBZone";
            var PyHBObject = setPyObject(objName, objType);


            //Act
            var HBObject = new HBZone(PyHBObject);
            var expectedResult = HBType.HBZone;
            var acturalResult = HBObject.ObjectType;

            //Assert
            Assert.AreEqual(expectedResult, acturalResult);
        }


        [TestMethod]
        public void TestListReverse()
        {
            //Assemble
            var old = new List<int>() { 1, 2, 3 };
            
            //Act
            old.Reverse();

            //Assert
            var expectedResult = 3;
            Assert.AreEqual(expectedResult, old[0]);
        }

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
