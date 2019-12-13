using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2019.DayTen
{
    public class AsteroidMonitoringStation
    {
        public IReadOnlyCollection<(int X, int Y)> Asteroids { get; }

        public IReadOnlyCollection<((int X, int Y) Asteroid, double Theta)> Vaporized { get; }

        public static AsteroidMonitoringStation Create(IEnumerable<string> map) =>
            new AsteroidMonitoringStation(GetAsteroids(map), Enumerable.Empty<((int X, int Y) Asteroid, double Theta)>().ToList());

        private AsteroidMonitoringStation(IReadOnlyCollection<(int X, int Y)> asteroids, IReadOnlyCollection<((int X, int Y) Asteroid, double Theta)> vaporized)
        {
            Asteroids = asteroids;
            Vaporized = vaporized;
        }

        private static IReadOnlyCollection<(int X, int Y)> GetAsteroids(IEnumerable<string> map) =>
            map.SelectMany((line, y) => line
                    .Select((legend, x) => (X: x, Y: y, Legend: legend))
                    .Where(x => x.Legend == '#')
                    .Select(asteroid => (asteroid.X, asteroid.Y)))
                .ToList();

        public IReadOnlyCollection<((int X, int Y) Asteroid, double Theta)> DetectAsteroidsInSight((int X, int Y) station)
        {
            return Asteroids
                .Where(x => x != station)
                .GroupBy(asteroid => LineOfSight(asteroid, station))
                .Select(asteroidByDistance =>
                    asteroidByDistance.Select(asteroidInLine => new
                    {
                        asteroidInLine,
                        Distance = Distance(station, asteroidInLine),
                        Theta = ToDegrees(LineOfSight(station, asteroidInLine))
                    })
                        .OrderBy(asteroid => asteroid.Distance)
                        .Select(directSight => (directSight.asteroidInLine, directSight.Theta))
                        .First())
                .ToList();

            int Distance((int X, int Y) a, (int X, int Y) b) => Math.Abs(a.X - b.X) + Math.Abs(a.Y - b.Y);
            double LineOfSight((int X, int Y) a, (int X, int Y) b) => Math.Atan2(a.X - b.X, a.Y - b.Y);
            double ToDegrees(double thetaRad) => (thetaRad / Math.PI * 180) + (thetaRad > 0 ? 0 : 360);
        }

        public (int X, int Y) FindLocationWithMostAsteroidsInDirectSight() =>
            Asteroids
                .Select(a => new { Station = a, DetectAsteroidsInSight(a).Count })
                .OrderByDescending(asteroids => asteroids.Count)
                .First().Station;

        public AsteroidMonitoringStation Vaporize((int X, int Y) station)
        {
            var inSight = DetectAsteroidsInSight(station)
                .OrderByDescending(x => x.Theta)
                .ToList();

            return new AsteroidMonitoringStation(
                Asteroids.Except(inSight.Select(x => x.Asteroid)).ToList(),
                Vaporized.Concat(inSight).ToList());
        }
    }
}