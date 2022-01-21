using System.Linq;

namespace Blazy.Utility.Math
{
    public class NearestPointAlgorithm
    {
        Dictionary<Rectangle, List<Point>> PointsMap = new Dictionary<Rectangle, List<Point>>();

        Rectangle _containingSpace;
        double _sideLength;

        public NearestPointAlgorithm(IEnumerable<Point> points, int columns = 20)
        {
            if (points.Count() == 0)
                throw new ArgumentException("Algorithm requires at least one point", nameof(points));

            _containingSpace = new Rectangle(
                new(points.MinBy(p => p.X)!.X, points.MinBy(p => p.Y)!.Y),
                new(points.MaxBy(p => p.X)!.X, points.MaxBy(p => p.Y)!.Y));

            _sideLength = _containingSpace.Width / columns;

            // make squares
            for (var i = 0; i <= columns; i++)
                for (var j = 0; j <= _containingSpace.Height / _sideLength; j++)
                    PointsMap.Add(
                        GetRectangle(i, j),
                        new List<Point>());

            foreach (var point in points)
                PointsMap[GetSection(point)].Add(point);      
        }

        public Point? GetNearestPoint(Point p)
        {
            try
            {
                List<Rectangle> completedSections = new List<Rectangle>();

                // determine which space the new point is in
                var newPointsSection = GetSection(p);

                // find the min distance from the new point to the nearest point in the new point's space
                var closestPoint = GetNearestPoint(p, PointsMap[newPointsSection]);
                completedSections.Add(newPointsSection);

                return closestPoint.Item2;
                // repeat which neighboring spaces until the distance from the new point to the nearest neighboring space exceeds the min distance
                var distanceMaps = new List<(double, Rectangle)>();
                foreach (var section in PointsMap.Keys)
                    distanceMaps.Add((GetDistanceToSection(p, section), section));

                List<Rectangle> SectionsToCheck()
                {
                    var closeEnoughSections = distanceMaps.Where(s => s.Item1 < closestPoint.Item1).Select(x => x.Item2);
                    return closeEnoughSections.Except(completedSections).ToList();
                };

                var sectionsToCheck = SectionsToCheck();

                while (sectionsToCheck.Any())
                {
                    var checksection = sectionsToCheck.First();
                    var closestPointInSection = GetNearestPoint(p, PointsMap[checksection]);
                    if (closestPointInSection.Item1 < closestPoint.Item1)
                        closestPoint = closestPointInSection;

                    completedSections.Add(checksection);
                    sectionsToCheck = SectionsToCheck();
                }

                return closestPoint.Item2;
            }
            catch
            {
                return null;
            }
        }

        Rectangle GetSection(Point p)
        {
            var xOffset = (int)System.Math.Floor((p.X - _containingSpace.P1.X) / _sideLength);
            var yOffset = (int)System.Math.Floor((p.Y - _containingSpace.P1.Y) / _sideLength);

            return GetRectangle(xOffset, yOffset);
        }

        Rectangle GetRectangle(int xOffset, int yOffset, int precision = 0)
            => new Rectangle(
                    new(
                        System.Math.Round(_containingSpace.P1.X + xOffset * _sideLength, precision), 
                        System.Math.Round(_containingSpace.P1.X + (xOffset + 1) * _sideLength, precision)),
                    new(
                        System.Math.Round(_containingSpace.P1.Y + yOffset * _sideLength, precision), 
                        System.Math.Round(_containingSpace.P1.Y + (yOffset + 1) * _sideLength, precision)));

        (double, Point?) GetNearestPoint(Point point, IEnumerable<Point> points)
        {
            int leastDistance = int.MaxValue;
            Point? nearestPt = null;

            foreach (var pt in points)
            {
                var distToPointSqrd = System.Math.Pow(pt.X - point.X, 2) + System.Math.Pow(pt.Y - point.Y, 2);

                if (distToPointSqrd < leastDistance)
                {
                    nearestPt = pt;
                    leastDistance = (int)distToPointSqrd;
                }
            }

            return (leastDistance, nearestPt);
        }

        double GetDistanceToSection(Point point, Rectangle rectangle)
        {
            return GetNearestPoint(point,
                new List<Point> {
                    rectangle.P1,
                    rectangle.P2,
                    rectangle.P1 + new Point(rectangle.Width, 0),
                    rectangle.P1 + new Point(0, rectangle.Height)
                }).Item1;
        }
    }
}
