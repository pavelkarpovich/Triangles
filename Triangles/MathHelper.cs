namespace Triangles
{
    public static class MathHelper
    {
        // Check whether there are collinear points
        public static bool AnyCollinear(List<Triangle> triangles)
        {
            foreach (var t in triangles)
            {
                if (ArePointsCollinear(t.PointA, t.PointB, t.PointC))
                    return true;
            }
            return false;
        }

        // Check whether 2 points are collinear
        public static bool ArePointsCollinear(Point point1, Point point2, Point point3)
        {
            if (point1.X != point2.X)
            {
                double offset = (double)(point2.X * point1.Y - point1.X * point2.Y) / (point2.X - point1.X);
                double factor = (double)(point2.Y - point1.Y) / (point2.X - point1.X);
                if (point3.Y == factor * point3.X + offset)
                    return true;
            }
            else if (point1.X == point3.X)
                return true;
            return false;
        }

        // Check whether any triangles in the list are intersected
        public static bool AreIntersected(List<Triangle> triangles)
        {
            List<Triangle> checkedTriangles = new List<Triangle>();
            foreach (var t1 in triangles)
            {
                foreach (var t2 in checkedTriangles)
                {
                    if (AreIntersected(t1, t2))
                        return true;
                }
                checkedTriangles.Add(t1);
            }
            return false;
        }

        // Check whether 2 triangles are intersected
        public static bool AreIntersected(Triangle t1, Triangle t2)
        {
            bool isPointAInsideT1 = IsPointInsideTriangle(t1, t2.PointA);
            bool isPointBInsideT1 = IsPointInsideTriangle(t1, t2.PointB);
            bool isPointCInsideT1 = IsPointInsideTriangle(t1, t2.PointC);

            bool isPointAInsideT2 = IsPointInsideTriangle(t2, t1.PointA);
            bool isPointBInsideT2 = IsPointInsideTriangle(t2, t1.PointB);
            bool isPointCInsideT2 = IsPointInsideTriangle(t2, t1.PointC);

            bool[] isInsideT1 = { isPointAInsideT1, isPointBInsideT1, isPointCInsideT1 };
            bool[] isInsideT2 = { isPointAInsideT2, isPointBInsideT2, isPointCInsideT2 };

            if (isInsideT1.Count(x => x == true) > 0 && isInsideT1.Count(x => x == false) > 0 ||
                isInsideT2.Count(x => x == true) > 0 && isInsideT2.Count(x => x == false) > 0)
                return true;
            else return false;
        }

        // Check whether the point is inside the triangle
        public static bool IsPointInsideTriangle(Triangle t, Point p)
        {
            if (Check2Points(t.PointA, t.PointB, t.PointC, p) &&
                Check2Points(t.PointB, t.PointC, t.PointA, p) &&
                Check2Points(t.PointA, t.PointC, t.PointB, p)
                )
                return true;
            else return false;
        }

        // Compare whether one triangle is inside the other
        // 0 - triangle 2 is inside triangle 1
        // 1 - triangle 1 is inside triangle 2
        // 2 - no inside
        public static int CompareTriangles(Triangle t1, Triangle t2)
        {
            if (Check2Points(t1.PointA, t1.PointB, t1.PointC, t2.PointA) &&
                Check2Points(t1.PointA, t1.PointB, t1.PointC, t2.PointB) &&
                Check2Points(t1.PointA, t1.PointB, t1.PointC, t2.PointC)
                &&
                Check2Points(t1.PointB, t1.PointC, t1.PointA, t2.PointA) &&
                Check2Points(t1.PointB, t1.PointC, t1.PointA, t2.PointB) &&
                Check2Points(t1.PointB, t1.PointC, t1.PointA, t2.PointC)
                &&
                Check2Points(t1.PointA, t1.PointC, t1.PointB, t2.PointA) &&
                Check2Points(t1.PointA, t1.PointC, t1.PointB, t2.PointB) &&
                Check2Points(t1.PointA, t1.PointC, t1.PointB, t2.PointC)
                )
                return 0;
            else
            if (Check2Points(t2.PointA, t2.PointB, t2.PointC, t1.PointA) &&
                Check2Points(t2.PointA, t2.PointB, t2.PointC, t1.PointB) &&
                Check2Points(t2.PointA, t2.PointB, t2.PointC, t1.PointC)
                &&
                Check2Points(t2.PointB, t2.PointC, t2.PointA, t1.PointA) &&
                Check2Points(t2.PointB, t2.PointC, t2.PointA, t1.PointB) &&
                Check2Points(t2.PointB, t2.PointC, t2.PointA, t1.PointC)
                &&
                Check2Points(t2.PointA, t2.PointC, t2.PointB, t1.PointA) &&
                Check2Points(t2.PointA, t2.PointC, t2.PointB, t1.PointB) &&
                Check2Points(t2.PointA, t2.PointC, t2.PointB, t1.PointC)
                )
                return 1;
            else return 2;
        }

        // Check that 2 points are on the same side from the line
        public static bool Check2Points(Point linePoint1, Point linePoint2, Point point1, Point point2)
        {
            if (linePoint1.X != linePoint2.X)
            {
                double offset = (double)(linePoint2.X * linePoint1.Y - linePoint1.X * linePoint2.Y) / (linePoint2.X - linePoint1.X);
                double factor = (double)(linePoint2.Y - linePoint1.Y) / (linePoint2.X - linePoint1.X);
                double y1 = factor * point1.X + offset;
                double y2 = factor * point2.X + offset;
                if (point1.Y >= y1 && point2.Y >= y2 || point1.Y <= y1 && point2.Y <= y2)
                    return true;
                else return false;
            }
            else
            {
                if (point1.X >= linePoint1.X && point2.X >= linePoint1.X || point1.X <= linePoint1.X && point2.X <= linePoint1.X)
                    return true;
                else return false;
            }
        }
    }
}
