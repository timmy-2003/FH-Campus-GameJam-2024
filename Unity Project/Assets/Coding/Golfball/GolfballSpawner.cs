using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolfballSpawner : MonoBehaviour
{

    public GameObject golfball;
    public float spawnRate;
    private float time = 0;

    void Update()
    {
        time += Time.deltaTime;
        if (time >= spawnRate)
        {
            Vector3 spawnPosition = new Vector3(Random.Range(-10f, 10f), 5, Random.Range(-10f, 10f));
            Instantiate(golfball, spawnPosition, Quaternion.identity);

            time = 0;
        }
    }
}
