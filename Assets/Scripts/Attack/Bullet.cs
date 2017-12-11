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

    public GameObject Player;
    public GameObject Enemy;
    public GameObject bezierpoint;


    public GameObject BallPrefab;
    public GameObject ball;
    GameObject trajectorydots;


    private float power = 25f;
    private int numberOfPoints = 15;
    public float transitionDuration = 0.5f;

    public GameObject camera1;
    public GameObject trajectoryPointPrefab;

    private bool NewGame = false;
    private bool NewGameCam = false;
    private bool Moving = false;

    private bool isFired = false;
    private int chances = 3;
    private bool ReadyToShoot = false;
    public bool isEnemyTrajectoryisActive = false;
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
        isFired = true;
        HideTrajectory();
        CameraInterpolePlayerToEnemy();
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
                    
                        DisplayTrajectory();
                    
                    break;

                case EVENT_TYPE.MOVED:

                    PlayerTrajectoryPath(ball.transform.position, (Vector3)data * power);


                    break;
                case EVENT_TYPE.ENDED:
                    
                        Fire((Vector3)data);
                        isFired = true;
                    

                    break;
            }
        }
    }
    #endregion

    private void Update()
    {
       
        
        if (isEnemyTrajectoryisActive == false&&TouchController.PlayerReadyToShoot==false)
        {
            DisplayTrajectory();
            EnemyTrajectory();

            isEnemyTrajectoryisActive = true;
        }
        
        StartingCameraMovement();
        

        if(Input.GetKey(KeyCode.Space))
        {
            NewGame = true;
        }

        #region PLAYER1_THROWS_OUT_OF_BOUNDS

        if (ball.transform.position.x > 90 || ball.transform.position.y > 50)
        {
            ball.GetComponent<Rigidbody>().isKinematic = true;
            MoveWithPlayer1();
            chances--;
            Debug.Log("ONLY " + chances + " CHANCE/CHANCES LEFT");

            CameraInterpoleEnemyToPlayer();
            isFired = true;

        }

        if (ball.transform.position.x < -90 || ball.transform.position.y > 50)
        {
            ball.GetComponent<Rigidbody>().isKinematic = true;
            MoveWithPlayer1();
            chances--;
            Debug.Log("ONLY " + chances + " CHANCE/CHANCES LEFT");
             
                CameraInterpoleEnemyToPlayer();
            isFired = true;
               
        }
        #endregion PLAYER1_THROWS_OUT_OF_BOUNDS_RESET_TO_PLAYER1
        if (chances == -1)
            Debug.Log("OUT OF CHANCES");
    }

    #region CAMERA_PLAYER_CAMERA

    void CameraInterpolePlayerToEnemy()
    {
         StartCoroutine(Transition(Player.transform.position, Enemy.transform.position));   
    }
    void CameraInterpoleEnemyToPlayer()
    {
        StartCoroutine(Transition(Enemy.transform.position, Player.transform.position));
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
    //Player Trajectory Calculated 
    public void PlayerTrajectoryPath(Vector3 startPos, Vector3 pVelocity)
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
    //Enemy Trajectory Calculated
    void EnemyTrajectory()
    {
        Vector3 p1 = Player.transform.position;
        Vector3 p2 = bezierpoint.transform.position;
        Vector3 p3 = Enemy.transform.position;

        for (int i = 1; i < numberOfPoints + 1; i++)
        {
            float t = i / (float)numberOfPoints;
            t = Mathf.Clamp01(t);
            Vector3 part1 = Mathf.Pow(1 - t, 2) * p1;
            Vector3 part2 = 2 * (1 - t) * t * p2;
            Vector3 part3 = Mathf.Pow(t, 2) * p3;

            Vector3 pos = part1 + part2 + part3;
            trajectoryList[i - 1].transform.position = pos;
        }

    }
    //Display Trajectory
    void DisplayTrajectory()
    {
        trajectoryList = new List<GameObject>();

        for (int i = 0; i < numberOfPoints; i++)
        {
            trajectorydots = Instantiate(trajectoryPointPrefab);

            trajectoryList.Add(trajectorydots);
            trajectoryList[i].SetActive(true);
        }
        
    }
    //Hide Trajectory
    void HideTrajectory()
    {
        for (int i = 0; i < numberOfPoints; i++)
        {
            trajectoryList[i].SetActive(false);
        }
    }
    #endregion TRAJECTORY

    
    void MoveWithPlayer1()
    {

        ball.transform.position = new Vector3(Player.transform.position.x, Player.transform.position.y + 2.3f, Player.transform.position.z);
        ball.GetComponent<Rigidbody>().isKinematic = false;
        ball.transform.parent = Player.transform;


        if (Vector3.Distance(ball.transform.position, Player.transform.position) > 5)
        {
            ball.transform.parent = null;
            CameraInterpolePlayerToEnemy();

        }
    }
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


}
