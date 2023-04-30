using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CheckPointScript;

public class BallSpecifics : MonoBehaviour, ICharControl, IRespawnable
{
    private Transform respawnPoint; // the respawn point to teleport the player to

    public Transform RespawnPoint
    {
        get { return respawnPoint; }
        set { respawnPoint = value; }
    }

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

    public float respawnHeight; // the height below which the player will respawn

    MoveCharacterScript parentScript;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        co = GetComponent<SphereCollider>();

        boostVelocity = new Vector3(boostHorizontal, boostVertical, 0f);

        respawnHeight = -100f;
        respawnPoint = new GameObject("Respawn Point").transform;
        respawnPoint.position = new Vector3(-0.7f, -2.6f, -70.1f);

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
        if (Input.GetKeyUp(KeyCode.LeftShift) && !hasBoosted)
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

        // If player falls below certain point, respawn.
        if (transform.position.y < respawnHeight)
        {
            Respawn();
        }

    }

    private void Respawn()
    {
        // Teleport player to respawn point.
        rb.position = respawnPoint.position;
        rb.velocity = Vector3.zero;
    }


    public bool check_Ground()
{
    bool grounded = false;

    if (Physics.Raycast(rb.position - parentScript.transform.TransformDirection(Vector3.down), parentScript.transform.TransformDirection(Vector3.down), co.radius + 2f)) //layermask for collision sphere
    {
        grounded = true;
    }

    return grounded;


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
