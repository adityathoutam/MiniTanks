using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrangeMapPoints : MonoBehaviour {

    public static List<GameObject> _orangeMapCircle1 = new List<GameObject>();
    public static List<GameObject> _orangeMapTriangle1 = new List<GameObject>();

    public GameObject Triangle1_Point1;
    public GameObject Triangle1_Point2;

    public GameObject Circle1_Point1;
    public GameObject Circle1_Point2;
    public GameObject Circle1_Point3;

    static GameObject TriangleTarget;
    static GameObject CircleTarget;


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
    public static void TriangleMove(float speed, GameObject triangleinstance, GameObject firstpoint, GameObject secondpoint)
    {


        speed = 1f;
        GameObject uppoint = firstpoint;
        GameObject downpoint = secondpoint;


        if (TriangleTarget == null)
        {
            TriangleTarget = downpoint;
            TriangleTarget.transform.position = downpoint.transform.position;
        }


        Vector3 directionVector = (TriangleTarget.transform.position - triangleinstance.transform.position);
        float angle = Mathf.Atan2(directionVector.x, directionVector.y) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(-angle, Vector3.forward);
        triangleinstance.transform.rotation = Quaternion.Lerp(triangleinstance.transform.rotation, rotation, 10 * Time.deltaTime);
        triangleinstance.transform.Translate(directionVector * speed * Time.deltaTime, Space.World);



        if (Vector3.Distance(triangleinstance.transform.position, TriangleTarget.transform.position) < 0.2f)
        {
            TriangleTarget = uppoint;
        }
        if (Vector3.Distance(triangleinstance.transform.position, TriangleTarget.transform.position) < 0.2f)
        {
            TriangleTarget = downpoint;
        }

    }
    public static void CircleMove(float speed, GameObject CircleInstance, GameObject firstCirPoint, GameObject SecondCirPoint, GameObject ThirdCirPoint)
    {
        speed = 1f;
        GameObject firstpoint = firstCirPoint;
        GameObject secondpoint = SecondCirPoint;
        GameObject thirdpoint = ThirdCirPoint;

        if (CircleTarget == null)
        {
            CircleTarget = secondpoint;
            CircleTarget.transform.position = secondpoint.transform.position;
        }

        Vector3 directionVector = (CircleTarget.transform.position - CircleInstance.transform.position);
        float angle = Mathf.Atan2(directionVector.x, directionVector.y) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(-angle, Vector3.forward);
        CircleInstance.transform.rotation = Quaternion.Lerp(CircleInstance.transform.rotation, rotation, 10 * Time.deltaTime);
        CircleInstance.transform.Translate(directionVector * speed * Time.deltaTime, Space.World);
        if (Vector3.Distance(CircleInstance.transform.position, CircleTarget.transform.position) < 0.2f)
        {
            int random = Random.Range(0, 2);
            if (random == 0)
            {
                CircleTarget = thirdpoint;
            }
            if (random == 1)
            {
                CircleTarget = firstpoint;
            }
        }

        if (Vector3.Distance(CircleInstance.transform.position, CircleTarget.transform.position) < 0.2f)
        {
            int random = Random.Range(0, 2);
            if (random == 0)
            {
                CircleTarget = firstpoint;
            }
            if (random == 1)
            {
                CircleTarget = secondpoint;
            }
        }

        if (Vector3.Distance(CircleInstance.transform.position, CircleTarget.transform.position) < 0.2f)
        {
            int random = Random.Range(0, 2);
            if (random == 0)
            {
                CircleTarget = secondpoint;
            }
            if (random == 1)
            {
                CircleTarget = thirdpoint;
            }
        }
    }
}
