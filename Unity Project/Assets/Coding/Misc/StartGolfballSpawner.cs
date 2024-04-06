using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGolfballSpawner : MonoBehaviour
{

    public GameObject golfCart;
    public GameObject golfballSpawner;
    public GameObject instruction2;
    private bool nextInstructionEnabled = false;

    void Update()
    {
        if (golfCart.transform.position.z > transform.position.z && !nextInstructionEnabled)
        {
            golfballSpawner.SetActive(true);
            gameObject.SetActive(false);
            instruction2.SetActive(true);
        }
    }
}
