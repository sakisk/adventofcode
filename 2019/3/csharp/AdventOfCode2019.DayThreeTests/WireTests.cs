using System.IO;
using System.Linq;
using AdventOfCode2019.DayThree;
using FluentAssertions;
using Xunit;

namespace AdventOfCode2019.DayThreeTests
{
    public class WireTests
    {
        [Fact]
        public void ShouldStartWireFromCentralPort() => new Wire().Tip.Should().Be((0, 0));

        [Fact]
        public void ShouldNotFindCentralPortAsAnIntersection()
        {
            var wire = new Wire();
            var anotherWire = new Wire();

            wire.FindIntersections(anotherWire).Should().BeEmpty();
        }

        [Theory]
        [InlineData("R1", 1, 0)]
        [InlineData("L1", -1, 0)]
        [InlineData("L2", -2, 0)]
        [InlineData("R2", 2, 0)]
        [InlineData("R1,L1", 0, 0)]
        [InlineData("R2,L1", 1, 0)]
        public void ShouldTraceWireSegmentToHorizontalDirection(string path, int x, int y) => WireBuilder.Create(path).Tip.Should().Be((x, y));

        [Theory]
        [InlineData("U1", 0, 1)]
        [InlineData("U2", 0, 2)]
        [InlineData("D1", 0, -1)]
        [InlineData("D2", 0, -2)]
        [InlineData("U2,D1", 0, 1)]
        public void ShouldTraceWireSegmentToVerticalDirection(string path, int x, int y) => WireBuilder.Create(path).Tip.Should().Be((x, y));

        [Theory]
        [InlineData("R8,U5,L5,D3", 3, 2, 21)]
        public void ShouldTraceWireToCorrectTip(string path, int x, int y, int segmentCount)
        {
            var wire = WireBuilder.Create(path);
            wire.Tip.Should().Be((x, y));
            wire.Horizontal.Concat(wire.Vertical).Where(s => s != (0, 0)).ToList().Count.Should().Be(segmentCount);
        }

        [Theory]
        [InlineData("U7,R6,D4,L4", "R8,U5,L5,D3", 2)]
        public void ShouldFindAllIntersections(string wire1, string wire2, int intersectionsCount)
        {
            var wire = WireBuilder.Create(wire1);
            var anotherWire = WireBuilder.Create(wire2);

            wire.FindIntersections(anotherWire).Count().Should().Be(intersectionsCount);
        }

        [Theory]
        [InlineData("U7,R6,D4,L4", "R8,U5,L5,D3", 6)]
        [InlineData("R75,D30,R83,U83,L12,D49,R71,U7,L72", "U62,R66,U55,R34,D71,R55,D58,R83", 159)]
        [InlineData("R98,U47,R26,D63,R33,U87,L62,D20,R33,U53,R51", "U98,R91,D20,R16,D67,R40,U7,R15,U6,R7", 135)]
        public void ShouldFindDistanceToNearestIntersectionToCentralPort(string wire1, string wire2, int distance)
        {
            var wire = WireBuilder.Create(wire1);
            var anotherWire = WireBuilder.Create(wire2);

            wire.FindDistanceToNearestIntersectionFromCentralPort(anotherWire).Should().Be(distance);
        }

        [Fact]
        public void ShouldFindDistanceToNearestIntersectionToCentralPortForPuzzleInput()
        {
            var wirePaths = File.ReadAllLines("input");
            var wire = WireBuilder.Create(wirePaths.First());
            var anotherWire = WireBuilder.Create(wirePaths.Last());

            wire.FindDistanceToNearestIntersectionFromCentralPort(anotherWire).Should().Be(2129);
        }
    }
}
