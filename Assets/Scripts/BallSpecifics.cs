using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpecifics : MonoBehaviour,ICharControl
{
    bool isGrounded = false;
    bool hasBoosted = false;

    public float boostHorizontal = 50f;
    public float boostVertical = 25f;

    public float jumpForce = 50f;
    public float movementSpeed = 500f;

    Vector3 boostVelocity;
    Rigidbody rb;
    public Transform dummyBall;
    SphereCollider co;

    MoveCharacterScript parentScript;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        co = GetComponent<SphereCollider>();

        boostVelocity = new Vector3(boostHorizontal, boostVertical, 0f);

    }

    // Update is called once per frame
    void Update()
    {
        //Horizontal inputs
        if (Input.GetKey(KeyCode.D))
        {
            rb.AddForce(dummyBall.right * movementSpeed * Time.deltaTime, ForceMode.Acceleration);
        }
        if (Input.GetKey(KeyCode.A))
        {
            rb.AddForce(-dummyBall.right * movementSpeed * Time.deltaTime, ForceMode.Acceleration);
        }

        //Vertical inputs
        if (Input.GetKey(KeyCode.W))
        {
            rb.AddForce(dummyBall.forward * movementSpeed * Time.deltaTime, ForceMode.Acceleration);
        }
        if (Input.GetKey(KeyCode.S))
        {
            rb.AddForce(-dummyBall.forward * movementSpeed * Time.deltaTime, ForceMode.Acceleration);
        }

        //Boost input
        if (Input.GetKeyUp(KeyCode.B) && !hasBoosted)
        {
            Boost();
            hasBoosted = true;
        }

        //Jump Input
        if (isGrounded && Input.GetKeyUp(KeyCode.Space))
        {
            Jump();
            hasBoosted = false;
        }

        dummyBall.position = transform.position;

        isGrounded = check_Ground();
    }

    public Boolean check_Ground()
    {
        Boolean Grounded = false;
        RaycastHit hit;


        if (Physics.Raycast(new Vector3(rb.position.x, rb.position.y, rb.position.z) - (new Vector3(0, -1, 0) - new Vector3(transform.up.x, transform.up.y, transform.up.z)), (new Vector3(0, -1, 0) - new Vector3(transform.up.x, transform.up.y, transform.up.z)), 2.2f));//layermask for collision sphere);
        {
            Grounded = true;
           
        }
        

        return Grounded;
    }

    public void Boost()
    {
        rb.AddForce(rb.velocity.normalized * boostHorizontal, ForceMode.Impulse);
    }


    public void Jump()
    {
        print("Jumping!!");
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }


    public MoveCharacterScript Daddy()
    {
        return parentScript;
    }

    public void IAm(MoveCharacterScript parent)
    {
        parentScript = parent;
    }
}
