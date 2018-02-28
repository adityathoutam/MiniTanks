using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class AIScript : MonoBehaviour
{
    public GameObject TriPrefab;
    public GameObject CirclePrefab;
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

    public GreenMapPoints gmp;
    public BrownMapPoints bmp;
    public PinkMapPoints pmp;
    public BlueMapPoints blmp;
    public OrangeMapPoints omp;

    private void Start()
    {
        GreenTanksCreate();
        BrownTanksCreate();
        OrangeTanksCreate();
        BlueTanksCreate();
        PinkTanksCreate();
    }
    private void Update()
    {
        
        Destroyer();

        if (GreenTriangle1 != null)
            GreenTriangleMove();
        if (GreenCircle1 != null)
            GreenCircleMove();
        if (BrownTriangle1 != null)
            BrownTriangleMove();
        if (BrownCircle1 != null)
            BrownCircleMove();
        if (OrangeTriangle1 != null)
            OrangeTriangleMove();
        if (OrangeCircle1 != null)
            OrangeCircleMove();
        if (BlueTriangle1 != null)
            BlueTriangleMove();
        if (BlueCircle1 != null)
            BlueCircleMove();
        if (PinkTriangle1 != null)
            PinkTriangleMove();
        if (PinkCircle1 != null)
            PinkCircleMove();
    }
    void Destroyer()
    {

        if(MainManager.Completed_0_5)
        {
            if (MainManager.Triangle)
            {
                Destroy(GreenTriangle1);
                MainManager.Triangle = false;
            }
            //OR
            if (MainManager.Circle)
            {
                Destroy(GreenCircle1);
                MainManager.Circle = false;
            }
            MainManager.Completed_0_5 = false;
            
        }
        if (MainManager.Completed_1)
        {
            Destroy(GreenCircle1);
            //AND
            Destroy(GreenTriangle1);
            MainManager.Completed_1 = false;
        }
        if(MainManager.Completed_1_5)
        {
            Destroy(GreenTriangle1);
            Destroy(GreenCircle1);
            // AND
            if (MainManager.Triangle)
            {
                Destroy(BrownTriangle1);
                MainManager.Triangle = false;
            }
            //OR
            if (MainManager.Circle)
            {
                Destroy(BrownCircle1);
                MainManager.Circle = false;
            }

            MainManager.Completed_1_5 = false;

        }
        if (MainManager.Completed_2)
        {
            Destroy(GreenTriangle1);
            Destroy(GreenCircle1);
           // AND
            Destroy(BrownTriangle1);
            Destroy(BrownCircle1);
            MainManager.Completed_2 = false;
        }
        if(MainManager.Completed_2_5)
        {
            Destroy(GreenTriangle1);
            Destroy(GreenCircle1);
            Destroy(BrownTriangle1);
            Destroy(BrownCircle1);
            //AND
            if (MainManager.Triangle)
            {
                Destroy(PinkTriangle1);
                MainManager.Triangle = false;
            }
            //OR
            if (MainManager.Circle)
            {
                Destroy(PinkCircle1);
                MainManager.Circle = false;
            }
            MainManager.Completed_2_5 = false;
        }
        if (MainManager.Completed_3)
        {
            Destroy(GreenTriangle1);
            Destroy(GreenCircle1);
            Destroy(BrownTriangle1);
            Destroy(BrownCircle1);
           // AND
            Destroy(PinkTriangle1);
            Destroy(PinkCircle1);
            MainManager.Completed_3 = false;
        }
        if(MainManager.Completed_3_5)
        {
            Destroy(GreenTriangle1);
            Destroy(GreenCircle1);
            Destroy(BrownTriangle1);
            Destroy(BrownCircle1);
            Destroy(PinkTriangle1);
            Destroy(PinkCircle1);
            if (MainManager.Triangle)
            {
                Destroy(BlueTriangle1);
                MainManager.Triangle = false;
            }
            //OR
            if (MainManager.Circle)
            {
                Destroy(BlueCircle1);
                MainManager.Circle = false;
            }
            MainManager.Completed_3_5 = false;

        }
        if (MainManager.Completed_4)
        {
            Destroy(GreenTriangle1);
            Destroy(GreenCircle1);
            Destroy(BrownTriangle1);
            Destroy(BrownCircle1);
            Destroy(PinkTriangle1);
            Destroy(PinkCircle1);
            //AND
            Destroy(BlueTriangle1);
            Destroy(BlueCircle1);
            MainManager.Completed_4 = false;
        }
        if (MainManager.Completed_4_5)
        {
            Destroy(GreenTriangle1);
            Destroy(GreenCircle1);
            Destroy(BrownTriangle1);
            Destroy(BrownCircle1);
            Destroy(PinkTriangle1);
            Destroy(PinkCircle1);
            Destroy(BlueTriangle1);
            Destroy(BlueCircle1);
            if (MainManager.Triangle)
            {
                Destroy(OrangeTriangle1);
                MainManager.Triangle = false;
            }
            //OR
            if (MainManager.Circle)
            {
                Destroy(OrangeCircle1);
                MainManager.Circle = false;
            }
            MainManager.Completed_4_5 = false;
        }
        if (MainManager.Completed_5)
        {
            Destroy(GreenTriangle1);
            Destroy(GreenCircle1);
            Destroy(BrownTriangle1);
            Destroy(BrownCircle1);
            Destroy(PinkTriangle1);
            Destroy(PinkCircle1);
            Destroy(BlueTriangle1);
            Destroy(BlueCircle1);
            Destroy(OrangeTriangle1);
            Destroy(OrangeCircle1);
            MainManager.Completed_5 = false;
        }
    }
    #region TanksCreateAndMove
    void GreenTanksCreate()
    {
        if (GreenTriangle1 == null)
        {
            GreenTriangle1 = Instantiate(TriPrefab);
            GreenTriangle1.transform.position = gmp._greenMapTriangle1[0].transform.position;
        }
        if (GreenCircle1 == null)
        {
            GreenCircle1 = Instantiate(CirclePrefab);
            GreenCircle1.transform.position = gmp._greenMapCircle1[0].transform.position;
        }
    }
    void GreenTriangleMove()
    {
        gmp.TriangleMove(speed, GreenTriangle1, gmp._greenMapTriangle1[0], gmp._greenMapTriangle1[1]);
    }
    void GreenCircleMove()
    { 
        gmp.CircleMove(speed, GreenCircle1, gmp._greenMapCircle1[0], gmp._greenMapCircle1[1], gmp._greenMapCircle1[2]);
    }
    void BrownTanksCreate()
    {
        if (BrownTriangle1 == null)
        {
            BrownTriangle1 = Instantiate(TriPrefab);
            BrownTriangle1.transform.position = bmp._brownMapTriangle1[0].transform.position;
        }
        if (BrownCircle1 == null)
        {
            BrownCircle1 = Instantiate(CirclePrefab);
            BrownCircle1.transform.position = bmp._brownMapCircle1[0].transform.position;
        }
    }
    void BrownTriangleMove()
    {
        bmp.TriangleMove(speed, BrownTriangle1, bmp._brownMapTriangle1[0], bmp._brownMapTriangle1[1]);
    }
    void BrownCircleMove()
    { 
        bmp.CircleMove(speed, BrownCircle1, bmp._brownMapCircle1[0], bmp._brownMapCircle1[1], bmp._brownMapCircle1[2]);
    }
    void OrangeTanksCreate()
    {
        if (OrangeTriangle1 == null)
        {
            OrangeTriangle1 = Instantiate(TriPrefab);
            OrangeTriangle1.transform.position = omp._orangeMapTriangle1[0].transform.position;
        }
        if (OrangeCircle1 == null)
        {
            OrangeCircle1 = Instantiate(CirclePrefab);
            OrangeCircle1.transform.position = omp._orangeMapCircle1[0].transform.position;
        }
    }
    void OrangeTriangleMove()
    {
        omp.TriangleMove(speed, OrangeTriangle1, omp._orangeMapTriangle1[0], omp._orangeMapTriangle1[1]);
    }
    void OrangeCircleMove()
    { 
        omp.CircleMove(speed, OrangeCircle1, omp._orangeMapCircle1[0], omp._orangeMapCircle1[1], omp._orangeMapCircle1[2]);
    }
    void BlueTanksCreate()
    {
        if (BlueTriangle1 == null)
        {
            BlueTriangle1 = Instantiate(TriPrefab);
            BlueTriangle1.transform.position = blmp._blueMapTriangle1[0].transform.position;
        }
        blmp.TriangleMove(speed, BlueTriangle1, blmp._blueMapTriangle1[0], blmp._blueMapTriangle1[1]);
        if (BlueCircle1 == null)
        {
            BlueCircle1 = Instantiate(CirclePrefab);
            BlueCircle1.transform.position = blmp._blueMapCircle1[0].transform.position;
        }
        blmp.CircleMove(speed, BlueCircle1, blmp._blueMapCircle1[0], blmp._blueMapCircle1[1], blmp._blueMapCircle1[2]);
    }
    void BlueTriangleMove()
    {
        blmp.TriangleMove(speed, BlueTriangle1, blmp._blueMapTriangle1[0], blmp._blueMapTriangle1[1]);
    }
    void BlueCircleMove()
    { 
        blmp.CircleMove(speed, BlueCircle1, blmp._blueMapCircle1[0], blmp._blueMapCircle1[1], blmp._blueMapCircle1[2]);
    }
    void PinkTanksCreate()
    {
        if (PinkTriangle1 == null)
        {
            PinkTriangle1 = Instantiate(TriPrefab);
            PinkTriangle1.transform.position = pmp._pinkMapTriangle1[0].transform.position;
        }
        pmp.TriangleMove(speed, PinkTriangle1, pmp._pinkMapTriangle1[0], pmp._pinkMapTriangle1[1]);
        if (PinkCircle1 == null)
        {
            PinkCircle1 = Instantiate(CirclePrefab);
            PinkCircle1.transform.position = pmp._pinkMapCircle1[0].transform.position;
        }
        pmp.CircleMove(speed, PinkCircle1, pmp._pinkMapCircle1[0], pmp._pinkMapCircle1[1], pmp._pinkMapCircle1[2]);
    }
    void PinkTriangleMove()
    {
        pmp.TriangleMove(speed, PinkTriangle1, pmp._pinkMapTriangle1[0], pmp._pinkMapTriangle1[1]);
    }
    void PinkCircleMove()
    { 
        pmp.CircleMove(speed, PinkCircle1, pmp._pinkMapCircle1[0], pmp._pinkMapCircle1[1], pmp._pinkMapCircle1[2]);
    }
    #endregion
}