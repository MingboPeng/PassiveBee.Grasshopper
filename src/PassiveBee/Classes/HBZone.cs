using Grasshopper.Kernel.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PassiveBee
{
    
    //HBZone in C#
    public class HBZone
    {
        //ID
        private Guid _ID;

        public Guid ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        //Name
        private string _Name;

        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }

        //ObjectType
        private HBType _ObjectType;

        public HBType ObjectType
        {
            get { return _ObjectType; }
            set { _ObjectType = value; }
        }

        //Surfaces
        private List<HBSurface> _Surfaces;

        public List<HBSurface> Surfaces
        {
            get { return _Surfaces; }
            set { _Surfaces = value; }
        }



        public HBZone(dynamic inObject)
        {
            this._ID = Guid.Parse(inObject.ID);
            this._Name = inObject.name;
            //convert HBSurfaces
            
            var surfaces = inObject.surfaces as IList<dynamic>;
            var HBSurfaces = new List<HBSurface>();
            foreach (var item in surfaces)
            {
                HBSurfaces.Add(new HBSurface(item));
            }

            this._Surfaces = HBSurfaces;
            

        }

        



        



    }
}
