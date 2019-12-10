using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2019.DaySeven
{
    public static class Helpers
    {
        public static IEnumerable<IEnumerable<int>> Permutations(this IEnumerable<int> values)
        {
            if (values.Count() == 1)
                return new[] { values };
            return values.SelectMany(v => Permutations(values.Where(x => x != v)), (v, p) => p.Prepend(v));
        }
    }
}