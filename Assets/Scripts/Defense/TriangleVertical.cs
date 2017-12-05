using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriangleVertical : MonoBehaviour
{
    public GameObject Enemy;
    private int speed = 15;
    GameObject go;
    public Transform firspoint;
    public Transform secondpoint;
    Vector3 spawnPoint, uppoint, downpoint,target;
    private float t = 0;

    void Start()
    {
         VerticalTriangleStart();
    }

   

    void VerticalTriangleStart()
    {
        go = Instantiate(Enemy,firspoint.position,Quaternion.identity);
        spawnPoint = go.transform.position;
        uppoint = firspoint.position;
        downpoint =secondpoint.position;
        target = downpoint;


    }
   
    void Update()
    {
       
        Vector3 directionVector = (downpoint - go.transform.position);

        float angle = Mathf.Atan2(directionVector.x, directionVector.y) * Mathf.Rad2Deg;

        Quaternion rotation = Quaternion.AngleAxis(-angle, Vector3.forward);

        t += 0.01f;
        if (t > 0.5f)
            t = 0;


        go.transform.rotation = Quaternion.Lerp(go.transform.rotation, rotation, t);

        go.transform.Translate(Vector3.up * speed * Time.deltaTime, Space.Self);

       
        if (Vector3.Distance(go.transform.position, downpoint) < 0.2f)
        {
            downpoint = uppoint;
        }

        if (Vector3.Distance(go.transform.position, uppoint) < 0.2f)
        {
            downpoint = target;
        }    

    }
}

