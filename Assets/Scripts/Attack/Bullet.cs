using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;
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
    public GameObject player;
    public GameObject Enemy;
    public GameObject bezierpoint;
    

    GameObject EnemyBullet;
    GameObject PlayerBullet;

    private float power = 200f;
    private bool NewGame = false;
    private bool PlayerFired = false;
    private bool EnemyFired = false;



    Vector3[] EnemyToPlayer;
    Vector3[] PlayerToEnemy;
    Vector3[] StartingCamera;

    void Awake()
    {
        CreateArrayofTweens();

        Time.timeScale = 1f;
        TouchController.disabletouch = false;

        winPanel.SetActive(false);
        lostPanel.SetActive(false);
        drawPanel.SetActive(false);
        CreateBall();
        CreateTrajectory();

        NewGame = true;
        Nozzle.transform.parent = player.transform;
        PlayerBullet.transform.position = NozzlePoint.transform.position;
        PlayerBullet.GetComponent<Renderer>().enabled = false;
        EnemyBullet.transform.position = EnemyNozzleStart.transform.position;
        EnemyBullet.GetComponent<Renderer>().enabled = false;

    }
    void CreateArrayofTweens()
    {
        Vector3 EnemyPos = new Vector3(Enemy.transform.position.x, TransitCamera.transform.position.y, TransitCamera.transform.position.z);
        Vector3 PlayerPos = new Vector3(player.transform.position.x, TransitCamera.transform.position.y, TransitCamera.transform.position.z);

        EnemyToPlayer = new Vector3[2];
        EnemyToPlayer[0] = EnemyPos;
        EnemyToPlayer[1] = PlayerPos;

        PlayerToEnemy = new Vector3[2];
        PlayerToEnemy[0] = PlayerPos;
        PlayerToEnemy[1] = EnemyPos;

        StartingCamera = new Vector3[4];
        StartingCamera[0] = PlayerPos;
        StartingCamera[1] = EnemyPos;
        StartingCamera[2] = EnemyPos;
        StartingCamera[3] = PlayerPos;
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
        PlayerBullet = (GameObject)Instantiate(BallPrefab);
        EnemyBullet = (GameObject)Instantiate(BallPrefabEnemy);
    }
    private void Fire(Vector3 directionVector)
    {

        Vector3[] PlayerPathPos = new Vector3[trajectoryList.Count];
        for (int j = 0; j < trajectoryList.Count; j++)
        {
            PlayerPathPos[j] = trajectoryList[j].transform.position;
        }
        PlayerBullet.GetComponent<Renderer>().enabled = true;
        PlayerBullet.GetComponent<Rigidbody>().useGravity = true;
        PlayerBullet.GetComponent<Rigidbody>().AddForce(directionVector * power, ForceMode.Impulse);
       

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
                    PlayerBullet.transform.position = NozzlePoint.transform.position;
                    PlayerTrajectoryPath(PlayerBullet.transform.position, (Vector3)data * power);
                    break;
                case EVENT_TYPE.ENDED:

                    PlayerBullet.GetComponent<Renderer>().enabled = true;
                    PlayerBullet.GetComponent<Rigidbody>().isKinematic = false;

                    TouchController.disabletouch = true;

                    Fire((Vector3)data);
                    OnOffTrajectory(false);
                    PlayerFired = true;
                    TransitCamera.transform.DOPath(PlayerToEnemy, 10f, PathType.Linear, PathMode.Full3D);
                    break;
            }


        }
    }
    #endregion

     void Update()
    {
        BulletAttachToTank();
        StartingCameraMovement();

        if(PlayerFired == true && TransitCamera.transform.position.x>= Enemy.transform.position.x)
        {
            if (CheckCollider.player2bool == false)
                EnemyTrajectoryPath();
            else
            {
                MainManager.isLevel2Success = true;
                winPanel.SetActive(true);
                SceneManager.LoadScene("MainScene");
            }
        } 
        if(Input.GetKey(KeyCode.A))
        {
            MainManager.isLevel2Success = true;
            winPanel.SetActive(true);
            SceneManager.LoadScene("MainScene");

        }
        if(EnemyFired==true && TransitCamera.transform.position.x<=player.transform.position.x)
        {
           
            if (CheckColliderEnemy.player1bool == true)
            {
                
                lostPanel.SetActive(true);
                Time.timeScale = 0f;
                
            }
            if (CheckColliderEnemy.LeftGround == true)
            {
                if(!lostPanel.activeInHierarchy)
                drawPanel.SetActive(true);
            }
        }
        if (CheckColliderEnemy.LeftGround||CheckColliderEnemy.player1bool)
        {
            Destroy(EnemyBullet);
        }
        if(CheckCollider.RightGround||CheckCollider.LeftGround||CheckCollider.player2bool)
        {
            Destroy(PlayerBullet);
        }

    }
    public void RetryButton()
    {
        SceneManager.LoadScene("Attack");
    }
    public void Cheat()
    {
        TouchController.disabletouch = false;
    }
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
            Vector3 difference = trajectoryList[0].transform.position - Nozzle.transform.position;
            float rotationX = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
            Nozzle.transform.rotation = Quaternion.Euler(-rotationX, 90f, 90f);
            time += 0.1f;
        }
    }
    void EnemyTrajectoryPath()
    {
        Vector3 p1 = Enemy.transform.position;
        Vector3 p2r = bezierpoint.transform.position;
        Vector3 p3r = player.transform.position;
        float p2randry = Random.Range(15, 45);
        float p3randrx = Random.Range(-10, 10);
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

        Vector3[] EnemyPathPos = new Vector3[trajectoryList.Count];
        for (int j = 0; j < trajectoryList.Count; j++)
        {
            EnemyPathPos[j] = trajectoryList[j].transform.position;
        }
        OnOffTrajectory(true);
        EnemyFired = true;
        EnemyBullet.GetComponent<Renderer>().enabled = true;
        EnemyBullet.transform.DOPath(EnemyPathPos, 10f, PathType.Linear, PathMode.Full3D);
        TransitCamera.transform.DOPath(EnemyToPlayer, 10f, PathType.Linear, PathMode.Full3D);
    }
    void OnOffTrajectory(bool value)
    {
        for (int i = 0; i < 15; i++)
        {
            trajectoryList[i].GetComponent<Renderer>().enabled = (value);
        }
    }
    void BulletAttachToTank()
    {  if (PlayerBullet != null)
        {
            PlayerBullet.transform.parent = player.transform;
            if (Vector3.Distance(PlayerBullet.transform.position, player.transform.position) > 8)
            {
                PlayerBullet.transform.parent = null;
            }
        }
    }
    void StartingCameraMovement()
    {
        if (NewGame == true)
        {
            TransitCamera.transform.DOPath(StartingCamera, 10f, PathType.Linear, PathMode.Full3D);
            NewGame = false;
        }
    }
}
