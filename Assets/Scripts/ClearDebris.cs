using UnityEngine;
using System.Collections;

public class ClearDebris : MonoBehaviour {
    
	// Use this for initialization
	void Start () {
        Invoke("Clear", 25.0f);
        MinimizeLine();
	}
    void OnTriggerEnter(Collider Col)
    {
        GameObject g = Col.transform.parent.gameObject;
        EllipticalPathCS1 E = g.gameObject.GetComponent<EllipticalPathCS1>();
        E.Hitpoints = E.Hitpoints - 5;
        Debug.Log(Col.name + " slam!");
        Destroy(gameObject);
    }
    public void MinimizeLine()
    {
       TrailRenderer Line = gameObject.GetComponent<TrailRenderer>();
       Line.time -= 0.2f;
       Invoke("MinimizeLine", 1.0f);
    }
    public void Clear()
    {
        Destroy(gameObject);
    }
	// Update is called once per frame
	void Update () {
	
	}
}
