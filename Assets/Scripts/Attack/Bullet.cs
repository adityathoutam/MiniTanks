using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor.SceneManagement;

public class Bullet : MonoBehaviour
{
    public GameObject winPanel;
    public GameObject lostPanel;
    public GameObject drawPanel;
    public List<EVENT_TYPE> eventsList;
    private List<GameObject> trajectoryList;
    public GameObject Nozzle;
    public GameObject NozzlePoint;
    public GameObject EnemyNozzle;
    public GameObject EnemyNozzleStart;
   

    public GameObject BallPrefab;
    public GameObject BallPrefabEnemy;
    public GameObject SpherePrefab;
    public GameObject TransitCamera;

    public GameObject Player;
    public GameObject Enemy;
    public GameObject bezierpoint;

    GameObject Ball2;
    GameObject ball;

    private float power = 200f;


    private bool NewGame = false;
    private bool NewGameCam = false;
    private bool Moving = false;




    void Awake()
    {
        Time.timeScale = 1f;
        TouchController.disabletouch = false;
        winPanel.SetActive(false);
        lostPanel.SetActive(false);
        drawPanel.SetActive(false);
        CreateBall();
        CreateTrajectory();
        NewGame = true;
        ball.transform.position = NozzlePoint.transform.position;
        ball.GetComponent<Renderer>().enabled = false;
        Ball2.transform.position = EnemyNozzleStart.transform.position;
        Ball2.GetComponent<Renderer>().enabled = false;
        // Ball2.transform.position = new Vector3(Enemy.transform.position.x, Enemy.transform.position.y + 8f, Enemy.transform.position.z);

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
        Ball2 = (GameObject)Instantiate(BallPrefabEnemy);

    }
    private void Fire(Vector3 directionVector)
    {
        ball.GetComponent<Rigidbody>().AddForce(directionVector * power, ForceMode.Impulse);
        ball.GetComponent<Rigidbody>().isKinematic = false;
        ball.GetComponent<Rigidbody>().useGravity = true;
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

                    ball.transform.position = NozzlePoint.transform.position;
                    PlayerTrajectoryPath(ball.transform.position, (Vector3)data * power);

                    break;
                case EVENT_TYPE.ENDED:
                    ball.GetComponent<Renderer>().enabled = true;
                    ball.GetComponent<Rigidbody>().isKinematic = false;
                    TouchController.disabletouch=true;
                    Fire((Vector3)data);
                    OnOffTrajectory(false);
                    CameraInterpolePlayerToEnemy();
                    StartCoroutine(PlayerToEnemy(Player.transform.position, Enemy.transform.position));
                   
                    break;
            }
        }
    }
    #endregion

    private void Update()
    {
        StartingCameraMovement();
        Nozzle.transform.parent = Player.transform; 
         MoveWithPlayer1();

       
       


    }
    public void retrybutton()
    {
        EditorSceneManager.LoadScene("Attack");
    }
    public void Cheat()
    {
        TouchController.disabletouch = false;

    }
    void EnemyTurn()
    {
        
            OnOffTrajectory(true);
            EnemyTrajectory();
        
    }
    void PlayerTurn()
    {
        OnOffTrajectory(true);
        CameraInterpoleEnemyToPlayer();
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
            t += Time.deltaTime * (Time.timeScale / 4f);
            Vector3 temp = Vector3.Lerp(startpos, endpos, t);
            TransitCamera.transform.position = new Vector3(temp.x, temp.y + 25.0f, TransitCamera.transform.position.z);
            yield return null;
        }
        if (CheckColliderEnemy.player1bool == true)
        {
            
            lostPanel.SetActive(true);
            Time.timeScale = 0f;

        }
        if (CheckColliderEnemy.LeftGround == true)
        {
            if (lostPanel.activeInHierarchy == false)
            {
                drawPanel.SetActive(true);
                Time.timeScale = 0f;
            }

        }
        Moving = false;
      
    }
    IEnumerator PlayerToEnemy(Vector3 startpos, Vector3 endpos)
    {
        float t = 0f;
        while (t <= 1f)
        {
            t += Time.deltaTime * (Time.timeScale / 4f);
            Vector3 temp = Vector3.Lerp(startpos, endpos, t);
            TransitCamera.transform.position = new Vector3(temp.x, temp.y + 25.0f, TransitCamera.transform.position.z);
            yield return null;
        }
        if (CheckCollider.player2bool == true)
        {
            winPanel.SetActive(true);
            Time.timeScale = 0f;

        }
        EnemyTurn();

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

            
            Vector3 difference = trajectoryList[0].transform.position- Nozzle.transform.position;
            float rotationX = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;

            Nozzle.transform.rotation = Quaternion.Euler(-rotationX, 90f, 90f);


            time += 0.1f;
        }
    }
    IEnumerator EnemyPath()
    {
        int i=0;
        PlayerTurn();

        while (i<trajectoryList.Count-1)
        {
            Vector3 currentPos = trajectoryList[i].transform.position;
            
            Vector3 nextPos = trajectoryList[i+1].transform.position;

            Ball2.GetComponent<Rigidbody>().isKinematic = false;
            Ball2.GetComponent<Rigidbody>().useGravity = true;

            
            Vector3 startpos = Ball2.transform.position;

           

            if (Vector3.Distance(Ball2.transform.position, nextPos) > 9f)
            {
                Ball2.GetComponent<Renderer>().enabled = true;
                Ball2.GetComponent<Rigidbody>().velocity = (nextPos - currentPos)*5f;
            }
            else
            {
                i++;
            }
            

            yield return null;
        }
      


    }
   

    void EnemyTrajectory()
    {
        Vector3 p1 = Enemy.transform.position;
        Vector3 p2r = bezierpoint.transform.position;
        Vector3 p3r = Player.transform.position;


        float p2randry = Random.Range(15, 60);
        float p3randrx = Random.Range(-15, 0);

        Vector3 p2 = new Vector3(p2r.x, p2r.y + p2randry, p2r.z);
        Vector3 p3r1 = new Vector3(p3r.x + p3randrx, p3r.y, p3r.z);
        Vector3 p3 = new Vector3(p3r.x + p3randrx, p3r.y, p3r.z);



        for (int i = 1; i < 15 + 1; i++)
        {
            float t = i / (float)15;
            t = Mathf.Clamp01(t);
            Vector3 part1 = Mathf.Pow(1 - t, 2) * p1;
            Vector3 part2 = 2 * (1 - t) * t * p2;
            Vector3 part3 = Mathf.Pow(t, 2) * p3;
            Vector3 part4 = Mathf.Pow(t, 2) * p3r1;

            float randpos = Random.Range(0, 1);

            if (randpos == 0)
            {
                Vector3 pos = part1 + part2 + part3;
                trajectoryList[i - 1].transform.position = pos;
            }
            if (randpos == 1)
            {
                Vector3 pos1 = part1 + part2 + part4;
                trajectoryList[i - 1].transform.position = pos1;
            }
            
        }
        Vector3 difference = EnemyNozzle.transform.position - trajectoryList[1].transform.position;
        float rotationX = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;

        EnemyNozzle.transform.rotation = Quaternion.Euler(0f, 0f, rotationX);
        StartCoroutine(EnemyPath());

    }
    void OnOffTrajectory(bool value)
    {
        for (int i = 0; i < 15; i++)
        {
            trajectoryList[i].GetComponent<Renderer>().enabled=(value);
        }
    }
    #endregion TRAJECTORY


    void MoveWithPlayer1()
    {
        ball.transform.parent = Player.transform;
        if (Vector3.Distance(ball.transform.position, Player.transform.position) > 8)
        {
            ball.transform.parent = null;
        }
    }
    void StartingCameraMovement()
    {
        if (NewGame == true)
        {
            CameraInterpolePlayerToEnemy();
            NewGameCam = true;
            NewGame = false;
            

        }
        if (NewGameCam == true && Moving == false)
        {
            
            CameraInterpoleEnemyToPlayer();
            Moving = true;
            NewGameCam = false;
            NewGame = false;

        }
    }

}
