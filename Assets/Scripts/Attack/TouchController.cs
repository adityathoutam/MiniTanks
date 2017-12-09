using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchController : MonoBehaviour
{
    private Vector3 startPos;

    private Vector3 currentPosition;

    private Vector3 directionVector;
    private Vector3 FinaldirectionVector;
    public static bool PlayerReadyToShoot = false; 

	void Update ()
    {

        if (PlayerReadyToShoot == true)
            UserInput();
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
}
