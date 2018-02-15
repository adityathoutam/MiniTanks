using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Player : MonoBehaviour
{
    public static bool Brownleft = false;
    public static bool Pinkleft = false;
    public static bool Blueleft = false;
    public static bool Orangeleft = false;


    public float moveSpeed = 25f;

    public GameObject Coin;

    public Transform[] Spawnpoints;

    Rigidbody2D rb;

    

    void Start()
    {
        Instantiate(Coin);
        Instantiate(Coin);
        
        
        rb = this.GetComponent<Rigidbody2D>();
    }
   
    void Update()
    {
        this.transform.position += VJHandler.InputDirection * moveSpeed;
        rb.velocity = VJHandler.InputDirection*moveSpeed;
        float angle = Mathf.Atan2(VJHandler.InputDirection.y, VJHandler.InputDirection.x) * Mathf.Rad2Deg;
        this.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag=="Bitcoin")
        {
            int spawnPointIndex = Random.Range(0, Spawnpoints.Length);
            collision.gameObject.transform.position = Spawnpoints[spawnPointIndex].position;
        }
    }

   

    void TransformCoin()
    {
       
        int spawnPointIndex = Random.Range(0, Spawnpoints.Length);
        Coin.transform.position = Spawnpoints[spawnPointIndex].position;
    }

    

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("BrownDoor"))
        {
            Brownleft = true;
        }
        if (collision.gameObject.CompareTag("PinkDoor"))
        {
            Pinkleft = true;
        }
        if (collision.gameObject.CompareTag("BlueDoor"))
        {
            Blueleft = true;
        }
        if (collision.gameObject.CompareTag("OrangeDoor"))
        {
            Orangeleft = true;
        }
    }

}
