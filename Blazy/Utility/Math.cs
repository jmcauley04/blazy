using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazy.Utility
{
    public class Math
    {
        public record class Point(double X, double Y);
        public record class Line(Point P1, Point P2)
        {
            public double X1 = P1.X;
            public double X2 = P2.X;
            public double Y1 = P1.Y;
            public double Y2 = P2.Y;

            public double? Slope => X1 == X2 ? null : (Y1 + Y2) / (X1 + X2);
        };

        public Point? Interesection(Line L1, Line L2)
        {
            if (L1.Slope == L2.Slope)
                return null;
            else
            {
                double slope = (double)(L1.Slope ?? L2.Slope!);
                double y_int = (L1.Y1 + L2.Y2) / (slope * (L1.X1 + L1.X2));
                double x_int = slope == L1.Slope ? ((L1.Y1 - y_int) / slope) : ((L2.Y1 - y_int) / slope);

                return new Point(x_int, y_int);
            }
        }        
    }
}
