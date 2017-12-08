using UnityEngine;
using System.Collections.Generic;

public class AIScript : MonoBehaviour
{   //storing triangles and circles
    private List<GameObject> _triangles = new List<GameObject>();
    private List<GameObject> _circles   = new List<GameObject>();

 
    public GameObject TriPrefab;
    public GameObject CirclePrefab;
    Vector3 Target;
    Vector3 target;
    Vector3 firstpoint;
    Vector3 secondpoint;
    Vector3 thirdpoint;

    #region CreatingInstances
    private void CreateTriangleInstances(int instanceCount)
    {
            for (int i = 0; i < instanceCount; ++i)
        {
            var instance = Instantiate(TriPrefab) as GameObject;
            _triangles.Add(instance);
        }
    }
    private void CreateCircleInstances(int instanceCount)
    {
        for (int i = 0; i < instanceCount; ++i)
        {
            var instance = Instantiate(CirclePrefab) as GameObject;
            _circles.Add(instance);
        }
    }
#endregion
    private void Start()
    {
        
            CreateTriangleInstances(2);
            CreateCircleInstances(2);

    }

    private void Update()
    {

        //if(GreenMapisActivated==true)
        TriangleMove(_triangles[0],GreenMapPoints._greenMapTriangle1[0],GreenMapPoints._greenMapTriangle1[1]);
        CircleMove(_circles[0], GreenMapPoints._greenMapCircle1[0], GreenMapPoints._greenMapCircle1[1], GreenMapPoints._greenMapCircle1[2]);

        //if(BrownMapisActivated==true)
        //TriangleMove(_triangles[0], BrownMapPoints._brownMapTriangle1[0], BrownMapPoints._brownMapTriangle1[1]);
        //CircleMove(_circles[0], BrownMapPoints._brownMapCircle1[0], BrownMapPoints._brownMapCircle1[1], BrownMapPoints._brownMapCircle1[2]);

        //if(BlueMapisActivated==true)
        //TriangleMove(_triangles[0], BlueMapPoints._blueMapTriangle1[0], BlueMapPoints._blueMapTriangle1[1]);
        //CircleMove(_circles[0], BlueMapPoints._blueMapCircle1[0], BlueMapPoints._blueMapCircle1[1], BlueMapPoints._blueMapCircle1[2]);

        //if(OrangeMapisActivated==true)
        //TriangleMove(_triangles[0], OrangeMapPoints._orangeMapTriangle1[0], OrangeMapPoints._orangeMapTriangle1[1]);
        //CircleMove(_circles[0], OrangeMapPoints._orangeMapCircle1[0], OrangeMapPoints._orangeMapCircle1[1], OrangeMapPoints._orangeMapCircle1[2]);

        //if(PinkMapisActivated==true)
        //TriangleMove(_triangles[0], PinkMapPoints._pinkMapTriangle1[0], PinkMapPoints._pinkMapTriangle1[1]);
        //CircleMove(_circles[0], PinkMapPoints._pinkMapCircle1[0], PinkMapPoints._pinkMapCircle1[1], PinkMapPoints._pinkMapCircle1[2]);



    }
    #region MovingEnemies
    void CircleMove(GameObject CircleInstance, Transform firstCirPoint, Transform SecondCirPoint, Transform ThirdCirPoint)
    {
        int speed = 15;
        firstpoint = firstCirPoint.position;
        secondpoint = SecondCirPoint.position;
        thirdpoint = ThirdCirPoint.position;
        //AT FIRST THE TARGET IS SECOND POINT
        if(Target == null)
        {
            Target = secondpoint;
        }
        Vector3 directionVector = (Target - CircleInstance.transform.position);
        float angle = Mathf.Atan2(directionVector.x, directionVector.y) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(-angle, Vector3.forward);
        CircleInstance.transform.rotation = Quaternion.Lerp(CircleInstance.transform.rotation, rotation, 10 * Time.deltaTime);
        CircleInstance.transform.Translate(Vector3.up * speed * Time.deltaTime, Space.Self);
        if (Vector3.Distance(CircleInstance.transform.position, Target) < 0.2f)
        {
            int random = Random.Range(0, 2);
            if (random == 0)
            {
                Target = thirdpoint;
            }
            if (random == 1)
            {
                Target = firstpoint;
            }
        }

        if (Vector3.Distance(CircleInstance.transform.position, Target) < 0.2f)
        {
            int random = Random.Range(0, 2);
            if (random == 0)
            {
                Target = firstpoint;
            }
            if (random == 1)
            {
                Target = secondpoint;
            }
        }

        if (Vector3.Distance(CircleInstance.transform.position, Target) < 0.2f)
        {
            int random = Random.Range(0, 2);
            if (random == 0)
            {
                Target = secondpoint;
            }
            if (random == 1)
            {
                Target = thirdpoint;
            }
        }
    }

    void TriangleMove(GameObject triangleinstance, Transform firstpoint, Transform secondpoint)
    {
        float speed = 15f;
        Vector3 uppoint = firstpoint.position;
        Vector3 downpoint = secondpoint.position;

        if (target == null)
        {
            target = downpoint;
        }

        Vector3 directionVector = (target - triangleinstance.transform.position);
        float angle = Mathf.Atan2(directionVector.x, directionVector.y) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(-angle, Vector3.forward);
        triangleinstance.transform.rotation = Quaternion.Lerp(triangleinstance.transform.rotation, rotation, 10*Time.deltaTime);
        triangleinstance.transform.Translate(Vector3.up * speed * Time.deltaTime, Space.Self);


        if (Vector3.Distance(triangleinstance.transform.position, target) < 0.2f)
        {
            target = uppoint;
        }
        if (Vector3.Distance(triangleinstance.transform.position, target) < 0.2f)
        {
            target = downpoint;
        }

    }
#endregion
}
