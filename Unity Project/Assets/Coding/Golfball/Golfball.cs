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
        adjustVelocity();
        if (shouldRemoveGolfball())
        {
            removeGolfball();
        }
    }

    private void adjustVelocity()
    {
        float velocityX = Math.Abs(rigidbody.velocity.x) <= 0.07f ? 0 : rigidbody.velocity.x;
        float velocityY = Math.Abs(rigidbody.velocity.y) <= 0.07f ? 0 : rigidbody.velocity.y;
        float velocityZ = Math.Abs(rigidbody.velocity.z) <= 0.07f ? 0 : rigidbody.velocity.z;
        rigidbody.velocity = new Vector3(velocityX, velocityY, velocityZ);
    }

    private bool shouldRemoveGolfball()
    {
        if (rigidbody.velocity == new Vector3(0, 0, 0))
        {
            timeIdle += Time.deltaTime;
        }
        else
        {
            timeIdle = 0;
        }
        return timeIdle >= maxTimeIdle;
    }

    private void removeGolfball() {
        Destroy(gameObject);
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Ground")
        {
            //rigidbody.velocity = new Vector3(rigidbody.velocity.x <= 0.02f ? 0 : rigidbody.velocity.x / 2f,
              //  rigidbody.velocity.y, rigidbody.velocity.z);
        }
    }

    void OnCollisionStay(Collision other)
    {
        if (other.gameObject.tag == "Ground")
        {
            rigidbody.velocity = new Vector3(rigidbody.velocity.x * 0.95f,
                rigidbody.velocity.y * 0.95f, rigidbody.velocity.z * 0.95f);
        }
    }
}
