using UnityEngine;
using System.Collections;

public class Moon  {
    public string Name { get; set; }
    public float Size { get; set; }
    
    public float RotationSpeed { get; set; }

    public float SemiMajoraxis { get; set; }
    public float SemiMinoraxis { get; set; }

    public float OrbitalSpeed { get; set; }
    public float LinearEccentricity { get; set; }

    public float OrbitalInclination { get; set; }

    public Moon Create(string name, float size, float semiMajoraxis, float semiMinoraxis, float orbitalSpeed, float linearEccentricity, float orbitalInclination, float rotationSpeed)
    {
        Name = name;
        Size = size;
        SemiMajoraxis = semiMajoraxis;
        SemiMinoraxis = semiMinoraxis;
        OrbitalSpeed = orbitalSpeed;
        LinearEccentricity = linearEccentricity;
        RotationSpeed = rotationSpeed;
        OrbitalInclination = orbitalInclination;
        return this;
    }
}
