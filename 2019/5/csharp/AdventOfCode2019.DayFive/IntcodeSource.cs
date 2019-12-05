using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2019.DayFive
{
    public class IntcodeSource
    {
        public int? Input { get; set; }
        public IList<int> Intcode { get; set; }

        public IntcodeSource(string input, int? inputParameter)
        {
            Intcode = InputToIntcode(input);
            Input = inputParameter;
        }
        private static IList<int> InputToIntcode(string input) => input.Split(',').Select(int.Parse).ToList();
    }
}