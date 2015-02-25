using UnityEngine;
using System.Collections;

public class Planet {
    public string Name { get; set; }
    public float Size { get; set; }
    public float RotationSpeed { get; set; }

    public float SemiMajoraxis { get; set; }
    public float SemiMinoraxis { get; set; }

    public float OrbitalSpeed { get; set; }
    public float LinearEccentricity { get; set; }

    public float OrbitalInclination { get; set; }
    public float StartingPostion { get; set; }

    public float HardPoints { get; set; }

    public Color _color { get; set; }

    public Planet Create(float size, float semiMajoraxis, float semiMinoraxis, float orbitalSpeed, float linearEccentricity, float orbitalInclination, float rotationSpeed, Color color, float hardpoints, float startingPostion)
    {
        Size = size;
        SemiMajoraxis = semiMajoraxis;
        SemiMinoraxis = semiMinoraxis;
        OrbitalSpeed = orbitalSpeed;
        RotationSpeed = rotationSpeed;
        LinearEccentricity = linearEccentricity;
        OrbitalInclination = orbitalInclination;
        _color = color;
        HardPoints = hardpoints;
        StartingPostion = startingPostion;
        return this;
    }
}
