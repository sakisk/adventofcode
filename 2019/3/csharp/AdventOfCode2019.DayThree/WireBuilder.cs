namespace AdventOfCode2019.DayThree
{
    public class WireBuilder
    {
        public static Wire Create(string path)
        {
            var traces = path.Split(',');
            var wire = new Wire();

            wire.Parse(traces);

            return wire;
        }
    }
}