using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircularField : MonoBehaviour
{

    public GameObject Enemy;
    public int speed = 5;
    GameObject go;
    
    bool firstturn, secondturn, thirdturn = false;

    float ab=15, ca=20;
    //float bc = 25;
    public Transform firstTriPoint;
    public Transform SecondTriPoint;
    public Transform ThirdTriPoint;

    Vector3 firstpoint;
    Vector3 secondpoint;
    Vector3 thirdpoint;
    Vector3 secpoint;


    void Start()
    { 
     
        go = Instantiate(Enemy, firstTriPoint.position, Quaternion.identity);

        firstturn = true;

        firstpoint = go.transform.position;

        secondpoint = SecondTriPoint.position;
       
        secpoint = SecondTriPoint.position;

        thirdpoint = ThirdTriPoint.position;
        
    }

    


    void Update()
    {

        Vector3 directionVector = (secondpoint - go.transform.position);
        float angle = Mathf.Atan2(directionVector.x, directionVector.y) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(-angle, Vector3.forward);
        go.transform.rotation = Quaternion.Lerp(go.transform.rotation, rotation, 10 * Time.deltaTime);
        go.transform.Translate(Vector3.up * speed * Time.deltaTime, Space.Self);


        if (Vector3.Distance(go.transform.position, secondpoint) < 0.2f)
        {
            int random = Random.Range(0, 2);
            if (random == 0)
            {
                secondpoint = thirdpoint;
            }
            if (random == 1)
            {
                secondpoint = firstpoint;
            }
        }

        if (Vector3.Distance(go.transform.position, thirdpoint) < 0.2f)
        {
            int random = Random.Range(0, 2);
            if (random == 0)
            {
                secondpoint = firstpoint;  
            }
            if (random == 1)
            {
                secondpoint = secpoint;

            }
        }

        if (Vector3.Distance(go.transform.position, firstpoint) < 0.2f)
        {
            int random = Random.Range(0, 2);
            if (random == 0)
            {
                secondpoint = secpoint;
 
            }
            if (random == 1)
            {
               secondpoint = thirdpoint;
            }
        }

    }
    
}

