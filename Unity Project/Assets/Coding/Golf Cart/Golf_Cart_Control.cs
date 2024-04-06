using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Axle_Info
{
    public WheelCollider left_wheel;
    public WheelCollider right_wheel;
    public bool motorized;
    public bool steerable;
}

/*
*If the camera collides with an object in the back, the camera should be positioned forward accordingly so that it is not positioned in the collided object anymore.
*The steering, braking, and slipping has to be calibrated more precisely.
*When steering the golf cart, its sides should slighty lean into the moving sideways direction. This should help with the golf cart tripping over less often.
*(Also concerns the above;) When not all the four wheels are on the ground, the golf cart should be able to be rotated or flipped onto its four wheels again.
*/

public class Golf_Cart_Control : MonoBehaviour
{
    public static Golf_Cart_Control instance { get; private set; }
    Rigidbody rigidbody;
    public Rigidbody Rigidbody { get { return rigidbody; } }
    public List<Axle_Info> axle_infos;
    public float maximum_motor_torque;
    public float maximum_steer_angle;
    float motor_torque;
    float steer_angle;
    bool brake;
    bool wheels_grounded;
    public bool Wheels_Grounded { get { return wheels_grounded;  } }
    public float airborne_rotation_speed;
    private bool beerPowerupEnabled = false;
    private float beerPowerupDuration = 0;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
    }

    void Start()
    {
        rigidbody = this.GetComponent<Rigidbody>();
    }
    
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            brake = true;
        }
        else
        {
            brake = false;
        }

        if (beerPowerupEnabled)
        {
            beerPowerupDuration += Time.deltaTime;
            if (beerPowerupDuration >= 3)
            {
                maximum_motor_torque = 400;
                beerPowerupEnabled = false;
                beerPowerupDuration = 0;
            }
        }
    }

    public void FixedUpdate()
    {
        Check_Wheels_Grounded();
        Check_Brake();
        Check_Wheels();
        Check_Airborne_Rotation();
    }

    private void Check_Wheels_Grounded()
    {
        int grounded_counter = 0;
        foreach (Axle_Info axle_info in axle_infos)
        {
            if (axle_info.left_wheel.isGrounded)
            {
                grounded_counter++;
            }
            if (axle_info.right_wheel.isGrounded)
            {
                grounded_counter++;
            }
        }
        if (grounded_counter < axle_infos.Count * 2)
        {
            wheels_grounded = false;
        }
        else
        {
            wheels_grounded = true;
        }
    }

    private void Check_Brake()
    {
        if (brake == true)
        {
            motor_torque = 0;
            foreach (Axle_Info axle_info in axle_infos)
            {
                axle_info.left_wheel.brakeTorque = maximum_motor_torque;
                axle_info.right_wheel.brakeTorque = maximum_motor_torque;
            }
        }
        else
        {
            motor_torque = maximum_motor_torque * Input.GetAxis("Vertical");
            
            foreach (Axle_Info axle_info in axle_infos)
            {
                if (Input.GetAxis("Vertical") != 0)
                {
                    axle_info.left_wheel.brakeTorque = 0;
                    axle_info.right_wheel.brakeTorque = 0;
                }
                else
                {
                    axle_info.left_wheel.brakeTorque = maximum_motor_torque / 4;
                    axle_info.right_wheel.brakeTorque = maximum_motor_torque / 4;
                }
            }
        }
    }

    private void Check_Wheels()
    {
        steer_angle = maximum_steer_angle * Input.GetAxis("Horizontal");

        foreach (Axle_Info axle_info in axle_infos)
        {
            if (axle_info.steerable)
            {
                axle_info.left_wheel.steerAngle = steer_angle;
                axle_info.right_wheel.steerAngle = steer_angle;
            }
            if (axle_info.motorized)
            {
                axle_info.left_wheel.motorTorque = motor_torque;
                axle_info.right_wheel.motorTorque = motor_torque;
            }
            Apply_Visual_Rotation(axle_info.left_wheel);
            Apply_Visual_Rotation(axle_info.right_wheel);
        }
    }

    private void Apply_Visual_Rotation(WheelCollider wheel_collider)
    {
        if (wheel_collider.transform.childCount == 0)
        {
            return;
        }

        Transform wheel_game_object = wheel_collider.transform.GetChild(0);

        Vector3 wheel_collider_position;
        Quaternion wheel_collider_rotation;
        wheel_collider.GetWorldPose(out wheel_collider_position, out wheel_collider_rotation);
        wheel_game_object.transform.rotation = wheel_collider_rotation;
    }

    private void Check_Airborne_Rotation()
    {
        if (wheels_grounded == false)
        {
            Quaternion additional_horizontal_rotation = Quaternion.Euler(new Vector3(0, 0, airborne_rotation_speed) * -Input.GetAxis("Horizontal"));
            rigidbody.MoveRotation(rigidbody.rotation * additional_horizontal_rotation);
            Quaternion additional_vertical_rotation = Quaternion.Euler(new Vector3(airborne_rotation_speed, 0, 0) * Input.GetAxis("Vertical"));
            rigidbody.MoveRotation(rigidbody.rotation * additional_vertical_rotation);
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Golfball")
        {
            Debug.Log("DEAD");
        }
        else if (other.gameObject.tag == "Beer")
        {
            GrantBeerPowerup();
            Destroy(other.gameObject);
        }
    }

    private void GrantBeerPowerup()
    {
        maximum_motor_torque *= 5;
        beerPowerupEnabled = true;
        beerPowerupDuration = 0;
    }
}