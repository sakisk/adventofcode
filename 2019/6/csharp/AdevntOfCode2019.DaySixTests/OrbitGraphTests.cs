using System;
using AdventOfCode2019.DaySix;
using FluentAssertions;
using Xunit;

namespace AdventOfCode2019.DaySixTests
{
    public class OrbitGraphTests
    {
        private readonly OrbitGraph _orbitGraph;

        public OrbitGraphTests()
        {
            _orbitGraph = new OrbitGraph();
        }

        [Fact]
        public void ShouldCreateMapWithCentreOfMassNode() => _orbitGraph.OrbitingObjects.Should().ContainKey("COM");

        [Fact]
        public void ShouldCreateADirectOrbitOnAnEmptyMap()
        {
            _orbitGraph.AddOrbit("A)B");
            _orbitGraph.OrbitingObjects.Count.Should().Be(3);
            _orbitGraph.OrbitingObjects.Should().ContainKey("A").And.ContainKey("B");
            _orbitGraph.OrbitingObjects["A"].DirectOrbits.Should().ContainKey("B");
        }

        [Theory]
        [InlineData("COM)A", "COM", 1)]
        [InlineData(@"COM)A
COM)B", "COM", 2)]
        [InlineData(@"COM)A
COM)B
COM)C", "COM", 3)]
        [InlineData(@"COM)A
A)B
A)C", "A", 2)]
        public void ShouldAddOrbitingObjectToExistingCenters(string orbits, string centre, int directOrbitingCount)
        {
            foreach (var orbit in orbits.Split(Environment.NewLine))
            {
                _orbitGraph.AddOrbit(orbit);
            }

            _orbitGraph.OrbitingObjects[centre].DirectOrbits.Keys.Should().HaveCount(directOrbitingCount);
        }
    }
}