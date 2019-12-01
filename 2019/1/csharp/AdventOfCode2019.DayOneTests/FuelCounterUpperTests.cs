using AdventOfCode2019.DayOne;
using FluentAssertions;
using Xunit;

namespace AdventOfCode2019.DayOneTests
{
    public class FuelCounterUpperTests 
    {
        [Fact]
        public void ShouldDivideByThree()
        {
            new FuelCounterUpper(12).DivideByThree().Compute().Should().Be(4);
        }
    }
}
