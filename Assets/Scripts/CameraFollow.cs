using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    private Vector3 offset;
    private int armLength =  7;

    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - target.position;
    }

    // Update is called once per frame
    void Update()
    {
        //Code to make camera follow character and roation of character
        Vector3 localPos = new Vector3 ((target.forward.x * -armLength), (target.forward.y * -armLength) + armLength/2.5f, (target.forward.z * -armLength));
        transform.position = target.position + localPos;

        Vector3 lookOffset = target.position;
        lookOffset = lookOffset + new Vector3(0, 2, 0);
        transform.LookAt(lookOffset);
    }
}
