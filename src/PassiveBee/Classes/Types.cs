using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PassiveBee
{
    public enum HBType
    {
        HBZone,  //HB_EPZone
        HBSurface, //HB_EPZoneSurface
        nonHBObject
    }

    public enum EPBoundaryCondition
    {
        Surface,  //interior
        Outdoors,
        Ground,
        Adiabatic
    }

    public enum ExtractPtsMethod
    {

    }
}
