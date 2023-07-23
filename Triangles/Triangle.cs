namespace Triangles
{
    public class Triangle
    {
        public int Rank { get; set; }
        public Point PointA { get; set; }
        public Point PointB { get; set; }
        public Point PointC { get; set; }

        public Triangle(Point pointA, Point pointB, Point pointC)
        {
            PointA = pointA;
            PointB = pointB;
            PointC = pointC;
        }
    }
}
