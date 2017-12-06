using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrangeMapPoints : MonoBehaviour {

    public static List<Transform> _orangeMapCircle1 = new List<Transform>();
    public static List<Transform> _orangeMapTriangle1 = new List<Transform>();
    public Transform Triangle1_Point1;
    public Transform Triangle1_Point2;
    public Transform Circle1_Point1;
    public Transform Circle1_Point2;
    public Transform Circle1_Point3;

    private void Awake()
    {
        orangeMapCircle1_PointsToList();
        orangeMapTriangle1_PointsToList();
    }
    private void orangeMapTriangle1_PointsToList()
    {
        _orangeMapTriangle1.Add(Triangle1_Point1);
        _orangeMapTriangle1.Add(Triangle1_Point2);
    }
    private void orangeMapCircle1_PointsToList()
    {
        _orangeMapCircle1.Add(Circle1_Point1);
        _orangeMapCircle1.Add(Circle1_Point2);
        _orangeMapCircle1.Add(Circle1_Point3);
    }
}
