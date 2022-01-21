namespace Blazy.Utility.Math
{
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
}
