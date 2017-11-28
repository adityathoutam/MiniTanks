using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private GameObject Cam;
    static Vector3 offset;
    private GameObject Bullet;
    
 

    void Start()
    {
        Cam = GameObject.Find("Main Camera");
        Bullet = GameObject.Find("CubePrefab(Clone)");
        offset = Cam.transform.position - Bullet.transform.position;

    }


    void LateUpdate()
    {
        if (Bullet == null)
        {
            Cam.transform.position = new Vector3(4f, 4f, 0f);
        }
        else
            Cam.transform.position = Bullet.transform.position + offset;
       
    }
}
