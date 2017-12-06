using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinkMapPoints : MonoBehaviour {

    public static List<Transform> _pinkMapCircle1 = new List<Transform>();
    public static List<Transform> _pinkMapTriangle1 = new List<Transform>();
    public Transform Triangle1_Point1;
    public Transform Triangle1_Point2;
    public Transform Circle1_Point1;
    public Transform Circle1_Point2;
    public Transform Circle1_Point3;

    private void Awake()
    {
        pinkMapCircle1_PointsToList();
        pinkMapTriangle1_PointsToList();
    }
    private void pinkMapTriangle1_PointsToList()
    {
        _pinkMapTriangle1.Add(Triangle1_Point1);
        _pinkMapTriangle1.Add(Triangle1_Point2);
    }
    private void pinkMapCircle1_PointsToList()
    {
        _pinkMapCircle1.Add(Circle1_Point1);
        _pinkMapCircle1.Add(Circle1_Point2);
        _pinkMapCircle1.Add(Circle1_Point3);
    }
}
