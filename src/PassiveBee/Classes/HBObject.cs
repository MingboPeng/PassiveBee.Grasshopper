using Grasshopper.Kernel.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PassiveBee
{
    //HBObject in C#
    class HBObject
    {
        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public HBObject(GH_Brep inBrep)
        {

        }

        private dynamic mapPyObject(GH_Brep inBrep)
        {
            var HBID = (inBrep.Value.UserDictionary["HBID"] as string).Split('#');
            string formatedHBID = string.Format("['{0}']['{1}']", HBID[0], HBID[1]);

            var pyRun = Rhino.Runtime.PythonScript.Create();
            string pyScript = "";
            pyScript += "import scriptcontext as sc;";
            pyScript += "PyHBObjects=[];";
            //pyScript += "for HBS in HBO.surfaces:";
            pyScript += string.Format(" PyHBObjects.append(sc.sticky['HBHive']{0});", formatedHBID);
            pyRun.ExecuteScript(pyScript);
            var HBObjects = pyRun.GetVariable("PyHBObjects") as IList<dynamic>;

            return HBObjects;
        }

    }
}
