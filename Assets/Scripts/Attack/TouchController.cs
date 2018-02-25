using UnityEngine;
public class TouchController : MonoBehaviour
{
    private Vector3 startPos;
    private Vector3 currentPosition;
    private Vector3 directionVector;
    private Vector3 FinaldirectionVector;
    private float speed = 5f;
    public GameObject PL1;
    public static bool disabletouch = false;
    void Update()
    {
        UserInput(disabletouch);
    }
    void UserInput(bool msg)
    {
        if (msg == false)
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100))
            {
                if (hit.collider.tag == "player1bool")
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
                }
                else
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        LeftButton(true);
                        RightButton(true);
                    }
                }
            }
        }
    }
    public void RightButton(bool msg)
    {
        if (msg != false)
        {
            if (Input.GetMouseButton(0) && Input.mousePosition.x > Screen.width / 2)
            {
                if (PL1.transform.position.x < -20f)
                    PL1.transform.Translate(new Vector3(0f, 0f, speed));
            }
        }
    }
    public void LeftButton(bool msg)
    {
        if (msg != false)
        {
            if (Input.GetMouseButton(0) && Input.mousePosition.x < Screen.width / 2)
            {
                if (PL1.transform.position.x > -85f)
                    PL1.transform.Translate(new Vector3(0f, 0f, -speed));
            }
        }
    }
}
