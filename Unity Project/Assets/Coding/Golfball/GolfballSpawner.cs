using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolfballSpawner : MonoBehaviour
{

    public GameObject golfCart;
    public GameObject golfball;
    public float spawnRate;
    private float time = 0;

    void Update()
    {
        time += Time.deltaTime;
        if (time >= spawnRate)
        {
            float cartPositionX = golfCart.transform.position.x;
            float cartPositionY = golfCart.transform.position.y;
            float cartPositionZ = golfCart.transform.position.z;
            Vector3 spawnPosition = new Vector3(Random.Range(cartPositionX - 10f, cartPositionX + 10f),
                cartPositionY + Random.Range(5f, 10f), Random.Range(cartPositionZ - 10f, cartPositionZ + 10f));
            Instantiate(golfball, spawnPosition, Quaternion.identity);

            time = 0;
        }
    }
}
