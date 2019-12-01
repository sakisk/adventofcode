using System.IO;
using System.Linq;
using AdventOfCode2019.DayOne;
using FluentAssertions;
using Xunit;

namespace AdventOfCode2019.DayOneTests
{
    public class FuelCounterUpperTests 
    {
        [Theory]
        [InlineData(12, 4)]
        [InlineData(14, 4)]
        public void ShouldDivideByThreeWithRoundingDown(double mass, double result) => 
            new FuelCounterUpper(mass).DivideByThree().Compute().Should().Be(result);

        [Theory]
        [InlineData(12, 2)]
        [InlineData(14, 2)]
        [InlineData(1969, 654)]
        [InlineData(100756, 33583)]
        public void ShouldComputeFuelForAModuleBasedOnMassCorrectly(double mass, double result) =>
            new FuelCounterUpper(mass).DivideByThree().TakeAwayTwo().Compute().Should().Be(result);

        [Theory]
        [InlineData(new[] {12d, 14d, 1969d, 100756d}, 34241)]
        public void ShouldSumFuelForAllModules(double[] masses, double sum) =>
            new FuelCounterUpper(masses).Sum().Should().Be(sum);

        [Fact]
        public void ShouldSumFuelForAllModuleMassesForPuzzleInput()
        {
            var masses = File.ReadAllLines("input").Select(double.Parse).ToArray();

            var result = new FuelCounterUpper(masses).Sum();

            result.Should().Be(3297866);
        }
    }
}
