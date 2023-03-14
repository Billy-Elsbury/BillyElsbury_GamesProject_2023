using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    Transform target;
    private Vector3 offset = new Vector3(0,1,-5), desiredPosition;
    private Quaternion desiredRotation;
    public float smoothness = 20f;
    Vector3 lastPosition;
    private Vector3 lastForward;

    float theta, phi=90;
    // Start is called before the first frame update
    void Start()
    {
        desiredPosition = getRelative(target, offset);
    }

    private Vector3 getRelative(Transform target, Vector3 offset)
    {
        return target.position + offset.x * transform.right + offset.y * transform.up + offset.z * transform.forward;
    }

    // Update is called once per frame
    void Update()
    {
  
        desiredPosition = getRelative(target, Quaternion.AngleAxis(phi, target.right)*offset);
        //desiredPosition = target.position - target.forward * 10 + 2 * Vector3.up;
        // desiredRotation = target.rotation;

        //// Use Vector3.Lerp to smoothly move the camera towards the target position
        transform.position = desiredPosition;
        // Vector3 dir = ((target.position + target.forward * 2) - transform.position).normalized;
        // Use Quaternion.Slerp to smoothly rotate the camera towards the target rotation
        // Quaternion targetRotation = Quaternion.LookRotation(dir, Vector3.up);
        // transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * smoothness);
        // float d = Vector3.Dot(transform.forward, lastForward);

        //if (d<0.8f)
        //{
        //    print(d);
        //}

        transform.LookAt(target);
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

    internal void update(float horizontalDelta, float verticalDelta)
    {
        theta += horizontalDelta;
        theta = Mathf.Clamp(theta, -45, 45);
        phi += verticalDelta;
        phi = Mathf.Clamp(phi, 60, 120);
        print(desiredPosition.ToString());
        print(theta.ToString() + "   " + phi.ToString());
    }
}
