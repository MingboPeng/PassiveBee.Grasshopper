using Rhino.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

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

        private List<HBSurface> _HBSurfaces;



        public WufiModel(HBZone inZone)
        {
            var zone = inZone;
            this._HBSurfaces = zone.Surfaces;

        }



        public void WriteXmlFile()
        {
            var pts = _HBSurfaces.First().ExtractPoints(true);
        }



    }
}
