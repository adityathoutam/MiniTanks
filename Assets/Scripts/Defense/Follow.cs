﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Follow : MonoBehaviour
{
    Vector3 offset;
    public GameObject Player;
    void Start()
    {
        Camera.main.orthographicSize = 148;
        offset = transform.position - Player.transform.position;
    }
    void Update()
    {
        transform.position = offset + Player.transform.position;
    }
}
