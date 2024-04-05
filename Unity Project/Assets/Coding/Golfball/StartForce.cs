using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartForce : MonoBehaviour
{

    void Start()
    {
        Rigidbody rigidBody = GetComponent<Rigidbody>();
        rigidBody.AddForce(new Vector3(getStartForce(), 0, 0), ForceMode.Impulse);
    }

    private int getStartForce()
    {
        int startForce = Random.Range(3, 10);
        return Random.Range(1, 100) <= 50 ? startForce : startForce * -1;
    }

    void Update()
    {
        
    }
}
