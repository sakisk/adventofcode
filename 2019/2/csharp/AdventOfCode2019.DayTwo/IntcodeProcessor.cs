using System.Linq;

namespace AdventOfCode2019.DayTwo
{
    public class IntcodeProcessor
    {
        private readonly string _input;

        public IntcodeProcessor(string input)
        {
            _input = input;
            Result = input;
        }

        public IntcodeProcessor Process()
        {
            var ints = _input.Split(',').Select(int.Parse).ToList();
            var index = 0;
            var result = _input;

            while (index <= ints.Count)
            {
                var opcode = ints[index];

                if (opcode == 99)
                {
                    Result = result;
                    return new IntcodeProcessor(result);
                }
            }

            return new IntcodeProcessor(result);
        }

        public string Result { get; set; }
    }
}