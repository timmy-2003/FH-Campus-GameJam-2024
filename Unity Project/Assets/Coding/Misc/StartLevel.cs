using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartLevel : MonoBehaviour
{

    public GameObject golfCart;

    void Update()
    {
        if (IsInArea())
        {
            SceneManager.LoadScene("moTest");
        }
        else
        {
            //Debug.Log("Out");
        }
    }

    private bool IsInArea()
    {
        float positionX = transform.position.x;
        float positionZ = transform.position.z;

        float cartPositionX = golfCart.transform.position.x;
        float cartPositionZ = golfCart.transform.position.z;
        return cartPositionZ + 10 > positionZ && cartPositionZ - 10 < positionZ
            && cartPositionX + 10 > positionX && cartPositionX - 10 < positionX;
    }
}
