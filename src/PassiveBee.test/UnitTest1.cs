using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using IronPython;
using PassiveBee;
using System.IO;
using IronPython.Hosting;
using Microsoft.Scripting;
using Microsoft.Scripting.Hosting;
using System.Dynamic;



namespace PassiveBee.test
{
    [TestClass]
    public class UnitTest1
    {

        public dynamic setPyObject(string name, string type)
        {
            var engine = Python.CreateEngine();
            var scope = engine.CreateScope();
            string variableName = "myObject";
            dynamic myObject = new ExpandoObject();
            myObject.name = name;
            myObject.objectType = type;

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


            var HBObject = new HBObject(PyHBObject);
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
            var HBObject = new HBObject(PyHBObject);
            var expectedResult = HBType.HBZone;
            var acturalResult = HBObject.ObjectType;

            //Assert
            Assert.AreEqual(expectedResult, acturalResult);
        }

        [TestMethod]
        public void TestHBObjectType_nonHBType()
        {
            //Assemble
            string objName = "zone_1";
            string objType = "somethingelse";
            var PyHBObject = setPyObject(objName, objType);


            //Act
            var HBObject = new HBObject(PyHBObject);
            var expectedResult = HBType.nonHBObject;
            var acturalResult = HBObject.ObjectType;

            //Assert
            Assert.AreEqual(expectedResult, acturalResult);
        }
    }
}
