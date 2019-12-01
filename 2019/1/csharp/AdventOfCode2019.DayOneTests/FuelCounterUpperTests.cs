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
        public void ShouldDivideByThreeWithRoundingDown(int mass, int result) => 
            new FuelCounterUpper(mass).DivideByThree().Compute().Should().Be(result);
    }
}
