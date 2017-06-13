using Grasshopper.Kernel.Types;
using Microsoft.CSharp.RuntimeBinder;
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



        public HBZone(object inObject)
        {
            var dyObj = inObject as dynamic;

            this.ID = Guid.Parse(dyObj.ID);
            this.Name = dyObj.name;
            this.ObjectType = HBType.HBZone;
            //convert HBSurfaces

            var HBSurfaces = new List<HBSurface>();
            try
            {
                var surfaces = dyObj.surfaces as IList<dynamic>;
                
                foreach (var item in surfaces)
                {
                    HBSurfaces.Add(new HBSurface(item));
                }
            }
            catch (RuntimeBinderException)
            {

                //throw;
            }
            

            this.Surfaces = HBSurfaces;
            

        }

        



        



    }
}
