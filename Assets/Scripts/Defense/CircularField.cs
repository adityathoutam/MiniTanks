using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircularField : MonoBehaviour
{

    public GameObject Enemy;

    GameObject go;
    
    bool firstturn, secondturn, thirdturn = false;

    float ab=15, ca=20;
    //float bc = 25;
    
    Vector3 firstpoint;
    Vector3 secondpoint;
    Vector3 thirdpoint;
    Vector3 secpoint;


    void Start()
    { 
        go = Instantiate(Enemy);
        
        firstturn = true;

        firstpoint = go.transform.position;

        secondpoint = firstpoint;
        secondpoint.x = firstpoint.x + ab;
        secpoint = secondpoint;

        thirdpoint.x = firstpoint.x;
        thirdpoint.y = firstpoint.y - ca; 
    }

    


    void Update()
    {

        Vector3 directionVector = (secondpoint - go.transform.position);
        float angle = Mathf.Atan2(directionVector.x, directionVector.y) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(-angle, Vector3.forward);
        go.transform.rotation = Quaternion.Lerp(go.transform.rotation, rotation, 10 * Time.deltaTime);
        go.transform.Translate(Vector3.up * 10 * Time.deltaTime, Space.Self);


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

