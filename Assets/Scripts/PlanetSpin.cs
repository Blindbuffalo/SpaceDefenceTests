using UnityEngine;
using System.Collections;

public class PlanetSpin : MonoBehaviour {
    public float spinSpeed = 1.0f;
	// Use this for initialization
	void Start () {
	
	}
    void OnParticleCollision(GameObject collision)
    {
        Debug.Log("bump planet!");
    }
	// Update is called once per frame
	void Update () {
        float t = spinSpeed * Time.deltaTime;
        gameObject.transform.Rotate(Vector3.up, -t);
        //Debug.Log(t);
	}
}
