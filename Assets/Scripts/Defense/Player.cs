using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Player : MonoBehaviour
{


    public GameObject endingPanel;
    public Text Score;
    int Scorecount=0;


    public float moveSpeed = 25f;

    public GameObject Coin;

    public Transform[] Spawnpoints;
    public GameObject SpawnpointsParent;

    Rigidbody2D rb;

    public GameObject Passage1;
    public GameObject Passage2;
    public GameObject Passage3;
    public GameObject Passage4;

    int count = 0;


    void Start()
    {
        endingPanel.SetActive(false);
        Instantiate(Coin);
        Instantiate(Coin);
        Coin.transform.position = new Vector3(0, 0, 0);

        DontDestroyOnLoad(Score);


        this.transform.position = new Vector3(50, 100, 0);
        rb = this.GetComponent<Rigidbody2D>();
    }

    void Update()
    {

        this.transform.position += VJHandler.InputDirection * moveSpeed;

        rb.velocity = VJHandler.InputDirection * moveSpeed;
        float angle = Mathf.Atan2(VJHandler.InputDirection.y, VJHandler.InputDirection.x) * Mathf.Rad2Deg;
        this.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        ChangeLevel();
    }
    void ChangeLevel()
    {


        if (MainManager.Completed_1)
        {
            Instantiate(Coin);
            Coin.transform.position = new Vector3(550, 30, 0);
            //BROWN
            MainManager.Completed_1 = false;
            Passage1.GetComponent<BoxCollider2D>().enabled = false;

            SpawnpointsParent.transform.position = new Vector3(550, 30, 0);
            Instantiate(Coin);

        }
        if (MainManager.Completed_2)
        {
            Instantiate(Coin);
            Coin.transform.position = new Vector3(627, -109, 0);
            //PINK
            MainManager.Completed_2 = false;
            this.transform.position = new Vector3(550, 30, 0);
            Passage2.GetComponent<BoxCollider2D>().enabled = false;

            SpawnpointsParent.transform.position = new Vector3(627, -109, 0);
            Instantiate(Coin);
        }
        if (MainManager.Completed_3)
        {
            Instantiate(Coin);
            Coin.transform.position = new Vector3(580, -280, 0);
            //BLUE
            MainManager.Completed_3 = false;
            this.transform.position = new Vector3(627, -109, 0);
            Passage3.GetComponent<BoxCollider2D>().enabled = false;

            SpawnpointsParent.transform.position = new Vector3(580, -280, 0);
            Instantiate(Coin);

        }
        if (MainManager.Completed_4)
        {
            Instantiate(Coin);
            Coin.transform.position = new Vector3(-124, -290, 0);
            //ORANGE
            MainManager.Completed_4 = false;
            this.transform.position = new Vector3(580, -280, 0);
            Passage4.GetComponent<BoxCollider2D>().enabled = false;

            SpawnpointsParent.transform.position = new Vector3(-124, -290, 0);
            Instantiate(Coin);
        }
        if (MainManager.Completed_5)
        {
            MainManager.Completed_5 = false;
            endingPanel.SetActive(true);
        }

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bitcoin")
        {
            Scorecount += 1;
            Score.text = "" + Scorecount;
            int spawnPointIndex = Random.Range(0, Spawnpoints.Length);
            collision.gameObject.transform.position = Spawnpoints[spawnPointIndex].position;
        }
        if (collision.gameObject.tag == "TEnemy")
        {
            
            SceneManager.LoadScene("Attack");
        }
        if (collision.gameObject.tag == "CEnemy")
        {
           
            SceneManager.LoadScene("Attack");
        }
    }



    void TransformCoin()
    {

        int spawnPointIndex = Random.Range(0, Spawnpoints.Length);
        Coin.transform.position = Spawnpoints[spawnPointIndex].position;
    }


}
