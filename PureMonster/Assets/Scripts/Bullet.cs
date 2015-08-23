﻿using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour
{
    bool alive = true;

    Vector3 speed;

    public Vector3 Speed
    {
        get { return speed; }
        set
        {
            distanceForStep = value.magnitude;
            speed = value;
        }
    }
    float distanceForStep;
    float distance;
    const float distanceMax = 2.0f;


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position += speed;
        distance += distanceForStep;
        if (distance > distanceMax) DestroySelf();
    }


    void DestroySelf()
    {
        alive = false;
        Destroy(gameObject);
    }
}