using System;
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

            new AsteroidMonitoringStation(m).Asteroids.Should().HaveCount(asteroidsCount);
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

            new AsteroidMonitoringStation(m).Asteroids.Should().ContainSingle(asteroid => asteroid.X == x && asteroid.Y == y);
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

            var asteroidMonitoringStation = new AsteroidMonitoringStation(m);

            asteroidMonitoringStation.DetectAsteroidsInSight((x, y)).Should().HaveCount(detectedAsteroidsCount);

            asteroidMonitoringStation.FindLocationWithMostAsteroidsInDirectSight().Should().Be(expectedStation);
        }

        [Fact]
        public void SolutionForPartOne()
        {
            var map = @"
#....#.....#...#.#.....#.#..#....#
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

            var asteroidMonitoringStation = new AsteroidMonitoringStation(map.Split(Environment.NewLine));

            var stationLocation = asteroidMonitoringStation.FindLocationWithMostAsteroidsInDirectSight();

            stationLocation.Should().Be((26, 29));

            asteroidMonitoringStation.DetectAsteroidsInSight(stationLocation).Should().HaveCount(267);
        }
    }
}
