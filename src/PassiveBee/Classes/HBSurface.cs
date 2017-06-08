using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Rhino.Geometry;

namespace PassiveBee
{
    public class HBSurface
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

        //ObjectType: HBSurface
        private HBType _ObjectType;

        public HBType ObjectType
        {
            get { return _ObjectType; }
            set { _ObjectType = value; }
        }

        //BC
        private EPBoundaryCondition _BC;

        public EPBoundaryCondition BC
        {
            get { return _BC; }
            set { _BC = value; }
        }

        //HasChild
        private bool _HasChild;

        public bool HasChild
        {
            get { return _HasChild; }
            set { _HasChild = value; }
        }


        private Brep _Geometry;

        public Brep Geometry
        {
            get { return _Geometry; }
            set { _Geometry = value; }
        }


        //Constructor
        public HBSurface(dynamic inObject)
        {
            this._ID = Guid.Parse(inObject.ID);
            this._Name = inObject.name;
            this._ObjectType = HBType.HBSurface;
            this._BC = getBC(inObject);
            this._HasChild = inObject.hasChild;
            this._Geometry = inObject.geometry;
        }

        private EPBoundaryCondition getBC(dynamic inObject)
        {
            string type = ((string) inObject.BC).ToUpper();
            if (type == "SURFACE")
            {
                return EPBoundaryCondition.Surface;
            }
            else if (type == "OUTDOORS")
            {
                return EPBoundaryCondition.Outdoors;
            }
            else if (type == "GROUND")
            {
                return EPBoundaryCondition.Ground;
            }
            else 
            {
                return EPBoundaryCondition.Adiabatic;
            }
        }
    }
}
