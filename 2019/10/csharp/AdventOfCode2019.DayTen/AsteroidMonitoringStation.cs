using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2019.DayTen
{
    public class AsteroidMonitoringStation
    {
        public IReadOnlyCollection<(int X, int Y)> Asteroids { get; }

        public AsteroidMonitoringStation(string[] map)
        {
            Asteroids = GetAsteroids(map);
        }

        private static IReadOnlyCollection<(int, int)> GetAsteroids(IEnumerable<string> map) =>
            map.SelectMany((line, y) => line
                    .Select((legend, x) => (X: x, Y:y, Legend: legend))
                    .Where(x => x.Legend == '#')
                    .Select(asteroid => (asteroid.X, asteroid.Y)))
                .ToList();

        public IReadOnlyCollection<(int X, int Y)> DetectAsteroidsInSight((int X, int Y) station)
        {
            return Asteroids
                .Where(asteroid => asteroid != station)
                .GroupBy(asteroid => LineOfSight(asteroid, station))
                .Select(asteroidByDistance =>
                    asteroidByDistance.Select(asteroidInLine => new
                        {
                            asteroidInLine,
                            Distance = Distance(station, asteroidInLine)
                        })
                        .OrderBy(asteroid => asteroid.Distance)
                        .Select(directSight => directSight.asteroidInLine)
                        .First())
                .ToList();

            int Distance((int X, int Y) a, (int X, int Y) b) => Math.Abs(a.X - b.X) + Math.Abs(a.Y - b.Y);
            double LineOfSight((int X, int Y) a, (int X, int Y) b) => Math.Atan2(a.X - b.X, a.Y - b.Y);
        }

        public (int X, int Y) FindLocationWithMostAsteroidsInDirectSight() =>
            Asteroids
                .Select(a => new { Station = a, DetectAsteroidsInSight(a).Count })
                .OrderByDescending(asteroids => asteroids.Count)
                .First().Station;
    }
}