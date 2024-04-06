using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("GameObject with sound")]
[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(float))]

public class proximity_sound_take2 : MonoBehaviour
{
    public bool useCollision = true;
    public AudioClip soundclip;
    public float positionAudioSource;
    public float positionCart;
    public float positionDifference;
    public float distance;

    public Transform target;
    private Transform _tranform;
    private Vector3 offset;
    public float dx;








    // Start is called before the first frame update
    void Start()
    {
        _tranform = GetComponent<Transform>();
        dx = 0.0f;





    }

    // Update is called once per frame
    void Update()
    {
        if (target)
        {
            offset = target.position - _tranform.position;
            dx = offset.sqrMagnitude;

            if (soundclip != null)
            {
                GetComponent<AudioSource>().clip = soundclip;
                GetComponent<AudioSource>().Play();


            }

            positionDifference = positionCart - positionAudioSource;
        }
    }
}
