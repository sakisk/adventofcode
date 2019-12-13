using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode2019.DayTen;
using FluentAssertions;
using Xunit;

namespace AdventOfCode2019.DayTenTests
{
    public class AsteroidMonitoringStationTests
    {
        [Theory]
        [InlineData(".", 0)]
        [InlineData("..", 0)]
        [InlineData(@"..
..", 0)]
        [InlineData(@"#", 1)]
        [InlineData(@"##", 2)]
        [InlineData(@"##
#.", 3)]
        public void ShouldHaveCorrectCountOfAsteroidsOnMap(string map, int asteroidsCount)
        {
            var m = map.Split(Environment.NewLine);

            AsteroidMonitoringStation.Create(m).Asteroids.Should().HaveCount(asteroidsCount);
        }

        [Theory]
        [InlineData("#", 0, 0)]
        [InlineData(".#", 1, 0)]
        [InlineData(@"..
#.", 0, 1)]
        [InlineData(@"..
.#", 1, 1)]
        public void ShouldHaveCorrectAsteroidCoordinates(string map, int x, int y)
        {
            var m = map.Split(Environment.NewLine);

            AsteroidMonitoringStation.Create(m).Asteroids.Should().ContainSingle(asteroid => asteroid.X == x && asteroid.Y == y);
        }

        [Theory]
        [InlineData(@"###
.##
...", 1, 0, 4)]
        [InlineData(@".#..#
.....
#####
....#
...##", 3, 4, 8)]
        [InlineData(@".#..#
.....
#####
....#
...##", 3, 4, 8)]
        [InlineData(@"......#.#.
#..#.#....
..#######.
.#.#.###..
.#..#.....
..#....#.#
#..#....#.
.##.#..###
##...#..#.
.#....####", 5, 8, 33)]
        [InlineData(@"#.#...#.#.
.###....#.
.#....#...
##.#.#.#.#
....#.#.#.
.##..###.#
..#...##..
..##....##
......#...
.####.###.", 1, 2, 35)]
        [InlineData(@".#..#..###
####.###.#
....###.#.
..###.##.#
##.##.#.#.
....###..#
..#.#..#.#
#..#.#.###
.##...##.#
.....#.#..", 6, 3, 41)]
        [InlineData(@".#..##.###...#######
##.############..##.
.#.######.########.#
.###.#######.####.#.
#####.##.#.##.###.##
..#####..#.#########
####################
#.####....###.#.#.##
##.#################
#####.##.###..####..
..######..##.#######
####.##.####...##..#
.#####..#.######.###
##...#.##########...
#.##########.#######
.####.#.###.###.#.##
....##.##.###..#####
.#.#.###########.###
#.#.#.#####.####.###
###.##.####.##.#..##", 11, 13, 210)]
        public void ShouldDetectAsteroidsInDirectLineOfSight(string map, int x, int y, int detectedAsteroidsCount)
        {
            var m = map.Split(Environment.NewLine);

            var expectedStation = (x, y);

            var asteroidMonitoringStation = AsteroidMonitoringStation.Create(m);

            asteroidMonitoringStation.DetectAsteroidsInSight((x, y)).Should().HaveCount(detectedAsteroidsCount);

            asteroidMonitoringStation.FindLocationWithMostAsteroidsInDirectSight().Should().Be(expectedStation);
        }

        [Fact]
        public void SolutionForPartOne()
        {
            const string map = @"#....#.....#...#.#.....#.#..#....#
#..#..##...#......#.....#..###.#.#
#......#.#.#.....##....#.#.....#..
..#.#...#.......#.##..#...........
.##..#...##......##.#.#...........
.....#.#..##...#..##.....#...#.##.
....#.##.##.#....###.#........####
..#....#..####........##.........#
..#...#......#.#..#..#.#.##......#
.............#.#....##.......#...#
.#.#..##.#.#.#.#.......#.....#....
.....##.###..#.....#.#..###.....##
.....#...#.#.#......#.#....##.....
##.#.....#...#....#...#..#....#.#.
..#.............###.#.##....#.#...
..##.#.........#.##.####.........#
##.#...###....#..#...###..##..#..#
.........#.#.....#........#.......
#.......#..#.#.#..##.....#.#.....#
..#....#....#.#.##......#..#.###..
......##.##.##...#...##.#...###...
.#.....#...#........#....#.###....
.#.#.#..#............#..........#.
..##.....#....#....##..#.#.......#
..##.....#.#......................
.#..#...#....#.#.....#.........#..
........#.............#.#.........
#...#.#......#.##....#...#.#.#...#
.#.....#.#.....#.....#.#.##......#
..##....#.....#.....#....#.##..#..
#..###.#.#....#......#...#........
..#......#..#....##...#.#.#...#..#
.#.##.#.#.....#..#..#........##...
....#...##.##.##......#..#..##....";

            var asteroidMonitoringStation = AsteroidMonitoringStation.Create(map.Split(Environment.NewLine));

            var stationLocation = asteroidMonitoringStation.FindLocationWithMostAsteroidsInDirectSight();

            stationLocation.Should().Be((26, 28));

            asteroidMonitoringStation.DetectAsteroidsInSight(stationLocation).Should().HaveCount(267);
        }


        [Fact]
        public void SolutionForPartTwo()
        {
            const string map = @"#....#.....#...#.#.....#.#..#....#
#..#..##...#......#.....#..###.#.#
#......#.#.#.....##....#.#.....#..
..#.#...#.......#.##..#...........
.##..#...##......##.#.#...........
.....#.#..##...#..##.....#...#.##.
....#.##.##.#....###.#........####
..#....#..####........##.........#
..#...#......#.#..#..#.#.##......#
.............#.#....##.......#...#
.#.#..##.#.#.#.#.......#.....#....
.....##.###..#.....#.#..###.....##
.....#...#.#.#......#.#....##.....
##.#.....#...#....#...#..#....#.#.
..#.............###.#.##....#.#...
..##.#.........#.##.####.........#
##.#...###....#..#...###..##..#..#
.........#.#.....#........#.......
#.......#..#.#.#..##.....#.#.....#
..#....#....#.#.##......#..#.###..
......##.##.##...#...##.#...###...
.#.....#...#........#....#.###....
.#.#.#..#............#..........#.
..##.....#....#....##..#.#.......#
..##.....#.#......................
.#..#...#....#.#.....#.........#..
........#.............#.#.........
#...#.#......#.##....#...#.#.#...#
.#.....#.#.....#.....#.#.##......#
..##....#.....#.....#....#.##..#..
#..###.#.#....#......#...#........
..#......#..#....##...#.#.#...#..#
.#.##.#.#.....#..#..#........##...
....#...##.##.##......#..#..##....";

            var asteroidMonitoringStation = AsteroidMonitoringStation.Create(map.Split(Environment.NewLine));

            while (asteroidMonitoringStation.Asteroids.Count > 1)
            {
                asteroidMonitoringStation = asteroidMonitoringStation.Vaporize((26, 28));
            }

            asteroidMonitoringStation.Vaporized.Select(x => x.Asteroid).ElementAt(199).Should().Be((13, 9));
        }

        [Theory]
        [InlineData(@".#..##.###...#######
##.############..##.
.#.######.########.#
.###.#######.####.#.
#####.##.#.##.###.##
..#####..#.#########
####################
#.####....###.#.#.##
##.#################
#####.##.###..####..
..######..##.#######
####.##.####...##..#
.#####..#.######.###
##...#.##########...
#.##########.#######
.####.#.###.###.#.##
....##.##.###..#####
.#.#.###########.###
#.#.#.#####.####.###
###.##.####.##.#..##", 11, 13)]
        public void ShouldVaporizeAsteroidsInCorrectOrder(string map, int x, int y)
        {
            var asteroidMonitoringStation = AsteroidMonitoringStation.Create(map.Split(Environment.NewLine));

            while (asteroidMonitoringStation.Asteroids.Count > 1)
            {
                asteroidMonitoringStation = asteroidMonitoringStation.Vaporize((x, y));
            }

            var indexes = new[] { 0, 1, 2, 9, 19, 49, 99, 198, 199, 200, 298 };
            asteroidMonitoringStation.Vaporized
                .Where((a, i) => indexes.Contains(i))
                .Select(a => a.Asteroid)
                .Should()
                .BeEquivalentTo(new[]
                {
                    (11, 12), (12, 1), (12, 2), (12, 8), (16, 0), (16, 9), (10, 16), (9, 6), (8, 2), (10, 9), (11, 1)
                });
        }

        [Theory]
        [ClassData(typeof(VaporizedAsteroidsData))]
        public void ShouldVaporizeCorrectAsteroidsInEveryRotation(string map, (int X, int Y) station, int batchSize, (int X, int Y)[] vaporized, int rotation)
        {
            var asteroidMonitoringStation = AsteroidMonitoringStation.Create(map.Split(Environment.NewLine));

            for (var i = 0; i < rotation; i++)
                asteroidMonitoringStation = asteroidMonitoringStation.Vaporize(station);

            asteroidMonitoringStation.Vaporized
                .Select(x => x.Asteroid)
                .Skip((rotation - 1) * batchSize)
                .Take(batchSize)
                .Should().BeEquivalentTo(vaporized);
        }
    }

    public class VaporizedAsteroidsData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[]
            {
                @".#....#####...#..
##...##.#####..##
##...#...#.#####.
..#.....X...###..
..#.#.....#....##",
                (8, 3), 9, new[] {(8,1),(9,0),(9,1),(10,0),(9,2),(11,1),(12,1),(11,2),(15,1)}, 1 };

            yield return new object[]
            {
                @".#....#####...#..
##...##.#####..##
##...#...#.#####.
..#.....X...###..
..#.#.....#....##",
                (8, 3), 9, new[] {(12,2),(13,2),(14,2),(15,2),(12,3),(16,4),(15,4),(10,4),(4,4)}, 2 };

            yield return new object[]
            {
                @".#....#####...#..
##...##.#####..##
##...#...#.#####.
..#.....X...###..
..#.#.....#....##",
                (8, 3), 9, new[] {(2,4),(2,3),(0,2),(1,2),(0,1),(1,1),(5,2),(1,0),(5,1)}, 3 };

            yield return new object[]
            {
                @".#....#####...#..
##...##.#####..##
##...#...#.#####.
..#.....X...###..
..#.#.....#....##",
                (8, 3), 9, new[] {(6,1),(6,0),(7,0),(8,0),(10,1),(14,0),(16,1),(13,3),(14,3)}, 4 };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }


}
