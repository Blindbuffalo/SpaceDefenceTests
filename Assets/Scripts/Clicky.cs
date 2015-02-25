using UnityEngine;
using System.Collections;

public class Clicky : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
    void OnMouseDown () {


        //Debug.Log (transform.name);

        
        GameObject MainCam = GameObject.Find("Main Camera");
        GameObject PlanetCamera = transform.FindChild(transform.name + "Camera").gameObject;
        GameObject PlanetText = transform.FindChild(transform.name + "Text").gameObject;
        TextMesh TM = (TextMesh)PlanetText.GetComponent(typeof(TextMesh));
        TM.text = transform.name;
        //Debug.Log(Cam.transform.parent);
        if (MainCam.camera.enabled == true)
        {
            PlanetCamera.camera.enabled = true;
            MainCam.camera.enabled = false;
            //PlanetText.SetActive(false);
        }
        else
        {
            MainCam.camera.enabled = true;
            PlanetCamera.camera.enabled = false;
            //PlanetText.SetActive(true);
        }
        

        //if (MainCam.transform.parent == transform)
        //{
        //    MainCam.transform.parent = null;
        //    //Cam.camera.orthographic = true; 
        //    MainCam.camera.orthographicSize = 25.0f + (6.0f / 2.0f) + 4.0f;
        //    MainCam.camera.transform.position = new Vector3(0, 70, 0);
        //}
        //else
        //{
        //    MainCam.transform.parent = transform;
        //    //Cam.camera.orthographic = false; 
        //    MainCam.camera.orthographicSize = transform.localScale.x + 1.0f;
        //    MainCam.camera.transform.position = new Vector3(0, 70, 0) + transform.position;
            
        //}

        
    }


}
