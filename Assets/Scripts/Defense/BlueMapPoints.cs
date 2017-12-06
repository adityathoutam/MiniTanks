using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueMapPoints : MonoBehaviour {
    public static List<Transform> _blueMapCircle1 = new List<Transform>();
    public static List<Transform> _blueMapTriangle1 = new List<Transform>();
    public Transform Triangle1_Point1;
    public Transform Triangle1_Point2;
    public Transform Circle1_Point1;
    public Transform Circle1_Point2;
    public Transform Circle1_Point3;

    private void Awake()
    {
        blueMapCircle1_PointsToList();
        blueMapTriangle1_PointsToList();
    }
    private void blueMapTriangle1_PointsToList()
    {
        _blueMapTriangle1.Add(Triangle1_Point1);
        _blueMapTriangle1.Add(Triangle1_Point2);
    }
    private void blueMapCircle1_PointsToList()
    {
        _blueMapCircle1.Add(Circle1_Point1);
        _blueMapCircle1.Add(Circle1_Point2);
        _blueMapCircle1.Add(Circle1_Point3);
    }
}
