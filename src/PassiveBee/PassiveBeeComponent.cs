using System;
using System.Xml;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;
using Grasshopper.Kernel.Types;
using System.Dynamic;
using Newtonsoft.Json;
using System.Linq;

namespace PassiveBee
{
    public class PassiveBeeComponent : GH_Component
    {
        /// <summary>
        /// Each implementation of GH_Component must provide a public 
        /// constructor without any arguments.
        /// Category represents the Tab in which the component will appear, 
        /// Subcategory the panel. If you use non-existing tab or panel names, 
        /// new tabs/panels will automatically be created.
        /// </summary>
        public PassiveBeeComponent()
          : base("PassiveBee", "PassiveBeeNickname",
              "Description????????",
              "PassiveBee", "PassiveBee")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddBrepParameter("Surfaces", "Srfs","Envelope surfaces",GH_ParamAccess.list);
            pManager[0].DataMapping = GH_DataMapping.Flatten;
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddTextParameter("out", "out", "out", GH_ParamAccess.item);
            pManager.AddTextParameter("json", "json", "json", GH_ParamAccess.item);
            pManager.AddGenericParameter("srfs", "srfs", "srfs", GH_ParamAccess.list);

        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object can be used to retrieve data from input parameters and 
        /// to store data in output parameters.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            var inBreps = new List <GH_Brep>();

            if(!DA.GetDataList(0, inBreps))return;
            var HBObjects = callFromHBHive(inBreps);

            //GH_Brep inBrep = inBreps[0];

            var HBNames = new List<string>();
            var HBTypes = new List<HBType>();

            foreach (var item in HBObjects)
            {
                HBNames.Add(item.Name);
                HBTypes.Add(item.ObjectType);
            }
            
            


            //var pyRun = Rhino.Runtime.PythonScript.Create();
            //string pyScript = "";
            //pyScript += "import scriptcontext as sc;";
            //pyScript += "PyHBObjects=[];";
            ////pyScript += "for HBS in HBO.surfaces:";
            //pyScript += string.Format(" PyHBObjects.append(sc.sticky['HBHive']{0});", formatedHBID);
            ////pyScript += "points=[];";


            ////pyScript += "\nif HBO.objectType == 'HBZone':";
            ////pyScript += "\n for HBS in HBO.surfaces:";
            ////pyScript += "   points.append(HBS.extractPoints(1, True))";

            

            //pyRun.ExecuteScript(pyScript);
            //var HBObjects = pyRun.GetVariable("PyHBObjects") as IList<dynamic>;

            //var backFromPython = pyRun.GetVariable("points") as IList<object>;
            //var backFromPython = HBObjects.GetType().GetProperties();
            //var firstItem = backFromPython[0] as IList<object>;
            //var firstPt = firstItem[0];

            //var HBObjects = new List<object>(PyHBObjects);
            

            //dynamic HBObject = HBObjects.First().name;



            //if (backFromPython !=null)
            //{
            //    //var PassiveBee = new PassiveBeeObject();
            //    //PassiveBee.WriteWufiXml("tet1");
            //}
            
            //string jsonStrings = JsonConvert.SerializeObject(HBObjects);

            DA.SetDataList(0, HBTypes);
            //DA.SetData(1, backFromPython);
            DA.SetDataList(2, HBNames);

        }

        /// <summary>
        /// Provides an Icon for every component that will be visible in the User Interface.
        /// Icons need to be 24x24 pixels.
        /// </summary>
        protected override System.Drawing.Bitmap Icon
        {
            get
            {
                // You can add image files to your project resources and access them like this:
                //return Resources.IconForThisComponent;
                return null;
            }
        }

        /// <summary>
        /// Each component must have a unique Guid to identify it. 
        /// It is vital this Guid doesn't change otherwise old ghx files 
        /// that use the old ID will partially fail during loading.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("{92bd1f3e-02e8-4f51-9a5f-8d9753dfde00}"); }
        }

        private List<HBObject> callFromHBHive(List<GH_Brep> inBreps)
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

            var HBObjects = getHBObjects(HBIDs);
            return HBObjects;

        }

        private List<HBObject> getHBObjects(List<string> HBIDs)
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

            var HBObjects = new List<HBObject>();

            foreach (var item in PyHBObjects)
            {
                var HBObject = new HBObject(item);
                HBObjects.Add(HBObject);
            }


            return HBObjects;
        }

    }
}
