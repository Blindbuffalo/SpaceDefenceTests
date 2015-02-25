using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class Laser : MonoBehaviour {
    public GameObject Bullet;
    public float TimeSinceFire = 0;
    public float FireIntervalMin = 2;
    public float FireIntervalMax = 4;
    public float FireDistence = 4.0f;
    private float FireInterval;
    public float laserDuration = 0.5f;
    public float Power = 10.0f;
    private GameObject LastHit;

	// Use this for initialization
	void Start () {
        FireInterval = UnityEngine.Random.Range(FireIntervalMin, FireIntervalMax);
	}
	
	// Update is called once per frame
	void Update () {
        ChargerColor();
        ShootatStuff();
	}
    public void ChargerColor()
    {
        GameObject Charger = transform.FindChild("Charger").gameObject;
       // Debug.Log(TimeSinceFire + "   " + FireInterval + "   " + FireInterval * 0.8f);
        if (TimeSinceFire >= FireInterval * 0.9f)
        {
            Charger.renderer.material.color = Color.green;
        }
        else
        {
            if (TimeSinceFire >= FireInterval * 0.4f)
            {
                Charger.renderer.material.color = Color.red;
            }
            else
            {

                if (TimeSinceFire < FireInterval)
                {
                    Charger.renderer.material.color = Color.blue;
                }
                
            }
        }
        

    }
    public void ShootatStuff()
    {
        GameObject Emitter = transform.FindChild("Emitter").gameObject;
        LineRenderer TM = (LineRenderer)Emitter.GetComponent(typeof(LineRenderer));
        List<GameObject> EnemiesCanHit = new List<GameObject>();
        if (LastHit != null)
        {
            TM.SetVertexCount(2);
            TM.SetPosition(0, Emitter.transform.position);
            TM.SetPosition(1, LastHit.transform.position);
        }
        if (TimeSinceFire >= laserDuration)
        {
            TM.SetVertexCount(0);
        }
        if (TimeSinceFire >= FireInterval)
        {

            //GameObject B = Instantiate(Bullet, transform.position, transform.rotation) as GameObject;
            //B.rigidbody.AddForce(gameObject.transform.forward * 1000);

            GameObject[] Enemies = GameObject.FindGameObjectsWithTag("Bads");

            int Eint = UnityEngine.Random.Range(0, Enemies.Length);
            float smallestDistence = float.MaxValue;
            GameObject FireAt = null;
            float D = 0.0f;
            foreach (GameObject E in Enemies)
            {
                RaycastHit hit;
                D = Vector3.Distance(transform.position, E.transform.position);
                if (Physics.Linecast(Emitter.transform.position, E.transform.position, out hit, 1 << 12 | 1 << 9 | 1 << 10 | 1 << 11) && D < smallestDistence && D < FireDistence)
                {
                    //Debug.Log(hit.transform.name);
                    if (hit.transform.name == E.transform.name)
                    {
                        FireAt = E;
                        smallestDistence = D;
                    }
                    else
                    {
                        //LastHit = null;
                    }
                }
            }
            //Debug.Log(FireAt);
            if (FireAt != null)
            {
                TimeSinceFire = 0;
                FireInterval = UnityEngine.Random.Range(FireIntervalMin, FireIntervalMax);
                TM.SetVertexCount(2);
                TM.SetPosition(0, Emitter.transform.position);
                TM.SetPosition(1, FireAt.transform.position);
                LastHit = FireAt.transform.gameObject;
                //Debug.Log(D.ToString() + " " + ((Power - D) * Power).ToString());
                FireAt.GetComponent<Asteroid>().Life -= Power;
            }
            else
            {
                TimeSinceFire += Time.deltaTime;
                LastHit = null;
            }




        }
        else
        {
            TimeSinceFire += Time.deltaTime;
        }
    }
}
