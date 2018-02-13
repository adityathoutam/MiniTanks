using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour {


    public GameObject bullet;
    public Transform Gun;
    public Transform[] endpoint;

    GameObject go;
    GameObject go1;
    GameObject go2;

   public  void Start () {

       
        go = Instantiate(bullet, Gun.position, Quaternion.identity);
        go1 = Instantiate(bullet, Gun.position, Quaternion.identity);
        go2 = Instantiate(bullet, Gun.position, Quaternion.identity);



    }


    public void Update()
    {

         MoveExplo(go, endpoint[0]);
        MoveExplo(go1, endpoint[1]);
        MoveExplo(go2, endpoint[2]);


    }

   

    void MoveExplo(GameObject GoBullet, Transform TargetPos)
    {
        float step = 250 * Time.deltaTime;
        GoBullet.transform.position = Vector3.MoveTowards(GoBullet.transform.position, TargetPos.position, step);

        if (GoBullet.transform.position == TargetPos.position)
            GoBullet.transform.position = Gun.position;

       
       
    }
}
