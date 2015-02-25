using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {
    public float speed = 100.0f;
    public float LifeTime = 5.0f;
    private float timeAlive = 0;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void LateUpdate () {
        gameObject.rigidbody.AddForce(gameObject.transform.forward*speed*Time.deltaTime);
        if (timeAlive >= LifeTime)
        {
            Destroy(gameObject);
        }
        else
        {
            timeAlive += Time.deltaTime;
        }
	}
}
