using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallCharacterController : MonoBehaviour
{
    public float speed = 2f;
    private Rigidbody rigidBodyBall;

    // Start is called before the first frame update
    void Start()
    {
        rigidBodyBall = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //Horizontal inputs
        if (Input.GetAxis("Horizontal") > 0)
        {
            rigidBodyBall.AddForce(Vector3.right * speed);
        }
        else if (Input.GetAxis("Horizontal") < 0)
        {
            rigidBodyBall.AddForce(-Vector3.right * speed);
        }

        //Vertical inputs
        if (Input.GetAxis("Vertical") > 0)
        {
            rigidBodyBall.AddForce(Vector3.forward * speed);
        }
        else if (Input.GetAxis("Vertical") < 0)
        {
            rigidBodyBall.AddForce(-Vector3.forward * speed);
        }
    }
}
