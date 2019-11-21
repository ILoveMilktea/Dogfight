﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTest : MonoBehaviour
{
    Vector3 prevPlayerPos;
    Vector3 distance = new Vector3(0, 13, -15);
    public Transform player;

    // Start is called before the first frame update
    void Start()
    {
        prevPlayerPos = player.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 v = player.transform.position - prevPlayerPos;
        v += distance;
        transform.position = Vector3.Lerp(transform.position, v, Time.deltaTime * 3);   
    }
}