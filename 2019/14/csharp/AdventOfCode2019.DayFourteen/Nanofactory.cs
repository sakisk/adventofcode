using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode2019.DayFourteen
{
    public class Nanofactory
    {
        public IEnumerable<((long Quantity, string Chemical) Output, IEnumerable<(long Quantity, string Chemical)> Ingredients)> FuelRecipe { get; }

        private readonly Regex _ingredientRegex = new Regex(@"(?'quantity'\d+) (?'chemical'[A-Z]+)");
        public Nanofactory(string reactions) => FuelRecipe = ParseReactions(reactions);

        private IEnumerable<((long Quantity, string Chemical) Output, IEnumerable<(long Quantity, string Chemical)> Ingredients)> ParseReactions(string reactions)
        {
            foreach (var reaction in reactions.Split(Environment.NewLine))
            {
                var parts = reaction.Split("=>").Select(x => x.Trim()).ToList();
                var result = _ingredientRegex.Match(parts.Last());
                var ingredients = _ingredientRegex.Matches(parts.First());

                yield return (Output: ParseIngredients(result), Ingredients: ingredients.Select(ParseIngredients));
            }

            static (long Quantity, string Chemical) ParseIngredients(Match reaction) =>
            (
                Quantity: long.Parse(reaction.Groups["quantity"].Value),
                Chemical: reaction.Groups["chemical"].Value
            );
        }

        public long React(long fuel = 1L)
        {
            var ore = 0L;
            var reactor = new Queue<(long Quantity, string Chemical)>(new[] {(Quantity: fuel, Chemical: "FUEL")});
            var leftOvers = FuelRecipe.ToDictionary(x => x.Output.Chemical, _ => 0L);

            while (reactor.Any())
            {
                var (quantity, chemical) = reactor.Dequeue();
                if (chemical == "ORE")
                    ore += quantity;
                else
                {
                    var leftOver = Math.Min(quantity, leftOvers[chemical]);
                    quantity -= leftOver;
                    leftOvers[chemical] -= leftOver;
                    if (quantity > 0)
                    {
                        var ((outputQuantity, _), ingredients) = FuelRecipe.Single(x => x.Output.Chemical == chemical);
                        var multiplier = (long) Math.Ceiling((decimal) quantity / outputQuantity);
                        leftOvers[chemical] = Math.Max(0L, multiplier * outputQuantity - quantity);
                        foreach (var (q, c) in ingredients)
                            reactor.Enqueue((q * multiplier, c));
                    }
                }
            }

            return ore;
        }
    }
}