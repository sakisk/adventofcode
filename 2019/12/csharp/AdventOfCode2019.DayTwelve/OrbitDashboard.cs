using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode2019.DayTwelve
{
    public class OrbitDashboard
    {
        public IEnumerable<(int X, int Y, int Z)> Positions { get; private set; }
        public IEnumerable<(int X, int Y, int Z)> Velocities { get; private set; }
        public int TotalEnergy { get; private set; }

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

        public void ApplyGravity() => ApplyGravity(Positions.ToArray());

        public void ApplyGravity((int X, int Y, int Z)[] positions)
        {
            var velocitiesByPosition = (from first in positions
                                        from second in positions
                                        where first != second
                                        let velocityChange = new
                                        {
                                            Position = first,
                                            Velocity = (
                                                X: VelocityChange(first.X, second.X),
                                                Y: VelocityChange(first.Y, second.Y),
                                                Z: VelocityChange(first.Z, second.Z)
                                                )
                                        }
                                        group velocityChange by velocityChange.Position into velocityChanges
                                        select new
                                        {
                                            Position = velocityChanges.Key,
                                            Change = (
                                                X: velocityChanges.Sum(x => x.Velocity.X), 
                                                Y: velocityChanges.Sum(x => x.Velocity.Y), 
                                                Z: velocityChanges.Sum(x => x.Velocity.Z))
                                        }).ToList();

            Velocities = velocitiesByPosition.Select(x => x.Change).Zip(Velocities, (previous, current) => 
                (previous.X + current.X, previous.Y + current.Y, previous.Z + current.Z)).ToArray();

            Positions = Velocities.Zip(Positions, (moon, velocity) =>  (
                moon.X + velocity.X, 
                moon.Y + velocity.Y, 
                moon.Z + velocity.Z)).ToArray();

            var potentialEnergy = Positions.Select(x => Math.Abs(x.X) + Math.Abs(x.Y) + Math.Abs(x.Z));
            var kineticEnergy = Velocities.Select(x => Math.Abs(x.X) + Math.Abs(x.Y) + Math.Abs(x.Z));

            TotalEnergy = potentialEnergy.Zip(kineticEnergy, (p, k) => p * k).Sum();

            int VelocityChange(int a, int b) => a < b ? 1 : a > b ? -1 : 0;
        }

        public void Move(in int steps)
        {

            for (var i = 0; i < steps; i++) 
                ApplyGravity();
        }
    }
}