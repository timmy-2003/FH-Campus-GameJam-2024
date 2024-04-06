using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveInstruction3 : MonoBehaviour
{

    public GameObject golfCart;

    void Update()
    {
        if (golfCart.transform.position.z > transform.position.z)
        {
            gameObject.SetActive(false);
        }
    }
}
