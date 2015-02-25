using UnityEngine;
using System.Collections;

public class SpawnAsteroid : MonoBehaviour {
    public int Number = 2;
    public int NumberSpawned = 1;
    public float timeSinceLastAsteroid = 0;
    public GameObject Ast;
	// Use this for initialization
	void Start () {
        //for (int i = 0; i < Number; i++)
        //{
            
        //}
        Invoke("Doo", 5.0f);
	}
    public void Doo()
    {
        if (NumberSpawned >= Number)
        {

        }
        else
        {
            for (int i = 0; i <= Random.Range(1.0f, 3.0f); i++ )
            {
                int Ra = Random.Range(50, 70);
                Spawn(
                    Ra: Ra,
                    Rb: Random.Range(20, 25),
                    speed: Random.Range(15, 80),
                    ecc: Random.Range(Ra - 20, Ra - 5),
                    angle: Random.Range(0, 360),
                    scale: 0.75f,
                    Name: "Asteroid_S" + NumberSpawned);
                NumberSpawned++;
            }
            
            Invoke("Doo", 10.0f);
        }
    }
	// Update is called once per frame
	void Update () {

	}
    public void Spawn(float Ra, float Rb, float speed, float ecc, float angle, float scale, string Name)
    {
        GameObject Asteroid = Instantiate(Ast, new Vector3(Random.Range(40, 50), Random.Range(40, 50), Random.Range(40, 50)), Quaternion.Euler(Ast.transform.forward)) as GameObject;
        Asteroid.GetComponent<EllipticalPathCS1>().Parent = GameObject.Find("Sol");
        Asteroid.name = Name;
        Asteroid.GetComponent<EllipticalPathCS1>().radiusA = Ra;
        Asteroid.GetComponent<EllipticalPathCS1>().radiusB = Rb;
        Asteroid.GetComponent<EllipticalPathCS1>().speed = speed;
        Asteroid.GetComponent<EllipticalPathCS1>().eccentricity = ecc;
        Asteroid.GetComponent<EllipticalPathCS1>().InclinationAngle = angle;
        Asteroid.GetComponent<EllipticalPathCS1>().ShowOrbit = false;
        Asteroid.GetComponent<EllipticalPathCS1>().angle = 180;
        Asteroid.transform.localScale = new Vector3(1, 1, 1);
        Asteroid.transform.localRotation = Quaternion.Euler(0.0f, Random.Range(0, 360), 90.0f);

        Asteroid.GetComponent<TrailRenderer>().time = 3;
    }
}
