using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Rotation_Control : MonoBehaviour
{
    public GameObject camera_rotation_object;
    float y_axis_position_offset;
    Camera camera;
    public float camera_rotation_speed;
    public float horizontal_angle_limit; 
    public float vertical_angle_limit;
    float x_axis_rotation = 0;
    float y_axis_rotation = 0;

    void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Start()
    {
        camera = this.GetComponent<Camera>();
        y_axis_position_offset = camera_rotation_object.transform.position.y - Golf_Cart_Control.instance.transform.position.y;
    }

    void Update()
    {
        camera_rotation_object.transform.position = new Vector3(Golf_Cart_Control.instance.transform.position.x, Golf_Cart_Control.instance.transform.position.y + y_axis_position_offset, Golf_Cart_Control.instance.transform.position.z);
        camera_rotation_object.transform.eulerAngles = new Vector3(0, Golf_Cart_Control.instance.transform.eulerAngles.y, 0);
        x_axis_rotation += -Input.GetAxis("Mouse Y") * camera_rotation_speed;
        x_axis_rotation = Mathf.Clamp(x_axis_rotation, -vertical_angle_limit, vertical_angle_limit);
        y_axis_rotation += Input.GetAxis("Mouse X") * camera_rotation_speed;
        y_axis_rotation = Mathf.Clamp(y_axis_rotation, -horizontal_angle_limit, horizontal_angle_limit);
        camera_rotation_object.transform.localRotation = Quaternion.Euler(-x_axis_rotation * -1, -y_axis_rotation * -1, 0);
    }
}