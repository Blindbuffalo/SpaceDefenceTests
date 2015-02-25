using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StarSystem  {
    public string Name = "Solarsystem";
    public Star star = new Star();
    public List<PlanetSystem> PlanetSystems = new List<PlanetSystem>();

    public StarSystem Create(Star s, List<PlanetSystem> planetSystems)
    {
        StarSystem SS = new StarSystem();
        SS.star = s;
        SS.PlanetSystems = planetSystems;
        return SS;
    }
}
