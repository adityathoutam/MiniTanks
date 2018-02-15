using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionScript : MonoBehaviour {


    
    public GameObject Player;

    public GameObject Enemy1;
    public GameObject Enemy2;

    void Start () {

	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Debug.Log("gg");

            Enemy1.SetActive(false);
            Enemy2.SetActive(false);

        }
    }

    void Update () {
		
	}
}
