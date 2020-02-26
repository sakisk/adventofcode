using System.IO;
using System.Linq;
using System.Net;
using AdventOfCode2019.Day1;
using FluentAssertions;
using Xunit;

namespace AdventOfCode2019Tests.Day1
{
    public class SolutionTests
    {
        [Theory]
        [InlineData(12, 2)]
        [InlineData(14, 2)]
        [InlineData(1969, 654)]
        [InlineData(100756, 33583)]
        public void FuelCalculatorShouldCalculateFuelBasedOnMass(double mass, double fuel)
            => FuelCalculator.Calculate(mass).Should().Be(fuel);

        [Fact]
        public void ShouldSumFuelRequirements() =>
            new double[] {12, 14, 1969, 100756}
                .Sum(FuelCalculator.Calculate)
                .Should().Be(2 + 2 + 654 + 33583);

        [Fact]
        public void Part1() =>
            File.ReadAllLines("Day1/input")
                .Select(double.Parse)
                .Sum(FuelCalculator.Calculate)
                .Should().Be(3297866);
    }
}