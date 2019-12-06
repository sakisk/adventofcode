using System.Collections.Generic;

namespace AdventOfCode2019.DaySix
{
    public class Orbit
    {
        public string Centre { get; }

        public Dictionary<string, Orbit> DirectOrbits;

        public Orbit(string centre)
        {
            Centre = centre;
            DirectOrbits = new Dictionary<string, Orbit>();
        }

        public void AddOrbit(Orbit orbiting)
        {
            if (!DirectOrbits.ContainsKey(orbiting.Centre)) 
                DirectOrbits.Add(orbiting.Centre, orbiting);
        }
    }
}