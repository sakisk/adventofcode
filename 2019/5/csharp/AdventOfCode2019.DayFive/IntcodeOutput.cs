using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2019.DayFive
{
    public class IntcodeOutput
    {
        public int? Output { get; set; }
        public string Intcode { get; set; }

        public IntcodeOutput(IList<int> intcode, int? output)
        {
            Intcode = IntcodeToString(intcode);
            Output = output;
        }

        private static string IntcodeToString(IList<int> input) => string.Join(",", input.Select(x => x.ToString()).ToArray());
    }
}