using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenMapPoints: MonoBehaviour{


    public static List<Transform> _greenMapCircle1 = new List<Transform>();
    public static List<Transform> _greenMapTriangle1 = new List<Transform>();
    public  Transform Triangle1_Point1;
    public  Transform Triangle1_Point2;
    public  Transform Circle1_Point1;
    public  Transform Circle1_Point2;
    public  Transform Circle1_Point3;

    private void Awake()
    {
        GreenMapCircle1_PointsToList();
        GreenMapTriangle1_PointsToList();
    }
    private void GreenMapTriangle1_PointsToList()
    {
        _greenMapTriangle1.Add(Triangle1_Point1);
        _greenMapTriangle1.Add(Triangle1_Point2);
    }
    private  void GreenMapCircle1_PointsToList()
    {
        _greenMapCircle1.Add(Circle1_Point1);
        _greenMapCircle1.Add(Circle1_Point2);
        _greenMapCircle1.Add(Circle1_Point3);
    }

}
