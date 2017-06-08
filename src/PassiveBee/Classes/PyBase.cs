using Grasshopper.Kernel.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PassiveBee
{
    static class PyBase
    {


        public static List<HBZone> CallFromHBHive(List<GH_Brep> inBreps)
        {
            var HBIDs = new List<string>();
            foreach (var item in inBreps)
            {
                //todo: if null
                //todo: check if HBID existed
                var HBID = item.Value.UserDictionary["HBID"] as string;
                //string formatedHBID = string.Format("['{0}']['{1}']", HBID[0], HBID[1]);
                HBIDs.Add(HBID);
            }

            var PyHBObjects = PyBase.GetHBObjects(HBIDs);

            var HBZones = new List<HBZone>();
            //todo: for later use
            var HBSurfaces = new List<HBSurface>();

            foreach (var item in PyHBObjects)
            {
                var hbType = PyBase.GetHBType(item);
                if (hbType == HBType.HBZone)
                {
                    HBZones.Add(new HBZone(item));
                }
                else if (hbType == HBType.HBSurface)
                {
                    HBSurfaces.Add(new HBSurface(item));
                }
                else
                {
                    //non hbObject
                }

            }
            return HBZones;

        }

        

        public static IList<dynamic> GetHBObjects(List<string> HBIDs)
        {

            var pyRun = Rhino.Runtime.PythonScript.Create();
            pyRun.SetVariable("HBIDs", HBIDs.ToArray());
            string pyScript = @"
import scriptcontext as sc;
PyHBObjects=[];
for HBID in HBIDs:
    baseKey, key = HBID.split('#')[0], '#'.join(HBID.split('#')[1:])
    PyHBObjects.append(sc.sticky['HBHive'][baseKey][key]);
";

            pyRun.ExecuteScript(pyScript);
            var PyHBObjects = pyRun.GetVariable("PyHBObjects") as IList<dynamic>;
            //todo: convert IList to List
            
            return PyHBObjects;
        }


        public static HBType GetHBType(dynamic inObject)
        {
            string type = ((string)inObject.objectType).ToUpper();
            if (type == "HBZONE")
            {
                return HBType.HBZone;
            }
            else if (type == "HBSURFACE")
            {
                return HBType.HBSurface;
            }
            else
            {
                return HBType.nonHBObject;
            }
            
        }
    }
}
