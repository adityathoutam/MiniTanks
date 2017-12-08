using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor.SceneManagement;

public class Bullet : MonoBehaviour
{
    public List<EVENT_TYPE> eventsList;
    private List<GameObject> trajectoryList;

    public GameObject OutOfBoundsPanel;
    public GameObject WinPanel;

    public GameObject player1;
    public GameObject player2;


    public GameObject BallPrefab;
    public GameObject ball;


    private float power = 25f;
    private int numberOfPoints = 30;
    public float transitionDuration = 0.5f;

    public GameObject camera1;
    public GameObject trajectoryPointPrefab;





    private bool NewGame = false;
    private bool NewGameCam = false;
    private bool Moving = false;
   

    void Awake()
    {
        CreateBall();
        DisplayTrajectory();

       
    }
    private void Start()
    {
        NewGame = true;
       
        
    }
    #region BALL
    private void CreateBall()
    {
        ball = (GameObject)Instantiate(BallPrefab);

    }
    private void Fire(Vector3 directionVector)
    {
        ball.GetComponent<Rigidbody>().AddForce(directionVector * power, ForceMode.Impulse);
    }


    #endregion

    #region EVENT_SYSTEM
    void OnEnable()
    {
        EventRelay.OnEvent += HandleEvent;
    }
    void OnDisable()
    {
        EventRelay.OnEvent -= HandleEvent;
    }

    void HandleEvent(EVENT_TYPE type, System.Object data)
    {
        if (eventsList.Contains(type))
        {
            switch (type)
            {
                case EVENT_TYPE.BEGAN:

                    break;

                case EVENT_TYPE.MOVED:

                    SetTrajectoryPath(ball.transform.position, (Vector3)data * power);


                    break;
                case EVENT_TYPE.ENDED:

                    Fire((Vector3)data);
					CameraInterpolePlayerToEnemy();
                   

                    break;
            }
        }
    }
    #endregion
    private void FixedUpdate()
    {
        
        
    }
    private void Update()
    {
        StartingCameraMovement();


        if(Input.GetKey(KeyCode.Space))
        {
            NewGame = true;
        }

        #region PLAYER1_THROWS_OUT_OF_BOUNDS

        if (ball.transform.position.x > 90 || ball.transform.position.y > 50)
        {

            OutOfBoundsPanel.SetActive(true);
        }

        if (ball.transform.position.x < -90 || ball.transform.position.y > 50)
        {

            OutOfBoundsPanel.SetActive(true);
        }
        #endregion PLAYER1_THROWS_OUT_OF_BOUNDS_RESET_TO_PLAYER1

    }

    #region CAMERA_PLAYER_CAMERA

    void CameraInterpolePlayerToEnemy()
    {
         StartCoroutine(Transition(player1.transform.position, player2.transform.position));   
    }
    void CameraInterpoleEnemyToPlayer()
    {
        StartCoroutine(Transition(player2.transform.position, player1.transform.position));
    }
    IEnumerator Transition(Vector3 startpos, Vector3 endpos)
    {
        Moving = true;
        float t = 0f;
        while (t <= 1.5f)
        {
            t += Time.deltaTime * (Time.timeScale / transitionDuration);
            Vector3 temp = Vector3.Lerp(startpos, endpos, t);
           camera1.transform.position = new Vector3(temp.x, temp.y, camera1.transform.position.z);
            yield return null;

        }
        Moving = false;
        
    }
    #endregion CAMERA_PLAYER_CAMERA



    #region TRAJECTORY
    public void SetTrajectoryPath(Vector3 startPos, Vector3 pVelocity)
    {
        float vel = Mathf.Sqrt((pVelocity.x * pVelocity.x) + (pVelocity.y * pVelocity.y));

        float angle = Mathf.Rad2Deg * (Mathf.Atan2(pVelocity.y, pVelocity.x));

        float time = 0;

        time += 0.1f;
        for (int i = 0; i < numberOfPoints; ++i)
        {
            float x = vel * time * Mathf.Cos(angle * Mathf.Deg2Rad);
            float y = vel * time * Mathf.Sin(angle * Mathf.Deg2Rad) - (9.8f * time * time / 2);

            Vector3 pos = new Vector3(startPos.x + x, startPos.y + y, 0);

            trajectoryList[i].transform.position = pos;
            trajectoryList[i].transform.eulerAngles = new Vector3(0, 0, Mathf.Atan2(pVelocity.y - (9.8f) * time, pVelocity.x) * Mathf.Rad2Deg);
            time += 0.1f;
        }
    }
    void DisplayTrajectory()
    {
        trajectoryList = new List<GameObject>();

        for (int i = 0; i < numberOfPoints; i++)
        {
            GameObject go = (GameObject)Instantiate(trajectoryPointPrefab);

            trajectoryList.Add(go);
        }

    }
    #endregion TRAJECTORY

    void StartingCameraMovement()
    {
        if (NewGame == true)
        {

            CameraInterpolePlayerToEnemy();

            NewGame = false;
            NewGameCam = true;
        }

        if (NewGameCam == true && Moving == false)
        {
            
            CameraInterpoleEnemyToPlayer();
            Moving = true;
            NewGameCam = false;
        }
    }


    void MoveWithPlayer1()
    {
        ball.transform.parent = player1.transform;
        if (Vector3.Distance(ball.transform.position, player1.transform.position) > 5)
        {
            ball.transform.parent = null;
            CameraInterpolePlayerToEnemy();
           
        }
    }
   
  

}
