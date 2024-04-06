using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveInstruction2 : MonoBehaviour
{

    public GameObject golfCart;
    public GameObject instruction3;

    void Update()
    {
        if (golfCart.transform.position.z > transform.position.z)
        {
            gameObject.SetActive(false);
            instruction3.SetActive(true);
        }
    }
}
