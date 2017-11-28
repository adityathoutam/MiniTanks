using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Player : MonoBehaviour
{


    public float moveSpeed = 25f;
  
   
     void Start()
    {
       
        this.transform.position += VJHandler.InputDirection * moveSpeed;
    }
   
    void Update()
    {
        this.transform.position += VJHandler.InputDirection * moveSpeed;
        

    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag=="Enemy")
        {

            SceneManager.LoadScene("Attack");

           
        }
        
    }

}
