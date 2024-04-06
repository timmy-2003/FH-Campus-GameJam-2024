using UnityEngine;

public class CollisionGravity : MonoBehaviour
{
    private bool isHit = false;
    private Rigidbody rb;

    void Start()
    {
        // Get the Rigidbody component attached to this GameObject or add one if it doesn't exist
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody>();
            rb.isKinematic = true;
        }
    }

    void Update()
    {
        if (!isHit)
        {
            // Object stands still until hit
            // You can put any other logic here for standing still
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the object is hit by another object
        if (!isHit)
        {
            // Once hit, turn on the Rigidbody with mass 1 and enable gravity
            isHit = true;
            rb.isKinematic = false;
            rb.mass = 20f;
            rb.useGravity = true;
            Debug.Log("Object has been hit! Rigidbody turned on with mass 1 and gravity enabled.");
        }
    }
}
