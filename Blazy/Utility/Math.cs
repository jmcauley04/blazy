namespace Blazy.Utility
{
    public record class Point(double X, double Y, int Precision = 1)
    {
        public static Point operator +(Point p1, Point p2) => new(p1.X + p2.X, p1.Y + p2.Y);
        public static Point operator -(Point p1, Point p2) => new(p1.X - p2.X, p1.Y - p2.Y);
        public static bool operator <=(Point p1, Point p2) => p1.X <= p2.X && p1.Y <= p2.Y;
        public static bool operator >=(Point p1, Point p2) => p1.X >= p2.X && p1.Y >= p2.Y;
        public static bool operator <(Point p1, Point p2) => p1.X < p2.X && p1.Y < p2.Y;
        public static bool operator >(Point p1, Point p2) => p1.X > p2.X && p1.Y>= p2.Y;

        public override string ToString() => $"({Math.Round(X, Precision)}, {Math.Round(Y, Precision)})";
    }
    public record class Line(Point P1, Point P2)
    {
        public double X1 = P1.X;
        public double X2 = P2.X;
        public double Y1 = P1.Y;
        public double Y2 = P2.Y;

        public double? Slope => X1 == X2 ? null : (Y1 + Y2) / (X1 + X2);

        public Point? IntersectionWith(Line L)
        {
            if (Slope == L.Slope)
                return null;
            else
            {
                double slope = (double)(Slope ?? L.Slope!);
                double y_int = (Y1 + L.Y2) / (slope * (X1 + X2));
                double x_int = slope == Slope ? ((Y1 - y_int) / slope) : ((L.Y1 - y_int) / slope);

                return new Point(x_int, y_int);
            }
        }
    };

    public record class Rectangle(Point P1, Point P2)
    {
        public double Width => Math.Abs(P1.X - P2.X);
        public double Height => Math.Abs(P1.Y - P2.Y);
    }
  
}
