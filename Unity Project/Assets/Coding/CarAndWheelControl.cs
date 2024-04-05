using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AxleInfo
{
    public WheelCollider leftWheel;
    public WheelCollider rightWheel;
    public bool motor;
    public bool steering;
}
public class CarAndWheelControl : MonoBehaviour
{
    public static CarAndWheelControl instance { get; private set; }
    Rigidbody rigidbody;
    public Rigidbody Rigidbody { get { return rigidbody; } }
    public List<AxleInfo> axleInfos;
    public float maxMotorTorque;
    public float maxSteeringAngle;
    float motor;
    float steering;
    bool braking;
    bool allWheelsGrounded;
    public bool AllWheelsGrounded { get { return allWheelsGrounded;  } }
    public float midAirRotationSpeed;
    public GameObject camera;
    Vector3 cameraStartPosition;

    private void Awake()
    {
        if (instance != null && instance != this) { Destroy(this); }
        else {
            instance = this;
            DontDestroyOnLoad(this);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = this.GetComponent<Rigidbody>();
        cameraStartPosition=camera.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            braking = true;
        }
        else
        {
            braking = false;
        }
        camera.transform.position = cameraStartPosition+transform.position;
    }

    public void FixedUpdate()
    {
        checkIfGrounded();
        checkBraking();
        checkWheels();
        checkMidAirRotation();
    }

    //Finds the corresponding visual wheel in the child object and correctly applies the rotation
    private void ApplyVisualRotation(WheelCollider collider)
    {
        //Checks if there are even any child objects
        if (collider.transform.childCount == 0)
        {
            return;
        }

        Transform visualWheel = collider.transform.GetChild(0); //Transform of the object with the wheel mesh

        Vector3 position; //Will not get used, however the collider always gives back the position as well
        Quaternion rotation; //Stores the rotation of the collider
        collider.GetWorldPose(out position, out rotation); //Gets position data from the collider
        visualWheel.transform.rotation = rotation; //Applies the colliders rotation to the wheel mesh
    }

    //Checks if all four wheels are grounded
    private void checkIfGrounded()
    {
        int groundedCounter = 0;
        foreach (AxleInfo axleInfo in axleInfos)
        {
            if (axleInfo.leftWheel.isGrounded)
            {
                groundedCounter++;
            }
            if (axleInfo.rightWheel.isGrounded)
            {
                groundedCounter++;
            }
        }
        if (groundedCounter < axleInfos.Count * 2)
        {
            allWheelsGrounded = false;
        }
        else
        {
            allWheelsGrounded = true;
        }
    }

    //Checks whether the car should brake or not and sets values accordingly
    private void checkBraking()
    {
        if (braking == true)
        {
            motor = 0; //Sets the cars applied motor torque/force to zero, meaning no additional speed gets added to the car
            //Sets the cars braking torque to the max motor torque, meaning it comes to a halt almost immediately
            foreach (AxleInfo axleInfo in axleInfos)
            {
                axleInfo.leftWheel.brakeTorque = maxMotorTorque;
                axleInfo.rightWheel.brakeTorque = maxMotorTorque;
            }
        }
        else
        {
            motor = maxMotorTorque * Input.GetAxis("Vertical"); //Reads vertical input, multiplies it with the max motor torque and adds it to the current motor torque
            //Sets the cars braking torque to zero again...
            foreach (AxleInfo axleInfo in axleInfos)
            {
                if (Input.GetAxis("Vertical") != 0)
                {
                    axleInfo.leftWheel.brakeTorque = 0;
                    axleInfo.rightWheel.brakeTorque = 0;
                }
                //...unless the input is zero, then the car should stop on its own, but gradually
                else
                {
                    axleInfo.leftWheel.brakeTorque = maxMotorTorque / 4;
                    axleInfo.rightWheel.brakeTorque = maxMotorTorque / 4;
                }
            }
        }
    }

    //Handles the cars wheels steering, acceleration and rotation
    private void checkWheels()
    {
        steering = maxSteeringAngle * Input.GetAxis("Horizontal"); //Reads horizontal input and sets the steering accordingly

        //Checks all the axles wheels and applies the correct steering, motor and rotation
        foreach (AxleInfo axleInfo in axleInfos)
        {
            if (axleInfo.steering)
            {
                axleInfo.leftWheel.steerAngle = steering;
                axleInfo.rightWheel.steerAngle = steering;
            }
            if (axleInfo.motor)
            {
                axleInfo.leftWheel.motorTorque = motor;
                axleInfo.rightWheel.motorTorque = motor;
            }
            ApplyVisualRotation(axleInfo.leftWheel);
            ApplyVisualRotation(axleInfo.rightWheel);
        }
    }

    //Enables the car to adjust its rotation when not on all four wheels (mainly intended for mid air adjustments)
    private void checkMidAirRotation()
    {
        if (allWheelsGrounded == false)
        {
            //Allows for midair rotation adjustments to the car
            Quaternion addRotationHorizontal = Quaternion.Euler(new Vector3(0, 0, midAirRotationSpeed) * -Input.GetAxis("Horizontal"));
            rigidbody.MoveRotation(rigidbody.rotation * addRotationHorizontal);
            Quaternion addRotationVertical = Quaternion.Euler(new Vector3(midAirRotationSpeed, 0, 0) * Input.GetAxis("Vertical"));
            rigidbody.MoveRotation(rigidbody.rotation * addRotationVertical);
        }
    }
}