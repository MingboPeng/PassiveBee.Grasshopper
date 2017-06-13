using Rhino.Geometry;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace PassiveBee
{
    public class WufiModel
    {
        public int DataVersion = 0;
        public int UnitSystem = 0;
        public int Scope = 0;
        public int DimensionsVisualizedGeometry = 0;
        //public object[] ProjectData;
        //public object[] Variants;

        public List<Point3d> Vertices;

        private List<HBSurface> _HBSurfaces;


        public WufiModel()
        {
            WriteXmlFile();
        }

        public WufiModel(HBZone inZone)
        {
            var zone = inZone;
            this._HBSurfaces = zone.Surfaces;

            var vertices = new List<Point3d>();

            foreach (var item in _HBSurfaces)
            {
                vertices.AddRange(item.ExtractPoints(true));
            }
            this.Vertices = vertices;
            WriteXmlFile();
        }




        public void WriteXmlFile()
        {
            var pts = _HBSurfaces.First().ExtractPoints(true);
            dynamic xmlObj = new ExpandoObject();
            xmlObj.DataVersion = DataVersion;
            xmlObj.UnitSystem = UnitSystem;
            xmlObj.Scope = Scope;
            xmlObj.DimensionsVisualizedGeometry = DimensionsVisualizedGeometry;
            xmlObj.pts =  "ss";

            //var obj = ((IDictionary<string, object>)xmlObj ) as object;

            XmlSerializer mySerializer = new XmlSerializer(typeof(DynamicObject));
            // To write to a file, create a StreamWriter object.  
            StreamWriter myWriter = new StreamWriter(@"C:\WUFI\test.xml");
            mySerializer.Serialize(myWriter, xmlObj);
            myWriter.Close();






            //using (XmlWriter writer = XmlWriter.Create(@"C:\WUFI\test.xml"))
            //{
            //    //writer.WriteStartDocument();
            //    //writer.WriteStartElement("WUFIplusProject");



            //    //writer.WriteElementString("DataVersion", this.DataVersion.ToString());
            //    //writer.WriteElementString("UnitSystem", this.UnitSystem.ToString());
            //    //writer.WriteElementString("Scope", this.Scope.ToString());
            //    //writer.WriteElementString("DimensionsVisualizedGeometry", this.DimensionsVisualizedGeometry.ToString());

            //    ////ProjectData
            //    //writer.WriteStartElement("ProjectData");
            //    //writer.WriteElementString("Customer_Name", "this is Customer_Name");
            //    //writer.WriteEndElement();


            //    ////Variants
            //    //writer.WriteStartElement("Variants");
            //    //writer.WriteElementString("Customer_Name", "this is Customer_Name");
            //    //writer.WriteEndElement();


            //    ////end of WUFIplusProject
            //    //writer.WriteEndElement();
            //    //writer.WriteEndDocument();
            //}
        }



    }
}
