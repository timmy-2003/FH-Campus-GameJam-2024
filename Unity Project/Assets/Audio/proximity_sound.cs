using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class proximity_sound : MonoBehaviour
{

    public Transform target;
    public float range;
    public float midrange;
    public float dx;

    private Transform _tranform;
    private Vector3 offset;
    public Audiosource Audio;

    // Start is called before the first frame update
    void Start()
    {
        _tranform = GetComponent<Transform>();
        dx = 0.0f;
        Audio.volume = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (target)
        {
            offset = target.position - _tranform.position;
            dx = offset.sqrMagnitude;

            if (dx < range * range)
            {
                Debug.Log("Range1");
            }

            if (dx < midrange * midrange)
            {
                Debug.Log("Range2");
                volume = 1 - (dx / (midrange * midrange));
                Audio.volume = volume;
            }

        }

    }
}
