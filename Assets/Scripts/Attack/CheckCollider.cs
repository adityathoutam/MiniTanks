using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CheckCollider : MonoBehaviour
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
        if (collision.gameObject.tag == "player2bool")
        {
            player2bool = true;
        }
    }
}
