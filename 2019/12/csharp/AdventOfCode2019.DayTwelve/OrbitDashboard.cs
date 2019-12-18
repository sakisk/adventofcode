using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode2019.DayTwelve
{
    public class OrbitDashboard
    {
        public IReadOnlyCollection<(int X, int Y, int Z)> Positions { get; }
        public IReadOnlyCollection<(int X, int Y, int Z)> Velocities { get; }

        private readonly Regex _moonPositionRegex = new Regex(@"<x=(?'X'-?\d*), y=(?'Y'-?\d*), z=(?'Z'-?\d*)>", RegexOptions.Multiline);

        public OrbitDashboard(string moonStartingPositions)
        {
            Positions = _moonPositionRegex
                .Matches(moonStartingPositions)
                .Cast<Match>()
                .Select(x => (
                    X: int.Parse(x.Groups["X"].Value),
                    Y: int.Parse(x.Groups["Y"].Value),
                    Z: int.Parse(x.Groups["Z"].Value)))
                .ToList();

            Velocities = Positions.Select(x => (X: 0, Y: 0, Z: 0)).ToList();
        }
    }
}