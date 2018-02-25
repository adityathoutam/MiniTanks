using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CheckColliderEnemy : MonoBehaviour
{
    public static bool LeftGround;
    public static bool RightGround;
    public static bool player1bool;
    public static bool player2bool;
    void Start()
    {
        LeftGround = false;
        RightGround = false;
        player1bool = false;
        player2bool = false;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "player1bool")
        {
            Debug.Log("HIT");
            player1bool = true;
        }
        if (collision.gameObject.tag == "LeftGround")
        {
            LeftGround = true;
        }
    }
}
