using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Golfball : MonoBehaviour
{

    private float xVelocity;
    private float timeSpentIdling = 0;
    private float timeDisappearWhenIdle = 3;
    private bool moveRight;

    void Start()
    {
        xVelocity = Random.Range(0.02f, 0.05f);
        if (Random.Range(1, 100) <= 50)
        {
            moveRight = true;
        }
        else
        {
            moveRight = false;
        }
    }

    void Update()
    {
        if (xVelocity != 0)
        {
            move();
        }
        else
        {
            remove();
        }
        
    }

    private void move()
    {
        float positionX = moveRight ? transform.position.x + xVelocity : transform.position.x - xVelocity;
        transform.position = new Vector3(positionX, transform.position.y, transform.position.z);
        if (xVelocity < 0.002f)
        {
            xVelocity = 0;
        }
    }

    private void remove() {
        Destroy(gameObject, 3);
    }

    void OnCollisionEnter(Collision other) {
        if (other.gameObject.tag == "Ground")
        {
            xVelocity /= 2;
        }
    }
}
