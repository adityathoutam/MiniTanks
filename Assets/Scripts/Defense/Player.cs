using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Player : MonoBehaviour
{
    [SerializeField]
    private Animation transport;
  

    public float moveSpeed = 25f;
    public AnimationClip move;

    void Start()
    {
        transport = GetComponent<Animation>();
        this.transform.position += VJHandler.InputDirection * moveSpeed;
    }
   
    void Update()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            transport.Play(move.name);
        }
        if(VJHandler.InputDirection!=Vector3.zero)
        {
            this.transform.position += VJHandler.InputDirection * moveSpeed;
            float angle = Mathf.Atan2(VJHandler.InputDirection.y, VJHandler.InputDirection.x) * Mathf.Rad2Deg;
            this.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
        if(this.transform.position == new Vector3(232,80,0))
        {
            this.transform.position = new Vector3(240, 80, 0);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Entered");
        //if(collision.gameObject.tag=="Enemy")
        //{
        //    Debug.Log("Entered");
        //    SceneManager.LoadScene("Attack");
        //}

    }

}
