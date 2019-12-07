using System;
using System.IO;
using AdventOfCode2019.DaySix;
using FluentAssertions;
using Xunit;

namespace AdventOfCode2019.DaySixTests
{
    public class SpaceObjectTransporterTests
    {
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
K)L
K)YOU
I)SAN", 4)]
        public void ShouldTransferYOUToSAN(string orbits, int transfers)
        {
            var graph = CreateGraph(orbits);

            new SpaceObjectTransporter(graph).CountTransfers("YOU", "SAN").Should().Be(transfers);
        }

        [Fact]
        public void ShouldTransferYOUToSANFromPuzzleInput()
        {
            var graph = CreateGraph(File.ReadAllText("input"));

            new SpaceObjectTransporter(graph).CountTransfers("YOU", "SAN").Should().Be(349);
        }

        private static OrbitGraph CreateGraph(string orbits)
        {
            var graph = new OrbitGraph();

            foreach (var orbit in orbits.Split(Environment.NewLine))
            {
                graph.AddOrbit(orbit, undirected: true);
            }

            return graph;
        }
    }
}
