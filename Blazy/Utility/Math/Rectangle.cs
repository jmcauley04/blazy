namespace Blazy.Utility.Math
{
    public record class Rectangle(Point P1, Point P2)
    {
        public double Width => System.Math.Abs(P1.X - P2.X);
        public double Height => System.Math.Abs(P1.Y - P2.Y);
    }
}
