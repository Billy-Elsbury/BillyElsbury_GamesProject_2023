using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BallCharacterController : MonoBehaviour
{
    public float movementSpeed = 5f;
    private float currentSpeed;
    public bool isGrounded;

    private Vector3 lastPosition;
    public Vector3 jumping_velocity;
    
    private Rigidbody rb;

    public TextMeshProUGUI speedUI;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        isGrounded = false;
        jumping_velocity = new Vector3(0f, 1000f, 0f);

        //lock cursor in camera view
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        //call speed control method
        //SpeedControl();

        //Horizontal inputs
        if (Input.GetAxis("Horizontal") > 0)
        {
            rb.AddForce(Vector3.right * movementSpeed);
        }
        else if (Input.GetAxis("Horizontal") < 0)
        {
            rb.AddForce(-Vector3.right * movementSpeed);
        }

        //Vertical inputs
        if (Input.GetAxis("Vertical") > 0)
        {
            rb.AddForce(Vector3.forward * movementSpeed);
        }
        else if (Input.GetAxis("Vertical") < 0)
        {
            rb.AddForce(-Vector3.forward * movementSpeed);
        }

        //Jump Input
        if (isGrounded == true && Input.GetKeyUp(KeyCode.Space))
        {
            print("Jumping!!");
            rb.AddForce(jumping_velocity);
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            print("Ball is Grounded");
            isGrounded = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            print("Ball is Airborne!");
            isGrounded = false;
        }
    }

    void FixedUpdate()
    {
        //Code to display ball's current speed to UI
        currentSpeed = Vector3.Distance(lastPosition, transform.position) * 100f;
        lastPosition = transform.position;
        speedUI.text = currentSpeed.ToString("F0");
    }

}
