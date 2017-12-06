using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrownMapPoints : MonoBehaviour {

    public static List<Transform> _brownMapCircle1 = new List<Transform>();
    public static List<Transform> _brownMapTriangle1 = new List<Transform>();
    public Transform Triangle1_Point1;
    public Transform Triangle1_Point2;
    public Transform Circle1_Point1;
    public Transform Circle1_Point2;
    public Transform Circle1_Point3;

    private void Awake()
    {
        brownMapCircle1_PointsToList();
        brownMapTriangle1_PointsToList();
    }
    private void brownMapTriangle1_PointsToList()
    {
        _brownMapTriangle1.Add(Triangle1_Point1);
        _brownMapTriangle1.Add(Triangle1_Point2);
    }
    private void brownMapCircle1_PointsToList()
    {
        _brownMapCircle1.Add(Circle1_Point1);
        _brownMapCircle1.Add(Circle1_Point2);
        _brownMapCircle1.Add(Circle1_Point3);
    }
}
