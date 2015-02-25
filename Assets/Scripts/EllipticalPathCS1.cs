using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class EllipticalPathCS1 : MonoBehaviour
{
    public GameObject Parent;
    public float radiusA = 5;
    public float radiusB = 10;
    public float eccentricity = 0.0f;
    public float speed = 1.0f;
    public float InclinationAngle = 10.0f;
    public bool ShowOrbit = false;
    public float angle;
    public bool Circ = false;
    public int Hitpoints = 20;
    public bool Alive = true;
    public GameObject Debris;
    // Use this for initialization
    void Start()
    {
        if (ShowOrbit)
        {
            createOrbitLine(ConvertScales(0.0f, 360.0f, angle, 0.0f, Mathf.PI * 2));
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
        DebrisInstance.transform.localRotation = Quaternion.Euler(0.0f, UnityEngine.Random.Range(0, 360), 90.0f);
    }
    // Update is called once per frame
    void Update()
    {
        if (Hitpoints <= 0 && Alive)
        {
            Debug.Log(gameObject.name + "has been destroyed, all life has been exterminated...etc...etc...//points should be taken away here or somthing.");
            Alive = false;
        }

        angle += speed * Time.deltaTime;
        transform.position = CalculateEllipse(ConvertScales(0.0f, 360.0f, angle, 0.0f, Mathf.PI * 2));

        float A;
        if (angle > 360.0f)
        {
            A = angle % 360.0f;
        }
        else
        {
            A = angle;
        }
        if (Circ && (A >= 0 && A <= 5 || A <= 360 && A >= 355))
        {
            StartCoroutine(CirculariseEllipse(angle, 1f));

        }
        

    }
    void LateUpdate()
    {
        if (ShowOrbit)
        {
            createOrbitLine(ConvertScales(0.0f, 360.0f, angle, 0.0f, Mathf.PI * 2));
        }
    }
    public void createOrbitLine(float PosAngle)
    {
        LineRenderer TM = (LineRenderer)transform.GetComponent(typeof(LineRenderer));
        TM.SetVertexCount(361);
        TM.SetColors(Color.red, Color.red);
        List<Vector3> Rs = new List<Vector3>();
        for (int i = 0; i <= 360; i++)
        {
            Vector3 CalcedEcc;
            float InclinationInRadians = ConvertScales(0.0f, 360.0f, InclinationAngle, 0.0f, Mathf.PI * 2);
            float Irads = ConvertScales(0.0f, 360.0f, i, 0.0f, Mathf.PI * 2);
            float x = 0 + radiusA * Mathf.Cos(Irads) * Mathf.Cos(InclinationInRadians) - radiusB * Mathf.Sin(Irads) * Mathf.Sin(InclinationInRadians);
            float y = 0 + radiusA * Mathf.Cos(Irads) * Mathf.Sin(InclinationInRadians) + radiusB * Mathf.Sin(Irads) * Mathf.Cos(InclinationInRadians);

            
            CalcedEcc = CalculateEccentricityOffset(eccentricity, InclinationAngle);

            TM.SetPosition(i, Parent.transform.position + new Vector3(x, 0, y) - CalcedEcc);
        }
    }
    //public Vector3 CalculatePosition(float angle)
    //{
    //    // Calculates position with parametric form, explanation:
    //    // http://en.wikipedia.org/wiki/Ellipse#Parametric_form_in_canonical_position
    //    float x = radiusA * Mathf.Cos(angle);
    //    float y = radiusB * Mathf.Sin(angle);

    //    return Parent.transform.position + new Vector3(x, 0, y) - new Vector3(eccentricity, 0, 0);
    //}
    public Vector3 CalculateEllipse(float PosAngle)
    {
        Vector3 CalcedEcc;
        float InclinationInRadians = ConvertScales(0.0f, 360.0f, InclinationAngle, 0.0f, Mathf.PI * 2);
        float x = 0 + radiusA * Mathf.Cos(PosAngle) * Mathf.Cos(InclinationInRadians) - radiusB * Mathf.Sin(PosAngle) * Mathf.Sin(InclinationInRadians);
        float y = 0 + radiusA * Mathf.Cos(PosAngle) * Mathf.Sin(InclinationInRadians) + radiusB * Mathf.Sin(PosAngle) * Mathf.Cos(InclinationInRadians);

        
        CalcedEcc = CalculateEccentricityOffset(eccentricity, InclinationAngle);


        return Parent.transform.position + new Vector3(x, 0, y) - CalcedEcc;
    }
    IEnumerator CirculariseEllipse(float PosAngle, float duration)
    {
        float NewRadius = radiusA - eccentricity;
        Debug.Log(NewRadius);
        float Rahold = radiusA;
        float Rbhold = radiusB;
        float Ecchold = eccentricity;
        float speedhold = speed;
        float elapsedTime = 0.0f;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / duration;

            radiusA = Mathf.Lerp(Rahold, NewRadius, t);
            radiusB = Mathf.Lerp(Rbhold, NewRadius, t);
            eccentricity = Mathf.Lerp(Ecchold, 0, t);
            speed = Mathf.Lerp(speedhold, speedhold / 2, t);
  

            yield return null;
        }
        eccentricity = 0;
        radiusA = NewRadius;
        speed = speedhold / 2;
        Circ = false;
    }
    //public void CirculariseEllipse(float PosAngle)
    //{
        
    //}
    public float ConvertScales(float scaleAstart, float scaleAend, float ScaleAValueGiven, float scaleBstart, float scaleBend)
    {
        float ScaleBvalueToFind = 0.0f;

        float Numerator = (ScaleAValueGiven - scaleAstart) * (scaleBstart - scaleBend);

        float denom = scaleAend - scaleAstart;

        ScaleBvalueToFind = Mathf.Abs(Numerator / denom);

        return ScaleBvalueToFind;
    }
    public Vector3 CalculateEccentricityOffset(float Hypot, float angle)
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

        return new Vector3(SideB * Multi, 0, SideA * Multi);
    }
    
}


        

        //if (A >= 0 && A <= 20 || A <= 360 && A >= 340)
        //{
        //    if (radiusA > radiusB)
        //    {
        //        radiusA -= 0.1f;
        //    }
        //    if (eccentricity > 0)
        //    {
        //        eccentricity -= 0.1f;
        //    }
        //}