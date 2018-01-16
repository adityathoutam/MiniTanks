using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor.SceneManagement;

public class Bullet : MonoBehaviour
{
    public List<EVENT_TYPE> eventsList; 
    private List<GameObject> trajectoryList;

    public GameObject BallPrefab;
    public GameObject SpherePrefab;
    public GameObject TransitCamera;

    public GameObject Player;
    public GameObject Enemy;
    public GameObject bezierpoint;

    GameObject ball;
 
    private float power = 25f;
    private int chances = 3;
    
    private bool NewGame = false;
    private bool NewGameCam = false;
    private bool Moving = false;

  
  
    void Awake()
    {
        CreateBall();
        CreateTrajectory();
        NewGame = true;
        ball.transform.position = new Vector3(Player.transform.position.x, Player.transform.position.y + 2.3f, Player.transform.position.z);

    }
    void CreateTrajectory()
    {
        trajectoryList = new List<GameObject>();

        for (int i = 0; i < 15; i++)
        {
            GameObject SpherePrefabLocal = Instantiate(SpherePrefab);
            trajectoryList.Add(SpherePrefabLocal);

        }

    }
    void CreateBall()
    {
        ball = (GameObject)Instantiate(BallPrefab);

    }
    private void Fire(Vector3 directionVector)
    {
        ball.GetComponent<Rigidbody>().AddForce(directionVector * power, ForceMode.Impulse); 
    }

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

                    OnOffTrajectory(true);

                    break;

                case EVENT_TYPE.MOVED:

                    PlayerTrajectoryPath(ball.transform.position, (Vector3)data * power);


                    break;
                case EVENT_TYPE.ENDED:

                    Fire((Vector3)data);
                    OnOffTrajectory(false);
                    CameraInterpolePlayerToEnemy();



                    break;
            }
        }
    }
    #endregion

    private void Update()
    {
       
        StartingCameraMovement();
      

        MoveWithPlayer1();

        #region PLAYER1_THROWS_OUT_OF_BOUNDS

        if (ball.transform.position.x > 100 || ball.transform.position.y > 50)
        {
            ball.GetComponent<Rigidbody>().isKinematic = true;
            MoveWithPlayer1();
            chances--;
            

            CameraInterpoleEnemyToPlayer();


        }

        if (ball.transform.position.x < -100 || ball.transform.position.y > 50)
        {
            ball.GetComponent<Rigidbody>().isKinematic = true;
            MoveWithPlayer1();
            chances--;


            CameraInterpoleEnemyToPlayer();
            

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
        while (t <= 1f)
        {
            t += Time.deltaTime * (Time.timeScale / 5f);
            Vector3 temp = Vector3.Lerp(startpos, endpos, t);
            TransitCamera.transform.position = new Vector3(temp.x, temp.y, TransitCamera.transform.position.z);
            yield return null;

        }
        Moving = false;

    }
    #endregion CAMERA_PLAYER_CAMERA



    #region TRAJECTORY
    public void PlayerTrajectoryPath(Vector3 startPos, Vector3 pVelocity)
    {
        float vel = Mathf.Sqrt((pVelocity.x * pVelocity.x) + (pVelocity.y * pVelocity.y));

        float angle = Mathf.Rad2Deg * (Mathf.Atan2(pVelocity.y, pVelocity.x));

        float time = 0;

        time += 0.1f;
        for (int i = 0; i < 15; ++i)
        {
            float x = vel * time * Mathf.Cos(angle * Mathf.Deg2Rad);
            float y = vel * time * Mathf.Sin(angle * Mathf.Deg2Rad) - (9.8f * time * time / 2);

            Vector3 pos = new Vector3(startPos.x + x, startPos.y + y, 0);

            trajectoryList[i].transform.position = pos;
            trajectoryList[i].transform.eulerAngles = new Vector3(0, 0, Mathf.Atan2(pVelocity.y - (9.8f) * time, pVelocity.x) * Mathf.Rad2Deg);
            time += 0.1f;
        }
    }
    void EnemyTrajectory()
    {
        Vector3 p1 = Player.transform.position;
        Vector3 p2 = bezierpoint.transform.position;
        Vector3 p3 = Enemy.transform.position;

        for (int i = 1; i < 15 + 1; i++)
        {
            float t = i / (float)15;
            t = Mathf.Clamp01(t);
            Vector3 part1 = Mathf.Pow(1 - t, 2) * p1;
            Vector3 part2 = 2 * (1 - t) * t * p2;
            Vector3 part3 = Mathf.Pow(t, 2) * p3;

            Vector3 pos = part1 + part2 + part3;
            trajectoryList[i - 1].transform.position = pos;
        }

    }
    void OnOffTrajectory(bool value)
    {
        for(int i = 0; i<15;i++)
        {
            trajectoryList[i].SetActive(value);
        }
    }
    #endregion TRAJECTORY


    void MoveWithPlayer1()
    {
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
