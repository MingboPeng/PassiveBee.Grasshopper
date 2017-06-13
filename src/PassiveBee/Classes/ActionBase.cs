using Rhino.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PassiveBee
{
    public static class ActionBase
    {

        public static bool IsAntiClockWise(List<Point3d> Pts, Vector3d AtSrfNormal)
        {
            
            // check the order if clock-wise
            var vector0 = new Vector3d(Pts[1] - Pts.First());
            var vector1 = new Vector3d(Pts.Last() - Pts.First());

            var ptsNormal = Vector3d.CrossProduct(vector0, vector1);


            //# in case points are anti-clockwise then normals should be parallel
            var crossProduct = Vector3d.CrossProduct(ptsNormal, AtSrfNormal);
            if (crossProduct > Vector3d.Zero)
            {
                return true;
            }
            else
            {
                return false;
            }
        
        }


        //HBSurface Extension
        public static List<Point3d> ExtractPoints (this HBSurface HBSurface, bool IgnoreInsidePts)
        {
            var inBrep = HBSurface.Geometry;
            var antiClockWisePoints = new List<Point3d>();

            var (center, normal) = inBrep.GetSrfCenPtandNormal();

            var edgeCurves = inBrep.DuplicateEdgeCurves(true).ToList();
            var joinedCurves = Curve.JoinCurves(edgeCurves).ToList();

            if (IgnoreInsidePts)
            {
                //keep the outter edge
                joinedCurves = new List<Curve>() { joinedCurves.First() };
            }
            
            foreach (var curve in joinedCurves)
            {
                //var segments = curve.DuplicateSegments().ToList();
                var originalPoints = new List<Point3d>() { curve.PointAtStart };
                originalPoints.AddRange(curve.Discontinuities());
                var isAntiClockWise = IsAntiClockWise(originalPoints, normal);
                
                if (isAntiClockWise)
                {
                    originalPoints.Reverse();
                }

                antiClockWisePoints.AddRange(originalPoints);

            }
            
            return antiClockWisePoints;
        }
    }
}
