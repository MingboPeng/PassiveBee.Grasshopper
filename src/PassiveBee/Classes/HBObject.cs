using Grasshopper.Kernel.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PassiveBee
{

    public enum HBType
    {
        HBZone,
        HBSurface,
        nonHBObject

    }
    //HBObject in C#
    public class HBObject
    {
        //Name
        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        //ObjectType
        private HBType objectType;

        public HBType ObjectType
        {
            get { return objectType; }
            set { objectType = value; }
        }




        public HBObject(dynamic inObject)
        {
            this.name = inObject.name;
            this.objectType = getHBType(inObject);

        }

        private HBType getHBType(dynamic inObject)
        {
            string type = inObject.objectType;
            if (type == "HBZone")
            {
                return HBType.HBZone;
            }
            else if (type == "HBSurface")
            {
                return HBType.HBSurface;
            }
            else
            {
                return HBType.nonHBObject;
            }


        }



        



    }
}
