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
        private Guid ID;

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


        //Geometry from Rhino
        private Brep _Geometry;

        public Brep Geometry
        {
            get { return _Geometry; }
            set { _Geometry = value; }
        }

        //Face normal
        private Vector3d _NormalVector;

        public Vector3d NormalVector
        {
            get
            {
                _NormalVector = this.Geometry.GetSrfCenPtandNormal().Normal;
                return _NormalVector;
            }
            set { _NormalVector = value; }
        }


        //Constructor
        public HBSurface(dynamic inObject)
        {
            this.ID = Guid.Parse(inObject.ID);
            this.Name = inObject.name;
            this.ObjectType = HBType.HBSurface;
            this.BC = GetBC(inObject);
            this.HasChild = inObject.hasChild;
            this.Geometry = inObject.geometry;
        }

        private EPBoundaryCondition GetBC(dynamic inObject)
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
