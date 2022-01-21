namespace Blazy.Utility.Math
{
    public record class Point(double X, double Y, int Precision = 1)
    {
        public static Point operator +(Point p1, Point p2) => new(p1.X + p2.X, p1.Y + p2.Y);
        public static Point operator -(Point p1, Point p2) => new(p1.X - p2.X, p1.Y - p2.Y);
        public static bool operator <=(Point p1, Point p2) => p1.X <= p2.X && p1.Y <= p2.Y;
        public static bool operator >=(Point p1, Point p2) => p1.X >= p2.X && p1.Y >= p2.Y;
        public static bool operator <(Point p1, Point p2) => p1.X < p2.X && p1.Y < p2.Y;
        public static bool operator >(Point p1, Point p2) => p1.X > p2.X && p1.Y>= p2.Y;

        public override string ToString() => $"({System.Math.Round(X, Precision)}, {System.Math.Round(Y, Precision)})";
    }
}
