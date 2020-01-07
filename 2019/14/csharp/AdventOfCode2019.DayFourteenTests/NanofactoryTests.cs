using System.Collections.Generic;
using System.Linq;
using AdventOfCode2019.DayFourteen;
using FluentAssertions;
using Xunit;

namespace AdventOfCode2019.DayFourteenTests
{
    public class NanofactoryTestsParsingReactions
    {
        [Theory]
        [MemberData(nameof(Reactions))]
        public void ShouldParseChemicalsForASingleReactionCorrectly(string input, (int Quantity, string Chemical) result, (int Quantity, string Chemical)[] ingredients)
        {
            var reactions = new Nanofactory(input).FuelRecipe.ToList();
            reactions.Should().ContainSingle(x => x.Output.Quantity == result.Quantity && x.Output.Chemical == result.Chemical);
            reactions.Single().Ingredients.Should().BeEquivalentTo(ingredients);
        }

        public static IEnumerable<object[]> Reactions => new[]
        {
            new object[]
            {
                "10 ORE => 10 A", (Quantity: 10, Name: "A"), new[]
                {
                    (Quantity: 10, Name: "ORE")
                }
            },
            new object[]
            {
                "7 A, 1 B => 1 C", (Quantity: 1, Name: "C"), new[]
                {
                    (Quantity: 1, Name: "B"),
                    (Quantity: 7, Name: "A"),
                }
            }
        };
    }
}