using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dummyBallScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 camForward = Camera.main.transform.forward;
        camForward = new Vector3(camForward.x, 0, camForward.z).normalized;

        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(camForward, Vector3.up), 0.02f);
    }
}
