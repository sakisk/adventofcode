using System.IO;
using System.Linq;
using AdventOfCode2019.DayOne;
using FluentAssertions;
using Xunit;

namespace AdventOfCode2019.DayOneTests
{
    public class RocketEquationDoubleCheckerTests
    {
        [Theory]
        [InlineData(14, 2)]
        [InlineData(1969, 654)]
        [InlineData(654, 216)]
        [InlineData(216, 70)]
        [InlineData(70, 21)]
        [InlineData(21, 5)]
        public void ShouldComputeNextFuelNeededForCurrentMassOfFuel(double mass, double fuel) =>
            new RocketEquationDoubleChecker(mass).Next().Fuel().Should().Be(fuel);

        [Theory]
        [InlineData(14, 2)]
        [InlineData(1969, 966)]
        [InlineData(100756, 50346)]
        public void ShouldComputeTotalFuelForAComponentBasedOnMass(double mass, double totalFuel) =>
            new RocketEquationDoubleChecker(mass).TotalFuel().Should().Be(totalFuel);

        [Fact]
        public void ShouldComputeTotalFuelForAllComponentsBasedOnMass() =>
            File.ReadAllLines("input")
                .Select(double.Parse)
                .Sum(x => new RocketEquationDoubleChecker(x).TotalFuel())
                .Should().Be(4943923);
    }

}
