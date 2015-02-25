using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlanetSystem  {
    public string Name = "Solarsystem";
    public Planet planet = new Planet();
    public List<Moon> Moons = new List<Moon>();

    public PlanetSystem Create(string PlanetName, Planet p, List<Moon> moons)
    {
        PlanetSystem PS = new PlanetSystem();
        PS.Name = PlanetName + " System";
        p.Name = PlanetName;
        PS.planet = p;
        PS.Moons = moons;
        return PS;
    }
}
