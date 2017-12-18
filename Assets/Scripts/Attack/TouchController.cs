using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchController : MonoBehaviour
{
    private Vector3 startPos;
    private Vector3 currentPosition;
    private Vector3 directionVector;
    private Vector3 FinaldirectionVector;

    public GameObject PL1;
    public static bool PlayerReadyToShoot = false;
    public static bool ReadyToShoot = false;
    private int touchcount = 0;

	void Update ()
    {

        if (PlayerReadyToShoot == true)
        {
            
           lol();
           if(ReadyToShoot==true)
            UserInput();
        }

    }
    public void ReadyToShootFunction()
    {
        ReadyToShoot = true;
    }
    void UserInput()
    {

        if (Input.GetMouseButtonDown(0))
        {
            startPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            EventRelay.RaiseEvent(EVENT_TYPE.BEGAN, startPos);
        }
        if (Input.GetMouseButton(0))
        {
            currentPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            directionVector = startPos - currentPosition;

            EventRelay.RaiseEvent(EVENT_TYPE.MOVED, directionVector);
        }
        if (Input.GetMouseButtonUp(0))
        {
            FinaldirectionVector = startPos - currentPosition;

            EventRelay.RaiseEvent(EVENT_TYPE.ENDED, FinaldirectionVector);
            ReadyToShoot = false;
        }
       
        

#if UNITY_ANDROID
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    startPos = Camera.main.ScreenToWorldPoint(touch.position);
                    
                    EventRelay.RaiseEvent(EVENT_TYPE.BEGAN,startPos);

                    break;

                case TouchPhase.Moved:
                    currentPosition = Camera.main.ScreenToWorldPoint(touch.position);

                    directionVector = startPos - currentPosition;

                    EventRelay.RaiseEvent(EVENT_TYPE.MOVED, directionVector);        
                    
                    break;
                
                case TouchPhase.Ended:
                    directionVector = startPos - currentPosition;

                    EventRelay.RaiseEvent(EVENT_TYPE.ENDED, directionVector);

                    break;
            }

        }
#endif
    }
    void lol()
    {
        if (Input.GetMouseButton(0))
        {
            print(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            var wantedPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            Vector3 newPos = new Vector3(Mathf.Clamp(wantedPos.x, -80f, -10f), 4f, 0f);

            PL1.transform.position = newPos;
        }
    }
}
