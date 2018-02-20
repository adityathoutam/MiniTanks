﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AIScript : MonoBehaviour
{
    public GameObject TriPrefab;
    public GameObject CirclePrefab;
    public GameObject player;
    

   

    float speed = 1f;

    
    public static GameObject GreenToBrown;
    public static GameObject BrownToPink;
    public static GameObject PinkToBlue;
    public static GameObject BlueToOrange;
    public static GameObject OrangeToBlue;

    public static GameObject GreenTriangle1;
    public static GameObject BrownTriangle1;
    public static GameObject BlueTriangle1;
    public static GameObject OrangeTriangle1;
    public static GameObject PinkTriangle1;

    public static GameObject GreenCircle1;
    public static GameObject BrownCircle1;
    public static GameObject BlueCircle1;
    public static GameObject OrangeCircle1;
    public static GameObject PinkCircle1;

    public static GameObject GreenTriangle2;
    public static GameObject BrownTriangle2;
    public static GameObject BlueTriangle2;
    public static GameObject OrangeTriangle2;
    public static GameObject PinkTriangle2;

    public static GameObject GreenCircle2;
    public static GameObject BrownCircle2;
    public static GameObject BlueCircle2;
    public static GameObject OrangeCircle2;
    public static GameObject PinkCircle2;

    public Transform Map;
    public Transform GreenMap;
    public Transform BrownMap;
    public Transform BlueMap;
    public Transform OrangeMap;
    public Transform PinkMap;

    public GameObject cam;

    private void Start()
    {
       GreenTanksCreate();
        BrownTanksCreate();
        OrangeTanksCreate();
        BlueTanksCreate();
        PinkTanksCreate();
        SetActiveFalse();

        GreenTriangle1.SetActive(true);
        GreenCircle1.SetActive(true);
    }
    private void Update()
    {
      
        
       
      
        GreenTanksMove();
        BrownTanksMove();
        OrangeTanksMove();
        BlueTanksMove();
        PinkTanksMove();
        BoardEntry();
    }
  
    #region TanksCreateAndMove
    void GreenTanksCreate()
    {
        if (GreenTriangle1 == null)
        {
            GreenTriangle1 = Instantiate(TriPrefab);
            GreenTriangle1.transform.position = GreenMapPoints._greenMapTriangle1[0].transform.position;
        }

        //if (GreenTriangle2 == null)
        //{
        //    GreenTriangle2 = Instantiate(TriPrefab);
        //    GreenTriangle2.transform.position = GreenMapPoints._greenMapTriangle1[0].transform.position;
        //}

        //if (GreenCircle2 == null)
        //{
        //    GreenCircle2 = Instantiate(CirclePrefab);
        //    GreenCircle2.transform.position = GreenMapPoints._greenMapCircle1[0].transform.position;
        //}

        if (GreenCircle1 == null)
        {
            GreenCircle1 = Instantiate(CirclePrefab);
            GreenCircle1.transform.position = GreenMapPoints._greenMapCircle1[0].transform.position;
        }

    }
         void GreenTanksMove()
    {
        
            GreenMapPoints.TriangleMove(speed, GreenTriangle1, GreenMapPoints._greenMapTriangle1[0], GreenMapPoints._greenMapTriangle1[1]);
            GreenMapPoints.CircleMove(speed, GreenCircle1, GreenMapPoints._greenMapCircle1[0], GreenMapPoints._greenMapCircle1[1], GreenMapPoints._greenMapCircle1[2]);
        
       
        
        //GreenMapPoints.TriangleMove(speed, GreenTriangle2, GreenMapPoints._greenMapTriangle1[0], GreenMapPoints._greenMapTriangle1[1]);
        //GreenMapPoints.CircleMove(speed, GreenCircle2, GreenMapPoints._greenMapCircle1[0], GreenMapPoints._greenMapCircle1[1], GreenMapPoints._greenMapCircle1[2]);
    }


    void BrownTanksCreate()
    {
        if (BrownTriangle1 == null)
        {
            BrownTriangle1 = Instantiate(TriPrefab);
            BrownTriangle1.transform.position = BrownMapPoints._brownMapTriangle1[0].transform.position;
        }
        if (BrownCircle1 == null)
        {
            BrownCircle1 = Instantiate(CirclePrefab);
            BrownCircle1.transform.position = BrownMapPoints._brownMapCircle1[0].transform.position;
        }
    }
    void BrownTanksMove()
    {
        
        BrownMapPoints.TriangleMove(speed, BrownTriangle1, BrownMapPoints._brownMapTriangle1[0], BrownMapPoints._brownMapTriangle1[1]);
        BrownMapPoints.CircleMove(speed, BrownCircle1, BrownMapPoints._brownMapCircle1[0], BrownMapPoints._brownMapCircle1[1], BrownMapPoints._brownMapCircle1[2]);

    }
    void OrangeTanksCreate()
    {
        if (OrangeTriangle1 == null)
        {
            OrangeTriangle1 = Instantiate(TriPrefab);
            OrangeTriangle1.transform.position = OrangeMapPoints._orangeMapTriangle1[0].transform.position;
        }
        if (OrangeCircle1 == null)
        {
            OrangeCircle1 = Instantiate(CirclePrefab);
            OrangeCircle1.transform.position = OrangeMapPoints._orangeMapCircle1[0].transform.position;
        }
    }
    void OrangeTanksMove()
    {
        OrangeMapPoints.TriangleMove(speed, OrangeTriangle1, OrangeMapPoints._orangeMapTriangle1[0], OrangeMapPoints._orangeMapTriangle1[1]);
        OrangeMapPoints.CircleMove(speed, OrangeCircle1, OrangeMapPoints._orangeMapCircle1[0], OrangeMapPoints._orangeMapCircle1[1], OrangeMapPoints._orangeMapCircle1[2]);
    }
    void BlueTanksCreate()
    {
        if (BlueTriangle1 == null)
        {
            BlueTriangle1 = Instantiate(TriPrefab);
            BlueTriangle1.transform.position = BlueMapPoints._blueMapTriangle1[0].transform.position;
        }
        BlueMapPoints.TriangleMove(speed, BlueTriangle1, BlueMapPoints._blueMapTriangle1[0], BlueMapPoints._blueMapTriangle1[1]);

        if (BlueCircle1 == null)
        {
            BlueCircle1 = Instantiate(CirclePrefab);
            BlueCircle1.transform.position = BlueMapPoints._blueMapCircle1[0].transform.position;
        }

        BlueMapPoints.CircleMove(speed, BlueCircle1, BlueMapPoints._blueMapCircle1[0], BlueMapPoints._blueMapCircle1[1], BlueMapPoints._blueMapCircle1[2]);
    }
    void BlueTanksMove()
    {
        BlueMapPoints.TriangleMove(speed, BlueTriangle1, BlueMapPoints._blueMapTriangle1[0], BlueMapPoints._blueMapTriangle1[1]);
        BlueMapPoints.CircleMove(speed, BlueCircle1, BlueMapPoints._blueMapCircle1[0], BlueMapPoints._blueMapCircle1[1], BlueMapPoints._blueMapCircle1[2]);
    }
    void PinkTanksCreate()
    {
        if (PinkTriangle1 == null)
        {
            PinkTriangle1 = Instantiate(TriPrefab);
            PinkTriangle1.transform.position = PinkMapPoints._pinkMapTriangle1[0].transform.position;
        }
        PinkMapPoints.TriangleMove(speed, PinkTriangle1, PinkMapPoints._pinkMapTriangle1[0], PinkMapPoints._pinkMapTriangle1[1]);

        if (PinkCircle1 == null)
        {
            PinkCircle1 = Instantiate(CirclePrefab);
            PinkCircle1.transform.position = PinkMapPoints._pinkMapCircle1[0].transform.position;
        }

        PinkMapPoints.CircleMove(speed, PinkCircle1, PinkMapPoints._pinkMapCircle1[0], PinkMapPoints._pinkMapCircle1[1], PinkMapPoints._pinkMapCircle1[2]);
    }
    void PinkTanksMove()
    {
        PinkMapPoints.TriangleMove(speed, PinkTriangle1, PinkMapPoints._pinkMapTriangle1[0], PinkMapPoints._pinkMapTriangle1[1]);
        PinkMapPoints.CircleMove(speed, PinkCircle1, PinkMapPoints._pinkMapCircle1[0], PinkMapPoints._pinkMapCircle1[1], PinkMapPoints._pinkMapCircle1[2]);
    }
    #endregion



   



    #region KeyControlls
    void SetActiveFalse()
    {

        BrownCircle1.SetActive(false);
        GreenCircle1.SetActive(false);
        OrangeCircle1.SetActive(false);
        PinkCircle1.SetActive(false);
        BlueCircle1.SetActive(false);


        BrownTriangle1.SetActive(false);
        GreenTriangle1.SetActive(false);
        OrangeTriangle1.SetActive(false);
        PinkTriangle1.SetActive(false);
        BlueTriangle1.SetActive(false);
    }
    void BoardEntry()
    {
        if(Input.GetKey(KeyCode.RightArrow))
        {
            GreenToBrown.GetComponent<BoxCollider2D>().enabled = false;
        }
        if(Player.Brownleft==true)
        {
            GreenToBrown.GetComponent<BoxCollider2D>().enabled = true;
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            BrownToPink.GetComponent<BoxCollider2D>().enabled = false;
        }
        if (Player.Pinkleft == true)
        {
            BrownToPink.GetComponent<BoxCollider2D>().enabled = true;
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            PinkToBlue.GetComponent<BoxCollider2D>().enabled = false;
        }
        if (Player.Blueleft == true)
        {
            PinkToBlue.GetComponent<BoxCollider2D>().enabled = true;
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            BlueToOrange.GetComponent<BoxCollider2D>().enabled = false;
        }
        if (Player.Orangeleft == true)
        {
            BlueToOrange.GetComponent<BoxCollider2D>().enabled = true;
        }

        if (Input.GetKey(KeyCode.RightShift))
        {
            OrangeToBlue.GetComponent<BoxCollider2D>().enabled = false;
        }

        if (Input.GetKeyUp(KeyCode.E))
        {
            StartCoroutine(Focusing(cam.transform.position, BrownMap.transform.position));
            SetActiveFalse();
            BrownTriangle1.SetActive(true);
            BrownCircle1.SetActive(true);
             
        }

        if (Input.GetKeyUp(KeyCode.D))
        {
            StartCoroutine(Focusing(cam.transform.position, BlueMap.transform.position));
            SetActiveFalse();
            BlueTriangle1.SetActive(true);
            BlueCircle1.SetActive(true);

        }

        if (Input.GetKeyUp(KeyCode.S))
        {
            
            StartCoroutine(Focusing(cam.transform.position, OrangeMap.transform.position));
            SetActiveFalse();
            OrangeTriangle1.SetActive(true);
            OrangeCircle1.SetActive(true);

        }

        if (Input.GetKeyUp(KeyCode.A))
        {
            StartCoroutine(Focusing(cam.transform.position, PinkMap.transform.position));
            SetActiveFalse();
            PinkTriangle1.SetActive(true);
            PinkCircle1.SetActive(true);

        }

        if (Input.GetKeyUp(KeyCode.W))
        {
           
           
            StartCoroutine(Focusing(cam.transform.position, GreenMap.transform.position));
            SetActiveFalse();
           
           
        }
    }
    IEnumerator Focusing(Vector3 start, Vector3 target)
    {
        float t = 0;
        while (t <= 1.5)
        {
            t += Time.deltaTime * (Time.timeScale / 0.5f);
            Vector3 temp = Vector3.Lerp(start, target, t);
            cam.transform.position = new Vector3(temp.x, temp.y, transform.position.z);
            yield return null;

        }

    }
    #endregion
}