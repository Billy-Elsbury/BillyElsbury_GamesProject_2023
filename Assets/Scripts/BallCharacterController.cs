using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallCharacterController : MonoBehaviour
{
    public float roll_speed = 5f;
    public bool isGrounded;

    public Vector3 jumping_velocity;
    
    private Rigidbody rigidBodyBall;

    // Start is called before the first frame update
    void Start()
    {
        rigidBodyBall = gameObject.GetComponent<Rigidbody>();
        isGrounded = false;
        jumping_velocity = new Vector3(0f, 2000f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        //Horizontal inputs
        if (Input.GetAxis("Horizontal") > 0)
        {
            rigidBodyBall.AddForce(Vector3.right * roll_speed);
        }
        else if (Input.GetAxis("Horizontal") < 0)
        {
            rigidBodyBall.AddForce(-Vector3.right * roll_speed);
        }

        //Vertical inputs
        if (Input.GetAxis("Vertical") > 0)
        {
            rigidBodyBall.AddForce(Vector3.forward * roll_speed);
        }
        else if (Input.GetAxis("Vertical") < 0)
        {
            rigidBodyBall.AddForce(-Vector3.forward * roll_speed);
        }

        //Jump Input
        if (isGrounded == true && Input.GetKeyUp(KeyCode.Space))
        {
            print("Attempting Jump!");
            rigidBodyBall.AddForce(jumping_velocity);
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name == "Ground")
        {
            print("Ball is Grounded");
            isGrounded = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.name == "Ground")
        {
            print("Ball is NOT Grounded!");
            isGrounded = false;
        }
    }
}
