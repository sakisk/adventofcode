using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2019.DaySix
{
    public class OrbitGraph
    {
        public const string CentreOfMass = "COM";

        public IDictionary<string, SpaceObject> OrbitingObjects { get; } = new Dictionary<string, SpaceObject>
        {
            {
                CentreOfMass, new SpaceObject(CentreOfMass)
            }
        };

        public void AddOrbit(string orbit, bool undirected = false)
        {
            var names = orbit.Split(')');

            var centre = TryGetOrbit(names.First());
            var orbiting = TryGetOrbit(names.Last());

            centre.AddOrbit(orbiting);
            if (undirected)
                orbiting.AddOrbit(centre);

            SpaceObject TryGetOrbit(string name)
            {
                if (OrbitingObjects.TryGetValue(name, out var o))
                    return o;

                o = new SpaceObject(name);
                OrbitingObjects.Add(name, o);

                return o;
            }
        }
    }
}