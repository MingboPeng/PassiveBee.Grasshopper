using System;
using System.Xml;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;
using Grasshopper.Kernel.Types;
using System.Dynamic;
using Newtonsoft.Json;

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
            pManager.AddBrepParameter("Surfaces", "Srfs","Envelope surfaces",GH_ParamAccess.item);
            pManager[0].DataMapping = GH_DataMapping.Flatten;
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddTextParameter("out", "out", "out", GH_ParamAccess.item);
            pManager.AddTextParameter("json", "json", "json", GH_ParamAccess.item);

        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object can be used to retrieve data from input parameters and 
        /// to store data in output parameters.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            var inBrep = new GH_Brep();
            DA.GetData(0, ref inBrep);

            
            var HBID = (inBrep.Value.UserDictionary["HBID"] as string).Split('#');
            string baseKey = HBID[0];
            string key = HBID[1];


            var pyRun = Rhino.Runtime.PythonScript.Create();
            string pyScript = @"import scriptcontext as sc;";
            pyScript += @"HBHive = sc.sticky['HBHive']";
            pyScript += "['" + baseKey + "']['" + key + "']";

            dynamic HBObjects = pyRun.EvaluateExpression(pyScript,"HBHive");


            if (HBObjects !=null)
            {
                var PassiveBee = new PassiveBeeObject();
                PassiveBee.WriteWufiXml("tet1");
            }
            
            //string jsonStrings = JsonConvert.SerializeObject(HBObjects);

            DA.SetDataList(0, HBID);
            //DA.SetData(1, HBObjects);

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
    }
}
