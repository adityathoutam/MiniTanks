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
   

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "player1bool")
        {
            player1bool = true;
        }
        if (other.gameObject.tag == "LeftGround")
        {
            
            LeftGround = true;
        }
        
    }
}
