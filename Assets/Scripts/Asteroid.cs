using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Asteroid : MonoBehaviour {
    public float Life = 100.0f;
    public GameObject Debris;
	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        if (Life <= 0)
        {
            Debug.Log(gameObject.name + ": LAZZZZZOOOOR BOOOOOM!");

            GenDebris();

            Destroy(gameObject);
        }
	}
    public void GenDebris()
    {
        EllipticalPathCS1 GOEs = gameObject.GetComponent<EllipticalPathCS1>();

        GameObject DebrisInstance = Instantiate(Debris, gameObject.transform.position, gameObject.transform.rotation) as GameObject;
        DebrisInstance.GetComponent<EllipticalPathCS1>().Parent = GameObject.Find("Sol");
        DebrisInstance.name = gameObject.name + "_debris";
        DebrisInstance.GetComponent<EllipticalPathCS1>().radiusA = GOEs.radiusA;
        DebrisInstance.GetComponent<EllipticalPathCS1>().radiusB = GOEs.radiusB;
        DebrisInstance.GetComponent<EllipticalPathCS1>().speed = GOEs.speed - 10;
        DebrisInstance.GetComponent<EllipticalPathCS1>().eccentricity = GOEs.eccentricity;
        DebrisInstance.GetComponent<EllipticalPathCS1>().InclinationAngle = GOEs.InclinationAngle;
        DebrisInstance.GetComponent<EllipticalPathCS1>().ShowOrbit = false;
        DebrisInstance.GetComponent<EllipticalPathCS1>().angle = GOEs.angle;
        DebrisInstance.transform.localScale = new Vector3(1, 1, 1);
        DebrisInstance.transform.localRotation = Quaternion.Euler(0.0f, Random.Range(0, 360), 90.0f);
    }
    void OnTriggerEnter(Collider Col)
    {
        GameObject Sol = GameObject.Find("Sol");
        
        switch (Col.transform.tag)
        {
            case "Bads":
                //if (gameObject.name.Contains("Asteroid_Col"))
                //{
                    
                //    Debug.Log(Col.name + ": splat");
                //}
                //else
                //{
                GenDebris();
                    Debug.Log(Col.name + ": booooom");
                //}
                Destroy(gameObject);
                break;
            case "Moon":
                GenDebris();
                GameObject gm = Col.transform.gameObject;
                EllipticalPathCS1 Em = gm.GetComponent<EllipticalPathCS1>();
                Em.Hitpoints = Em.Hitpoints - 10;
                Debug.Log(Col.name);
                Destroy(gameObject);
                break;
            case "LaserPart":
                Debug.Log(Col.transform.parent.parent.name + ": " + Col.name + ": laser being removed");
                Destroy(Col.transform.parent.gameObject);
                Destroy(gameObject);
                break;
            case "Planet":
                List<Transform> Childs = new List<Transform>();
                foreach (Transform child in Col.transform)
                {
                    Childs.Add(child);
                }
                if (Childs.Count == 0)
                {

                    GameObject g = Col.transform.parent.gameObject;
                    EllipticalPathCS1 E = g.gameObject.GetComponent<EllipticalPathCS1>();
                    E.Hitpoints = E.Hitpoints - 10;
                    Debug.Log(Col.name + ": possible place for sheilds etc.");
                    Destroy(gameObject);
                }
                else
                {
                   
                    Debug.Log(Col.name + ": laser being removed");
                    Destroy(Childs[Random.Range(0, Childs.Count - 1)].gameObject);
                    Destroy(gameObject);
                }
                
                break;
            case "Sun":
                Destroy(gameObject);
                break;
            default:

                break;
        }
    }
}
