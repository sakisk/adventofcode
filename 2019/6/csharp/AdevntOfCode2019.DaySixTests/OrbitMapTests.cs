using System;
using System.IO;
using System.Linq;
using System.Net;
using AdventOfCode2019.DaySix;
using FluentAssertions;
using Xunit;

namespace AdventOfCode2019.DaySixTests
{
    public class OrbitMapTests
    {
        private readonly OrbitMap _map;

        public OrbitMapTests()
        {
            _map = new OrbitMap();
        }

        [Fact]
        public void ShouldCreateMapWithCentreOfMassNode() => _map.Orbits.Should().ContainKey("COM");

        [Fact]
        public void ShouldCreateADirectOrbitOnAnEmptyMap()
        {
            _map.AddOrbit("A)B");
            _map.Orbits.Count.Should().Be(3);
            _map.Orbits.Should().ContainKey("A").And.ContainKey("B");
            _map.Orbits["A"].DirectOrbits.Should().ContainKey("B");
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
            AddOrbitsToMap(orbits);

            _map.Orbits[centre].DirectOrbits.Keys.Should().HaveCount(directOrbitingCount);
        }

        [Theory]
        [InlineData(@"COM)A
A)B
A)C","COM", 3)]
        [InlineData(@"COM)B
B)C
C)D
D)E
E)F
B)G
G)H
D)I
E)J
J)K
K)L", "COM", 11)]
        public void ShouldCountOrbitsStartingFromACentre(string orbits, string centre, int count)
        {
            AddOrbitsToMap(orbits);

            _map.CountOrbitsFrom(centre).Should().Be(count);
        }

        [Theory]
        [InlineData(@"COM)B
B)C
C)D
D)E
E)F
B)G
G)H
D)I
E)J
J)K
K)L", 42)]
        public void ShouldCountDirectAndIndrirectOrbitsInMap(string orbits, int count)
        {
            AddOrbitsToMap(orbits);

            _map.CountOrbitsForAllObjects().Should().Be(count);
        }

        [Fact]
        public void ShouldCountDirectAndIndirectOrbitsFromPuzzleInput()
        {
            AddOrbitsToMap(File.ReadAllText("input"));

            _map.CountOrbitsForAllObjects().Should().Be(11);
        }


        private void AddOrbitsToMap(string orbits)
        {
            foreach (var orbit in orbits.Split(Environment.NewLine))
            {
                _map.AddOrbit(orbit);
            }
        }
    }
}
