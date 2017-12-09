using UnityEngine;
using System.Collections.Generic;

public class AIScript : MonoBehaviour
{   
    public GameObject TriPrefab;
    public GameObject CirclePrefab;
    Vector3 CircleTarget;
    Vector3 TriangleTarget;
  

    GameObject GreenTriangle1;
    GameObject BrownTriangle1;
    GameObject BlueTriangle1;
    GameObject OrangeTriangle1;
    GameObject PinkTriangle1;

    GameObject GreenCircle1;
    GameObject BrownCircle1;
    GameObject BlueCircle1;
    GameObject OrangeCircle1;
    GameObject PinkCircle1;


    #region CreatingInstances
    private GameObject CreateTriangleInstances(Transform SpawnPosition)
    {
        Vector3 position = SpawnPosition.position;
        GameObject TriangleInstance = Instantiate(TriPrefab, position,Quaternion.identity);
        return TriangleInstance;
       
    }
    private GameObject CreateCircleInstances(Transform SpawnPosition)
    {
        Vector3 position = SpawnPosition.position;
        GameObject  CircleInstance = Instantiate(CirclePrefab, position, Quaternion.identity);
        return CircleInstance;
    }
#endregion

    private void Update()
    {
    #region Triangles
        if (GreenTriangle1 == null)
        {
            GreenTriangle1 = CreateTriangleInstances(GreenMapPoints._greenMapTriangle1[0]);  
        }
        TriangleMove(GreenTriangle1, GreenMapPoints._greenMapTriangle1[0], GreenMapPoints._greenMapTriangle1[1]);

        if(BrownTriangle1 ==null)
        {
            BrownTriangle1 = CreateTriangleInstances(BrownMapPoints._brownMapTriangle1[0]);
        }
        TriangleMove(BrownTriangle1, BrownMapPoints._brownMapTriangle1[0], BrownMapPoints._brownMapTriangle1[0]);

        //if(BlueTriangle1==null)
        //{
        //    BlueTriangle1 = CreateTriangleInstances(BlueMapPoints._blueMapTriangle1[0]);
        //}
        //TriangleMove(BlueTriangle1, BlueMapPoints._blueMapTriangle1[0], BlueMapPoints._blueMapTriangle1[1]);

        //if(OrangeTriangle1==null)
        //{
        //    OrangeTriangle1= CreateTriangleInstances(OrangeMapPoints._orangeMapTriangle1[0]);
        //}
        //TriangleMove(OrangeTriangle1, OrangeMapPoints._orangeMapTriangle1[0], OrangeMapPoints._orangeMapTriangle1[1]);

        //if(PinkTriangle1==null)
        //{
        //    PinkTriangle1 = CreateTriangleInstances(PinkMapPoints._pinkMapTriangle1[0]);
        //}
        //TriangleMove(PinkTriangle1, PinkMapPoints._pinkMapTriangle1[0], PinkMapPoints._pinkMapTriangle1[1]);
        #endregion
    #region Circles
        if (GreenCircle1 == null)
        {
            GreenCircle1 = CreateCircleInstances(GreenMapPoints._greenMapCircle1[0]);
        }
        CircleMove(GreenCircle1, GreenMapPoints._greenMapCircle1[0], GreenMapPoints._greenMapCircle1[1], GreenMapPoints._greenMapCircle1[2]);

        if (BrownCircle1 == null)
        {
            BrownCircle1 = CreateCircleInstances(BrownMapPoints._brownMapCircle1[0]);
        }
        //CircleMove(BrownCircle1, BrownMapPoints._brownMapCircle1[0], BrownMapPoints._brownMapCircle1[1], BrownMapPoints._brownMapCircle1[2]);
        //if (BlueCircle1 == null)
        //{
        //    BlueCircle1 = CreateCircleInstances(BlueMapPoints._blueMapCircle1[0]);
        //}
        //CircleMove(BlueCircle1, BlueMapPoints._blueMapCircle1[0], BlueMapPoints._blueMapCircle1[1], BlueMapPoints._blueMapCircle1[2]);

        //if (OrangeCircle1 == null)
        //{
        //    OrangeCircle1 = CreateCircleInstances(OrangeMapPoints._orangeMapCircle1[0]);
        //}
        //CircleMove(OrangeCircle1, OrangeMapPoints._orangeMapCircle1[0], OrangeMapPoints._orangeMapCircle1[1], OrangeMapPoints._orangeMapCircle1[2]);

        //if (PinkCircle1 == null)
        //{
        //    PinkCircle1 = CreateCircleInstances(PinkMapPoints._pinkMapCircle1[0]);
        //}
        //CircleMove(PinkCircle1, PinkMapPoints._pinkMapCircle1[0], PinkMapPoints._pinkMapCircle1[1], PinkMapPoints._pinkMapCircle1[2]);

#endregion


    }
    #region MovingEnemies
    void CircleMove(GameObject CircleInstance, Transform firstCirPoint, Transform SecondCirPoint, Transform ThirdCirPoint)
    {
        int speed = 15;
        Vector3 firstpoint = firstCirPoint.position;
        Vector3 secondpoint = SecondCirPoint.position;
        Vector3 thirdpoint = ThirdCirPoint.position;
        //AT FIRST THE TARGET IS SECOND POINT
        if(CircleTarget == null)
        {
            CircleTarget = secondpoint;
        }
        Vector3 directionVector = (CircleTarget - CircleInstance.transform.position);
        float angle = Mathf.Atan2(directionVector.x, directionVector.y) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(-angle, Vector3.forward);
        CircleInstance.transform.rotation = Quaternion.Lerp(CircleInstance.transform.rotation, rotation, 10 * Time.deltaTime);
        CircleInstance.transform.Translate(Vector3.up * speed * Time.deltaTime, Space.Self);
        if (Vector3.Distance(CircleInstance.transform.position, CircleTarget) < 0.2f)
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

        if (Vector3.Distance(CircleInstance.transform.position, CircleTarget) < 0.2f)
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

        if (Vector3.Distance(CircleInstance.transform.position, CircleTarget) < 0.2f)
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

    void TriangleMove(GameObject triangleinstance, Transform firstpoint, Transform secondpoint)
    {
        float speed = 15f;
        Vector3 uppoint = firstpoint.position;
        Vector3 downpoint = secondpoint.position;

        if (TriangleTarget == null)
        {
            TriangleTarget = downpoint;
        }

        Vector3 directionVector = (TriangleTarget - triangleinstance.transform.position);
        float angle = Mathf.Atan2(directionVector.x, directionVector.y) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(-angle, Vector3.forward);
        triangleinstance.transform.rotation = Quaternion.Lerp(triangleinstance.transform.rotation, rotation, 10*Time.deltaTime);
        triangleinstance.transform.Translate(Vector3.up * speed * Time.deltaTime, Space.Self);


        if (Vector3.Distance(triangleinstance.transform.position, TriangleTarget) < 0.2f)
        {
            TriangleTarget = uppoint;
        }
        if (Vector3.Distance(triangleinstance.transform.position, TriangleTarget) < 0.2f)
        {
            TriangleTarget = downpoint;
        }

    }
#endregion
}
