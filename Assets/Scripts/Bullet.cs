using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor.SceneManagement;

public class Bullet : MonoBehaviour
{
    public GameObject OutOfBoundsPanel;
    public GameObject WinPanel;

    public static bool PLAYER1TURN;
    public static bool PLAYER2TURN;

    public GameObject player1;
    public GameObject player2;

    public GameObject BallPrefab;
    public GameObject ball;


    private float power = 25f;
    private int numberOfPoints = 30;
    public float transitionDuration = 0.5f;

    public GameObject camera1;
    public GameObject trajectoryPointPrefab;

    public float angle;
    public Text angleText;
    public float velocity;
    public Text velocityText;

    Vector3 P2chance;
    Vector3 P1chance;

    public List<EVENT_TYPE> eventsList;

    private List<GameObject> trajectoryList;

    private float actualTime = 0f;
    private float distance = 0f;
    private bool AmIPlayer1;

    void Awake()
    {
        CreateBall();
        DisplayTrajectory();

        PLAYER1TURN = true;
        PLAYER2TURN = false;
    }
    #region BALL
    private void CreateBall()
    {
        ball = (GameObject)Instantiate(BallPrefab);

    }
    private void Fire(Vector3 directionVector)
    {
        ball.GetComponent<Rigidbody>().AddForce(directionVector * power, ForceMode.Impulse);
       // Debug.Log("fired");
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
                    Angle((Vector3)data);

                    break;
            }
        }
    }
    #endregion

    public void Update()
    {
        P2chance = player2.transform.position;
        P2chance.y = player2.transform.position.y + 2.3f;

        P1chance = player1.transform.position;
        P1chance.y = player1.transform.position.y + 2.3f;

        Distance();
        MoveWithPlayer1();

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

        #region Ground
       
            if (CheckCollider.LeftGround == true)
            {
            Debug.Log("yoooooo");

                ball.transform.position = P1chance;
                // Velocity();
                CameraInterpole(AmIPlayer1);
                MoveWithPlayer2();

            }
        
       

            if (CheckCollider.RightGround == true)
            {

            Debug.Log("yoooooo");

            ball.transform.position = P2chance;
                //Velocity();
                CameraInterpole(!AmIPlayer1);
                MoveWithPlayer1();

            }
            
        
        #endregion Ground        

        #region PLAYERTURN
        if (PLAYER2TURN == true)
        {
            
            if (CheckCollider.player1bool == true)
            {
                Debug.Log("chutiyapa");
                ball.transform.position = P1chance;

                MoveWithPlayer2();
            }
            
        }
        //PLAYER2TURN = false;
        if (PLAYER1TURN == true)
        {
            
            if (CheckCollider.player2bool == true)
            {
                Debug.Log("chutiyapa");
                
                ball.transform.position = P2chance;
               

                MoveWithPlayer1();
            }
            
            //  PLAYER1TURN = false;
        }
        
        #endregion PLAYERTURN

    }
    #region Reset
    public void Reset()
    {
        EditorSceneManager.LoadScene("Game");
    }
    #endregion Reset

    #region CAMERA_PLAYER_CAMERA

    void CameraInterpole(bool AmIPlayer1)
    {
        if (!AmIPlayer1)
        {
            StartCoroutine(Transition(player1.transform.position, player2.transform.position));
        }
        if (AmIPlayer1)
        {
            StartCoroutine(Transition(player2.transform.position, player1.transform.position));
        }
    }
    IEnumerator Transition(Vector3 startpos, Vector3 endpos)
    {

        float t = 0f;
        while (t <= 1.5f)
        {
            t += Time.deltaTime * (Time.timeScale / transitionDuration);
            Vector3 temp = Vector3.Lerp(startpos, endpos, t);
            camera1.transform.position = new Vector3(temp.x, temp.y, camera1.transform.position.z);

            yield return null;
            actualTime += Time.deltaTime;
        }

       // Debug.Log("STOPPED");
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

    #region Velocity_Angle_Distance_Calculation

    void Velocity()
    {
        velocity = distance / actualTime;

        velocityText.text = "Velocity: " + velocity.ToString();
    }
    void Angle(Vector3 pVelocity)
    {
        angle = Mathf.Rad2Deg * (Mathf.Atan2(pVelocity.y, pVelocity.x));
        angleText.text = "Angle: " + angle.ToString();
    }
    void Distance()
    {
        distance = player2.transform.position.x - player1.transform.position.x;
    }

    #endregion

    #region MoveWithPlayers
    void MoveWithPlayer1()
    {
        ball.transform.parent = player1.transform;
        if (Vector3.Distance(ball.transform.position, player1.transform.position) > 5)
        {
            ball.transform.parent = null;

            CameraInterpole(AmIPlayer1);
        }
    }
    void MoveWithPlayer2()
    {
        ball.transform.parent = player2.transform;
        if (Vector3.Distance(ball.transform.position, player2.transform.position) > 5)
        {
            ball.transform.parent = null;
            CameraInterpole(!AmIPlayer1);
        }
    }
    #endregion

}
