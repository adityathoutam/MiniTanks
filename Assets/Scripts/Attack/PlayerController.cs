using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public GameObject PL1;
    public GameObject PL2;
   
    // Use this for initialization

    // Update is called once per frame
    void Update()
    {
       

            if (Input.GetKey(KeyCode.A))
            {
                PL1.transform.Translate(Vector3.left * 100 * Time.deltaTime);

            }
            if (Input.GetKey(KeyCode.D))
            {
                PL1.transform.Translate(Vector3.right * 100 * Time.deltaTime);

            }
        

            if (Input.GetKey(KeyCode.LeftArrow))
            {
                PL2.transform.Translate(Vector3.left * 100 * Time.deltaTime);

            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                PL2.transform.Translate(Vector3.right * 100 * Time.deltaTime);

            }
       
        


#if UNITY_ANDROID
        //player1
       
            if (Input.touchCount > 0)
            {
                Vector2 startpos = Input.GetTouch(0).position;

                //Check if it is left or right
                if (startpos.x < Screen.width / 2.0)
                {

                    PL1.transform.Translate(Vector3.left * 10 * Time.deltaTime);
                }
                else if (startpos.x > Screen.width / 2.0)
                {
                    PL1.transform.Translate(Vector3.right * 10 * Time.deltaTime);
                }

            }
       
          
                if (Input.touchCount > 0)
                {
                    Vector2 startpos = Input.GetTouch(0).position;

                    //Check if it is left or right?
                    if (startpos.x < Screen.width / 2.0)
                    {

                        PL2.transform.Translate(Vector3.left * 10 * Time.deltaTime);
                    }
                    else if (startpos.x > Screen.width / 2.0)
                    {
                        PL2.transform.Translate(Vector3.right * 10 * Time.deltaTime);
                    }
                }

            
#endif
    }
    }