using System.Collections.Generic;
using AdventOfCode2019.DayTwelve;
using FluentAssertions;
using Xunit;

namespace AdventOfCode2019.DayTwelveTests
{
    public class OrbitDashboardTests
    {
        private readonly OrbitDashboard _orbitDashboard;

        private const string MoonStartingPositions = @"<x=-1, y=0, z=2>
<x=2, y=-10, z=-7>
<x=4, y=-8, z=8>
<x=3, y=5, z=-1>";

        public OrbitDashboardTests()
        {
            _orbitDashboard = new OrbitDashboard(MoonStartingPositions);
        }

        [Fact]
        public void ShouldInitializeOrbitDashboardWithCorrectInitialMoonPositionsAndVelocities()
        {

            var expected = new List<(int, int, int)>
            {
                (-1, 0, 2),
                (2, -10, -7),
                (4, -8, 8),
                (3, 5, -1)
            };

            _orbitDashboard.Positions.Should().BeEquivalentTo(expected);
            _orbitDashboard.Velocities.Should().HaveCount(4);
            _orbitDashboard.Velocities.Should().AllBeEquivalentTo((0, 0, 0));
        }

        [Fact]
        public void ShouldApplyGravityWithVelocitiesAndPositionsAdjustedCorrectly()
        {
            _orbitDashboard.ApplyGravity();

            var expectedPositions = new[]
            {
                (2, -1, 1),
                (3, -7, -4),
                (1, -7, 5),
                (2, 2, 0)
            };

            var expectedVelocities = new[]
            {
                (3, -1, -1),
                (1, 3, 3),
                (-3, 1, -3),
                (-1, -3, 1)
            };

            _orbitDashboard.Velocities.Should().BeEquivalentTo(expectedVelocities);
            _orbitDashboard.Positions.Should().BeEquivalentTo(expectedPositions);
        }

        [Theory]
        [MemberData(nameof(MovingStepsTestCases))]
        public void ShouldShowCorrectPositionsAfterStepsOfGravitationalChanges(string currentPosition, (int X, int Y, int Z)[] nextPosition, int steps)
        {
            var orbitDashboard = new OrbitDashboard(currentPosition);

            orbitDashboard.Move(steps);

            orbitDashboard.Positions.Should().BeEquivalentTo(nextPosition);
        }

        [Fact]
        public void ShouldCalculateSystemTotalEnergy()
        {
            const string positions = @"<x=-1, y=0, z=2>
<x=2, y=-10, z=-7>
<x=4, y=-8, z=8>
<x=3, y=5, z=-1>";

            var orbitDashboard = new OrbitDashboard(positions);

            orbitDashboard.Move(10);

            orbitDashboard.TotalEnergy.Should().Be(179);
        }

        [Fact]
        public void PartOneSolution()
        {
            const string positions = @"<x=17, y=-9, z=4>
<x=2, y=2, z=-13>
<x=-1, y=5, z=-1>
<x=4, y=7, z=-7>";

            var orbitDashboard = new OrbitDashboard(positions);

            orbitDashboard.Move(1000);

            orbitDashboard.TotalEnergy.Should().Be(7202);
        }

        public static IEnumerable<object[]> MovingStepsTestCases => new List<object[]>
        {
            new object[]
            {
                @"<x=-1, y=0, z=2>
<x=2, y=-10, z=-7>
<x=4, y=-8, z=8>
<x=3, y=5, z=-1>",
                new[]
                {
                    (2, -1, 1),
                    (3, -7, -4),
                    (1, -7, 5),
                    (2, 2, 0)
                },
                1
            },
            new object[]
            {
                @"<x=-1, y=0, z=2>
<x=2, y=-10, z=-7>
<x=4, y=-8, z=8>
<x=3, y=5, z=-1>",
                new[]
                {
                    (5, -3, -1),
                    (1, -2, 2),
                    (1, -4, -1),
                    (1, -4, 2)
                },
                2
            },
            new object[]
            {
                @"<x=-1, y=0, z=2>
<x=2, y=-10, z=-7>
<x=4, y=-8, z=8>
<x=3, y=5, z=-1>",
                new[]
                {
                    (5, -6, -1),
                    (0, 0, 6),
                    (2, 1, -5),
                    (1, -8, 2)
                },
                3
            },
            new object[]
            {
                @"<x=-1, y=0, z=2>
<x=2, y=-10, z=-7>
<x=4, y=-8, z=8>
<x=3, y=5, z=-1>",
                new[]
                {
                    (2, -8, 0),
                    (2, 1, 7),
                    (2, 3, -6),
                    (2, -9, 1)
                },
                4
            },
            new object[]
            {
                @"<x=-1, y=0, z=2>
<x=2, y=-10, z=-7>
<x=4, y=-8, z=8>
<x=3, y=5, z=-1>",
                new[]
                {
                    (-1, -9, 2),
                    (4, 1, 5),
                    (2, 2, -4),
                    (3, -7, -1)
                },
                5
            },
            new object[]
            {
                @"<x=-1, y=0, z=2>
<x=2, y=-10, z=-7>
<x=4, y=-8, z=8>
<x=3, y=5, z=-1>",
                new[]
                {
                    (-1, -7, 3),
                    (3, 0, 0),
                    (3, -2, 1),
                    (3, -4, -2)
                },
                6
            },
            new object[]
            {
                @"<x=-1, y=0, z=2>
<x=2, y=-10, z=-7>
<x=4, y=-8, z=8>
<x=3, y=5, z=-1>",
                new[]
                {
                    (2, -2, 1),
                    (1, -4, -4),
                    (3, -7, 5),
                    (2, 0, 0)
                },
                7
            },
            new object[]
            {
                @"<x=-1, y=0, z=2>
<x=2, y=-10, z=-7>
<x=4, y=-8, z=8>
<x=3, y=5, z=-1>",
                new[]
                {
                    (5, 2, -2),
                    (2, -7, -5),
                    (0, -9, 6),
                    (1, 1, 3)
                },
                8
            },
            new object[]
            {
                @"<x=-1, y=0, z=2>
<x=2, y=-10, z=-7>
<x=4, y=-8, z=8>
<x=3, y=5, z=-1>",
                new[]
                {
                    (5, 3, -4),
                    (2, -9, -3),
                    (0, -8, 4),
                    (1, 1, 5)
                },
                9
            },
            new object[]
            {
                @"<x=-1, y=0, z=2>
<x=2, y=-10, z=-7>
<x=4, y=-8, z=8>
<x=3, y=5, z=-1>",
                new[]
                {
                    (2, 1, -3),
                    (1, -8, 0),
                    (3, -6, 1),
                    (2, 0, 4)
                },
                10
            }
        };
    }
}
