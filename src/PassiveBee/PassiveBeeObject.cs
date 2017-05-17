using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace PassiveBee
{

    class PassiveBeeObject
    {


        public void WriteWufiXml(string FileName)
        {
            
            using (XmlWriter writer = XmlWriter.Create(@"C:\WUFI\test.xml"))
            {
                //writer.WriteStartDocument();
                //writer.WriteStartElement("WUFIplusProject");


                
                //writer.WriteElementString("DataVersion", this.DataVersion.ToString());
                //writer.WriteElementString("UnitSystem", this.UnitSystem.ToString());
                //writer.WriteElementString("Scope", this.Scope.ToString());
                //writer.WriteElementString("DimensionsVisualizedGeometry", this.DimensionsVisualizedGeometry.ToString());

                ////ProjectData
                //writer.WriteStartElement("ProjectData");
                //writer.WriteElementString("Customer_Name", "this is Customer_Name");
                //writer.WriteEndElement();


                ////Variants
                //writer.WriteStartElement("Variants");
                //writer.WriteElementString("Customer_Name", "this is Customer_Name");
                //writer.WriteEndElement();
                

                ////end of WUFIplusProject
                //writer.WriteEndElement();
                //writer.WriteEndDocument();
            }
        }


        public void Save()
        {

        }
    }
}
