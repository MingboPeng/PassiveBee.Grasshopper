using Rhino.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PassiveBee
{
    static class Extension
    {
        public static List<Point3d> Discontinuities(this Curve inCurve)
        {
            //Honeybee https://github.com/mostaphaRoudsari/honeybee/blob/master/src/Honeybee_Honeybee.py#L6084
            //# copied and modified from rhinoScript (@Steve Baer @GitHub)
            //            """Search for a derivatitive, tangent, or curvature discontinuity in
            //        a curve object.
            //        Parameters:
            //          curve_id = identifier of curve object
            //          style = The type of continuity to test for. The types of
            //              continuity are as follows:
            //              Value    Description
            //              1        C0 - Continuous function
            //              2        C1 - Continuous first derivative
            //              3        C2 - Continuous first and second derivative
            //              4        G1 - Continuous unit tangent
            //              5        G2 - Continuous unit tangent and curvature
            //        Returns:
            //          List 3D points where the curve is discontinuous
            //        """

            var dom = inCurve.Domain;
            double t0 = dom.Min;
            double t1 = dom.Max;
            var points = new List<Point3d>();
            bool get_next = true;
            while (get_next)
            {
                get_next = inCurve.GetNextDiscontinuity(Continuity.G1_continuous, t0, t1, out double t);

                if (get_next)
                {
                    points.Add(inCurve.PointAt(t));
                    t0 = t;
                }
            }

            return points;
        }

        public static (Point3d Center, Vector3d Normal) GetSrfCenPtandNormal(this Brep inBrep)
        {

            var brepFace = inBrep.Faces[0];

            double centerU, centerV;

            if (brepFace.IsPlanar() && brepFace.IsSurface)
            {
                centerU = brepFace.Domain(0).Mid;
                centerV = brepFace.Domain(1).Mid;
            }
            else
            {
                var centroid = AreaMassProperties.Compute(brepFace).Centroid;
                brepFace.ClosestPoint(centroid, out centerU, out centerV);

            }

            Point3d centerPt = brepFace.PointAt(centerU, centerV);
            Vector3d normalVector = brepFace.NormalAt(centerU, centerU);

            return (centerPt, normalVector);
        }





    }
}
