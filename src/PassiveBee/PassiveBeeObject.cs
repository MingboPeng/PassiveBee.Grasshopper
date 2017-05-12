using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace PassiveBee
{

    class PassiveBeeObject
    {
        int DataVersion = 0;
        int UnitSystem = 0;
        int Scope = 0;
        int DimensionsVisualizedGeometry = 0;
        object[] ProjectData;

        public void WriteWufiXml(string FileName)
        {
            
            using (XmlWriter writer = XmlWriter.Create(@"C:\WUFI\test.xml"))
            {
                writer.WriteStartDocument();
                writer.WriteStartElement("WUFIplusProject");


                
                writer.WriteElementString("DataVersion", this.DataVersion.ToString());
                writer.WriteElementString("UnitSystem", this.UnitSystem.ToString());
                writer.WriteElementString("Scope", this.Scope.ToString());
                writer.WriteElementString("DimensionsVisualizedGeometry", this.DimensionsVisualizedGeometry.ToString());

                //ProjectData
                writer.WriteStartElement("ProjectData");
                writer.WriteElementString("Customer_Name", "this is Customer_Name");
                writer.WriteEndElement();


                //ProjectData
                writer.WriteStartElement("Assemblies");
                writer.WriteElementString("Customer_Name", "this is Customer_Name");
                writer.WriteEndElement();
                

                //end of WUFIplusProject
                writer.WriteEndElement();
                writer.WriteEndDocument();
            }
        }

    }
}
