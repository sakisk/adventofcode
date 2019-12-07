using System;
using System.IO;
using AdventOfCode2019.DaySix;
using FluentAssertions;
using Xunit;

namespace AdventOfCode2019.DaySixTests
{
    public class OrbitCounterTests
    {

        [Theory]
        [InlineData(@"COM)A
A)B
A)C", "COM", 3)]
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
            var graph = CreateMap(orbits);

            new OrbitCounter(graph).CountOrbitsFrom(centre).Should().Be(count);
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
            var graph = CreateMap(orbits);

            new OrbitCounter(graph).CountOrbitsForAllObjects().Should().Be(count);
        }

        [Fact]
        public void ShouldCountDirectAndIndirectOrbitsFromPuzzleInput()
        {
            var graph = CreateMap(File.ReadAllText("input"));

            new OrbitCounter(graph).CountOrbitsForAllObjects().Should().Be(249308);
        }

        private static OrbitGraph CreateMap(string orbits)
        {
            var graph = new OrbitGraph();

            foreach (var orbit in orbits.Split(Environment.NewLine))
            {
                graph.AddOrbit(orbit);
            }

            return graph;
        }

    }
}
