using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Golfball : MonoBehaviour
{

    private float timeIdle = 0;
    private float maxTimeIdle = 3;
    private Vector3 position;
    private Rigidbody rigidbody;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Ground")
        {
            Destroy(gameObject, 6);
        }
    }
}
