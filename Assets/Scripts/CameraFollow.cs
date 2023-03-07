using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    Transform target;
    private Vector3 offset, desiredPosition;
    private Quaternion desiredRotation;
    private int armLength =  2;
    public float smoothness = 20f;
    Vector3 lastPosition;
    private Vector3 lastForward;

    // Start is called before the first frame update
    void Start()
    {
   
    }

    // Update is called once per frame
    void Update()
    {
        desiredPosition = target.position - target.forward * 10 + 2 * Vector3.up;
        desiredRotation = target.rotation;


        ////Code to make camera follow character and roation of character
        //// Normalize the target forward vector
        //Vector3 forward = target.forward.normalized;

        //// Calculate the local position for the camera
        //Vector3 localPos = new Vector3((forward.x * -armLength), (forward.y * -armLength) + armLength / 2.5f, (forward.z * -armLength));
        ////transform.position = target.position + localPos;

        //// Normalize the look offset vector
        //Vector3 lookOffset = (target.position + Vector3.up * 2f).normalized;

        //// Use Vector3.Lerp to smoothly move the camera towards the target position
        transform.position = Vector3.Lerp(transform.position,desiredPosition, Time.deltaTime * smoothness);

        // Use Quaternion.Slerp to smoothly rotate the camera towards the target rotation
        ;//  Quaternion targetRotation = Quaternion.LookRotation(lookOffset - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, Time.deltaTime * smoothness);
        float d = Vector3.Dot(transform.forward, lastForward);

        if (d<0.8f)
        {
            print(d);
        }
        

    }

    internal void follow(Transform targetTransform)
    {
        target = targetTransform;
    }

    private void LateUpdate()
    {
        lastPosition = transform.position;
        lastForward = transform.forward;
    }
}
