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
        
        if(VJHandler.InputDirection!=Vector3.zero)
        {
            this.transform.position += VJHandler.InputDirection * moveSpeed;
            float angle = Mathf.Atan2(VJHandler.InputDirection.y, VJHandler.InputDirection.x) * Mathf.Rad2Deg;
            this.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag=="Enemy")
        {
            Debug.Log("Entered");
            SceneManager.LoadScene("Attack");
        }
        
    }

}
