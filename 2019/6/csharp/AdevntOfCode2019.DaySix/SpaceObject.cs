using System.Collections.Generic;

namespace AdventOfCode2019.DaySix
{
    public class SpaceObject
    {
        public string Name { get; }

        public Dictionary<string, SpaceObject> DirectOrbits { get; } = new Dictionary<string, SpaceObject>();

        public SpaceObject(string name) => Name = name;

        public void AddOrbit(SpaceObject spaceObject)
        {
            if (!DirectOrbits.ContainsKey(spaceObject.Name)) 
                DirectOrbits.Add(spaceObject.Name, spaceObject);
        }
    }
}