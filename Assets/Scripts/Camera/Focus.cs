using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Focus : MonoBehaviour {

    public Transform Map;
    public Transform L1;
    public Transform L2;
    public Transform L3;
    public Transform L4;
    public Transform L5;
    Vector3 offset;
   

    void Start () {

       //offset = Map.position - transform.position;
	}

    private void Update()
    {
        if(Input.GetKeyUp(KeyCode.E))
        {
            StartCoroutine(Focusing(transform.position,L2.transform.position));
        }

        if (Input.GetKeyUp(KeyCode.D))
        {
            StartCoroutine(Focusing(transform.position, L3.transform.position));
        }

        if (Input.GetKeyUp(KeyCode.S))
        {
            StartCoroutine(Focusing(transform.position, L4.transform.position));
        }

        if (Input.GetKeyUp(KeyCode.A))
        {
            StartCoroutine(Focusing(transform.position, L5.transform.position));
        }

        if (Input.GetKeyUp(KeyCode.W))
        {
            StartCoroutine(Focusing(transform.position, L1.transform.position));
        }
    }


    IEnumerator Focusing(Vector3 start,Vector3 target)
    {
        float t = 0;
        while(t<=1.5)
        {
            t += Time.deltaTime * (Time.timeScale / 0.5f);
            Vector3 temp = Vector3.Lerp(start, target, t);
            transform.position = new Vector3(temp.x, temp.y, transform.position.z);
            yield return null;
            
        }
        
    }
}
