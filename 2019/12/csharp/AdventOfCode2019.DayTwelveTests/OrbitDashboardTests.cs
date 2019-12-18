using System.Collections.Generic;
using AdventOfCode2019.DayTwelve;
using FluentAssertions;
using Xunit;

namespace AdventOfCode2019.DayTwelveTests
{
    public class OrbitDashboardTests
    {
        [Fact]
        public void ShouldInitializeOrbitDashboardWithCorrectInitialMoonPositionsAndVelocities()
        {
            const string moonStartingPositions = @"<x=-1, y=0, z=2>
<x=2, y=-10, z=-7>
<x=4, y=-8, z=8>
<x=3, y=5, z=-1>";

            var expected = new List<(int, int, int)>
            {
                (-1,0,2),
                (2,-10,-7),
                (4,-8,8),
                (3,5,-1)
            };

            var orbitDashboard = new OrbitDashboard(moonStartingPositions);

            orbitDashboard.Positions.Should().HaveCount(4);
            orbitDashboard.Positions.Should().BeEquivalentTo(expected);

            orbitDashboard.Velocities.Should().HaveCount(4);
            orbitDashboard.Velocities.Should().AllBeEquivalentTo((0,0,0));
        }
    }
}
