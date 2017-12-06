using UnityEngine;
using System.Collections.Generic;

public class AIScript : MonoBehaviour
{   //storing triangles and circles
    private List<GameObject> _triangles = new List<GameObject>();
    private List<GameObject> _circles   = new List<GameObject>();

    public static List<Transform> _greenMapCircle1 = new List<Transform>();
    public static List<Transform> _greenMapTriangle1 = new List<Transform>();


    public GameObject TriPrefab;
    public GameObject CirclePrefab;


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
        
            CreateTriangleInstances(1);
            CreateCircleInstances(1);
    }

    private void Update()
    {

        Debug.Log(GreenMapPoints._greenMapCircle1[0].position);
    }
#region MovingEnemies
    void CircleMove(GameObject CircleInstance, Transform firstTriPoint, Transform SecondTriPoint, Transform ThirdTriPoint)
    {

        int speed = 15;
        Vector3 firstpoint = firstTriPoint.position;
        Vector3 secondpoint = SecondTriPoint.position;
        Vector3 secpoint =   SecondTriPoint.position;
        Vector3 thirdpoint = ThirdTriPoint.position;


        Vector3 directionVector = (secondpoint - CircleInstance.transform.position);
        float angle = Mathf.Atan2(directionVector.x, directionVector.y) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(-angle, Vector3.forward);
        CircleInstance.transform.rotation = Quaternion.Lerp(CircleInstance.transform.rotation, rotation, 10 * Time.deltaTime);
        CircleInstance.transform.Translate(Vector3.up * speed * Time.deltaTime, Space.Self);


        if (Vector3.Distance(CircleInstance.transform.position, secondpoint) < 0.2f)
        {
            int random = Random.Range(0, 2);
            if (random == 0)
            {
                secondpoint = thirdpoint;
            }
            if (random == 1)
            {
                secondpoint = firstpoint;
            }
        }

        if (Vector3.Distance(CircleInstance.transform.position, thirdpoint) < 0.2f)
        {
            int random = Random.Range(0, 2);
            if (random == 0)
            {
                secondpoint = firstpoint;
            }
            if (random == 1)
            {
                secondpoint = secpoint;

            }
        }

        if (Vector3.Distance(CircleInstance.transform.position, firstpoint) < 0.2f)
        {
            int random = Random.Range(0, 2);
            if (random == 0)
            {
                secondpoint = secpoint;

            }
            if (random == 1)
            {
                secondpoint = thirdpoint;
            }
        }

    }

    void TriangleMove(GameObject triangleinstance, Transform firstpoint, Transform secondpoint)
    {
        float time = 0f;
        float speed = 15f;
        Vector3 spawnPoint = firstpoint.transform.position;
        Vector3 uppoint = firstpoint.position;
        Vector3 downpoint = secondpoint.position;
        Vector3 target = downpoint;

        Vector3 directionVector = (downpoint - triangleinstance.transform.position);
        float angle = Mathf.Atan2(directionVector.x, directionVector.y) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(-angle, Vector3.forward);
        time += 0.01f;
        if (time > 0.5f)
            time = 0;
        triangleinstance.transform.rotation = Quaternion.Lerp(triangleinstance.transform.rotation, rotation, time);
        triangleinstance.transform.Translate(Vector3.up * speed * Time.deltaTime, Space.Self);


        if (Vector3.Distance(triangleinstance.transform.position, downpoint) < 0.2f)
        {
            downpoint = uppoint;
        }

        if (Vector3.Distance(triangleinstance.transform.position, uppoint) < 0.2f)
        {
            downpoint = target;
        }

    }
#endregion
}
