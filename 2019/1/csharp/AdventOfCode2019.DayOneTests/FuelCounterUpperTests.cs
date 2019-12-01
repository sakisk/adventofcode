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
        [InlineData(1969, 654)]
        [InlineData(100756, 33583)]
        public void ShouldFindFuelForAModuleBasedOnMassCorrectly(double mass, double result) =>
            new FuelCounterUpper(mass).DivideByThree().TakeAwayTwo().Compute().Should().Be(result);
    }
}
