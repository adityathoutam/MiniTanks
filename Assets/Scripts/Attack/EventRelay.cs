using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EventRelay : MonoBehaviour
{
    public delegate void My_delegate(EVENT_TYPE type, System.Object data);
    public static event My_delegate OnEvent;
    public static void RaiseEvent(EVENT_TYPE type, System.Object data = null)
    {
        if (OnEvent != null)
        {
            OnEvent(type, data);
        }
    }
}
