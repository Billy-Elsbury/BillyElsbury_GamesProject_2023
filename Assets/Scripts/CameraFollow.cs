using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // the target object to follow
    float smoothSpeed = 0.125f; // the speed at which the camera will follow the target
    Vector3 offset; // the initial offset from the target object
    float mouseSensitivity = 10.0f; // the sensitivity of the mouse movement
    float theta =90, phi = 60;
    float distance; // the distance between the camera and the target

    

    void Start()
    {
        offset = new Vector3(0, 2, -7);
        distance = offset.magnitude; // get the initial distance between the camera and the target
    }

    void Update()
    {
        // handle mouse movement
        theta += Input.GetAxis("Horizontal") * mouseSensitivity * Time.deltaTime;
        phi += Input.GetAxis("Vertical") * mouseSensitivity * Time.deltaTime;
        phi = Mathf.Clamp(phi, -60, 5);

        //print(Input.GetAxis("Vertical"));

        Vector3 desiredPosition = target.position + distance * (Quaternion.AngleAxis(theta, Vector3.up) * Quaternion.AngleAxis(phi, target.right) * target.forward);

        //print(phi.ToString() + "    " + theta.ToString());
        Vector3 axis = Vector3.Cross(Vector3.up, (transform.position - target.position).normalized);


        // smoothly update the position and rotation of the camera
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        transform.LookAt(target.position);
    }

}

