using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class CreateSolarSystem : MonoBehaviour {
    public GameObject GenericPlanetSystem;
    public GameObject MoonObj;
    public GameObject Gun;
    public GameObject Gun2;

    public Material EarthMat;
    public Material ColorMat;
	// Use this for initialization
	void Start () {
        PlanetSystem PS = new PlanetSystem().Create(
            "Earth",
            new Planet().Create(3.0f, 14.0f, 14.0f, 20.0f, 0.0f, 45.0f, 100.0f, Color.clear, 3, 90),
            new List<Moon> { 
                new Moon().Create("Moon", 0.55f, 4.0f, 4.0f, 60.0f, 0.0f, 0.0f, 10.0f)
            }
        );
        CreatePlanetSystem(PS);
        PlanetSystem PS1 = new PlanetSystem().Create(
            "Mars",
            new Planet().Create(4.5f, 25.0f, 23.0f, 10.0f, 0.0f, 0.0f, 80.0f, Color.red, 4, 0),
            new List<Moon> { 
                new Moon().Create("Dres", 0.45f, 6.0f, 6.0f, 100.0f, 0.0f, 0.0f, 10.0f)
            }
        );
        CreatePlanetSystem(PS1);
        PlanetSystem PS2 = new PlanetSystem().Create(
            "Mercury",
            new Planet().Create(2.0f, 7.0f, 7.0f, 10.0f, 0.0f, 0.0f, 80.0f, Color.blue, 1, 120),
            new List<Moon>
            {

            }
        );
        CreatePlanetSystem(PS2);
        PlanetSystem PS3 = new PlanetSystem().Create(
            "Gassssssss",
            new Planet().Create(5.0f, 35.0f, 35.0f, 10.0f, 0.0f, 0.0f, 80.0f, Color.blue, 6, 120),
            new List<Moon>
            {

            }
        );
        CreatePlanetSystem(PS3);
        PlanetSystem PS4 = new PlanetSystem().Create(
            "Jupitor",
            new Planet().Create(7.0f, 70.0f, 55.0f, 15.0f, 5.0f, 47.0f, 80.0f, Color.blue, 7, 120),
            new List<Moon>
            {
                new Moon().Create("Jup1", 0.4f, 10.0f, 9.0f, 120.0f, 0.0f, 0.0f, 10.0f),
                new Moon().Create("Jup2", 0.6f, 6.5f, 6.0f, 100.0f, 0.0f, 0.0f, 10.0f),
                new Moon().Create("Jup3",0.7f, 7.0f, 7.0f, 60.0f, 0.0f, 0.0f, 10.0f)
            }
        );
        CreatePlanetSystem(PS4);
        //set the cameras first view to the extents of the system.  eventually set this up to be dynamic: 
        //largest semimajoraxis + largets moons semimajoraxis + 4 - which ever system has the largest
        GameObject Cam = GameObject.Find("Main Camera");
        Cam.camera.orthographicSize = 45;
	}

    public void CreatePlanetSystem(PlanetSystem PS)
    {
        GameObject System = Instantiate(GenericPlanetSystem, new Vector3(PS.planet.SemiMajoraxis, 0, 0), Quaternion.identity) as GameObject;
        

        System.name = PS.Name;
        GameObject P = System.transform.FindChild("Planet").gameObject;
        P.name = PS.planet.Name;
        P.GetComponent<EllipticalPathCS1>().Parent = gameObject;
        P.GetComponent<EllipticalPathCS1>().radiusA = PS.planet.SemiMajoraxis;
        P.GetComponent<EllipticalPathCS1>().radiusB = PS.planet.SemiMinoraxis;
        P.GetComponent<EllipticalPathCS1>().speed = PS.planet.OrbitalSpeed;
        P.GetComponent<EllipticalPathCS1>().eccentricity = PS.planet.LinearEccentricity;
        P.GetComponent<EllipticalPathCS1>().InclinationAngle = PS.planet.OrbitalInclination;
        P.GetComponent<EllipticalPathCS1>().angle = PS.planet.StartingPostion;
        P.transform.localScale = new Vector3(PS.planet.Size, PS.planet.Size, PS.planet.Size);

        GameObject PlanetGeometry = P.transform.FindChild("PlanetGeo").gameObject;
        PlanetGeometry.name = PS.planet.Name + "Geo";
        PlanetGeometry.GetComponent<PlanetSpin>().spinSpeed = PS.planet.RotationSpeed;

        AddHardPoints(PS, PlanetGeometry);

        GameObject PlanetCamera = P.transform.FindChild("PlanetCamera").gameObject;
        PlanetCamera.camera.name = PS.planet.Name + "Camera";
        PlanetCamera.camera.enabled = false;

        GameObject PlanetText = P.transform.FindChild("PlanetText").gameObject;
        PlanetText.name = PS.planet.Name + "Text";
        TextMesh TM = (TextMesh)PlanetText.GetComponent(typeof(TextMesh));
        TM.text = PS.planet.Name;
        PlanetText.SetActive(false);

        if (PS.planet._color == Color.clear) 
        {
            PlanetGeometry.renderer.material = EarthMat;
        }
        else
        {
            PlanetGeometry.renderer.material = ColorMat;
            PlanetGeometry.renderer.material.color = PS.planet._color;
        }
        

        foreach (Moon M in PS.Moons)
        {
            GameObject Moona = Instantiate(MoonObj, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
            Moona.transform.parent = System.transform;
            Moona.name = M.Name;
            Moona.transform.localScale = new Vector3(M.Size, M.Size, M.Size);
            Moona.GetComponent<EllipticalPathCS1>().Parent = P;
            Moona.GetComponent<EllipticalPathCS1>().radiusA = M.SemiMajoraxis;
            Moona.GetComponent<EllipticalPathCS1>().radiusB = M.SemiMinoraxis;
            Moona.GetComponent<EllipticalPathCS1>().eccentricity = M.LinearEccentricity;
            Moona.GetComponent<EllipticalPathCS1>().speed = M.OrbitalSpeed;
            Moona.GetComponent<EllipticalPathCS1>().InclinationAngle = M.OrbitalInclination;
        }

    }
    public void AddHardPoints(PlanetSystem PS, GameObject PlanetGeometry)
    {
        
        float AngularInc = 360 / PS.planet.HardPoints;
        for (int i = 0; i < PS.planet.HardPoints; i++)
        {
            GameObject Blasta = null;
            if (PS.planet.Name == "Jupitor")
            {
                Blasta = Instantiate(Gun2, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
            }
            else
            {
                Blasta = Instantiate(Gun, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
            }
            
            //GameObject cube2 = GameObject.CreatePrimitive(PrimitiveType.Cube);
            Blasta.transform.parent = PlanetGeometry.transform;
            Blasta.transform.localPosition = CalculatePathag(0.5f, i * AngularInc);
            //cube2.transform.Rotate(0.0f, i * AngularInc, 0.0f);
            Blasta.transform.localRotation = Quaternion.Euler(0.0f, i * AngularInc, 0.0f);
            Blasta.transform.localScale = new Vector3(0.33f, 0.33f, 0.33f);
            Blasta.transform.name = "Hardpoint_" + i;
        }
        
    }
    public Vector3 CalculatePathag(float Hypot, float angle)
    {
        int Multi = 1;
        if (angle > 180)
        {
            angle -= 180;

            Multi = -1;
        }

        double CosAngD = Math.Cos((angle * Math.PI / 180));
        float CosAng = (float)CosAngD;
        float SideB = CosAng * Hypot;
        float SideA = Mathf.Sqrt((Hypot * Hypot) - (SideB * SideB));
        // Debug.Log("IncAngle: " + angle + "; Ecc: " + eccentricity + "; CosAngD: " + CosAngD + " CosAng: " + CosAng + "; sides: " + SideA + "  " + SideB + " --- " + SideB + Multi);

        return new Vector3(SideA * Multi, 0, SideB * Multi);
    }
}
        //EarthSystem = Instantiate(GenericPlanetSystem, new Vector3(20.0f, 0, 0), Quaternion.identity) as GameObject;
        //EarthSystem3 = Instantiate(GenericPlanetSystem, new Vector3(10.0f, 0, 0), Quaternion.identity) as GameObject;


        //EarthSystem.name = "EarthSystem";
        //GameObject Planet = EarthSystem.transform.FindChild("Planet").gameObject;
        //Planet.GetComponent<EllipticalPathCS1>().Parent = gameObject;
        //Planet.GetComponent<EllipticalPathCS1>().radiusA = 20.0f;
        //Planet.GetComponent<EllipticalPathCS1>().radiusB = 20.0f;
        //Planet.GetComponent<EllipticalPathCS1>().speed = 30.0f;
        //Planet.transform.localScale = new Vector3(1, 1, 1);
        //GameObject Pgeo2 = Planet.transform.FindChild("PlanetGeo").gameObject;
        //Pgeo2.renderer.material = EarthMat;
        

        //Moon = Instantiate(MoonObj, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;

        //Moon.transform.parent = EarthSystem.transform;
        //Moon.transform.localScale = new Vector3(0.25f, 0.25f, 0.25f);
        //Moon.GetComponent<EllipticalPathCS1>().Parent = Planet;
        //Moon.GetComponent<EllipticalPathCS1>().radiusA = 1.25f;
        //Moon.GetComponent<EllipticalPathCS1>().radiusB = 1.25f;
        //Moon.GetComponent<EllipticalPathCS1>().speed = 500;


        //GameObject Planet3 = EarthSystem3.transform.FindChild("Planet").gameObject;
        //Planet3.GetComponent<EllipticalPathCS1>().Parent = gameObject;
        //Planet3.GetComponent<EllipticalPathCS1>().radiusA = 10.0f;
        //Planet3.GetComponent<EllipticalPathCS1>().radiusB = 10.0f;
        //Planet3.GetComponent<EllipticalPathCS1>().speed = 50.0f;
        //Planet3.transform.localScale = new Vector3(0.53f, 0.53f, 0.53f);
        //GameObject Pgeo3 = Planet3.transform.FindChild("PlanetGeo").gameObject;
        //Pgeo3.renderer.material.color = Color.red;


        //EarthSystem2 = Instantiate(GenericPlanetSystem, new Vector3(5.0f, 0, 0), Quaternion.identity) as GameObject;
        //GameObject Planet2 = EarthSystem2.transform.FindChild("Planet").gameObject;
        //Planet2.GetComponent<EllipticalPathCS1>().Parent = gameObject;
        //Planet2.GetComponent<EllipticalPathCS1>().radiusA = 5.0f;
        //Planet2.GetComponent<EllipticalPathCS1>().radiusB = 5.0f;
        //Planet2.GetComponent<EllipticalPathCS1>().speed = 60.0f;
        //Planet2.transform.localScale = new Vector3(0.38f, 0.38f, 0.38f);
        //GameObject Pgeo = Planet2.transform.FindChild("PlanetGeo").gameObject;
        //Pgeo.renderer.material.color = Color.gray;
