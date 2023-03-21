using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpecifics : MonoBehaviour,ICharControl
{
    bool isGrounded = false;
    bool hasBoosted = false;

    public float boostHorizontal = 50f;
    public float boostVertical = 25f;

    float jumpForce = 1000f;
    public float movementSpeed = 10f;

    public Vector3 boostVelocity;
    public Vector3 jumpingVelocity;
    Rigidbody rb;
    public Transform dummyBall;


    MoveCharacterScript parentScript;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        jumpingVelocity = new Vector3(0f, jumpForce, 0f);
        boostVelocity = new Vector3(boostHorizontal, boostVertical, 0f);

    }

    // Update is called once per frame
    void Update()
    {
        //Horizontal inputs
        if (Input.GetKey(KeyCode.D))
        {
            rb.AddForce(dummyBall.right * movementSpeed);
        }
        if (Input.GetKey(KeyCode.A))
        {
            rb.AddForce(-dummyBall.right * movementSpeed);
        }

        //Vertical inputs
        if (Input.GetKey(KeyCode.W))
        {
            rb.AddForce(dummyBall.forward * movementSpeed);
        }
        if (Input.GetKey(KeyCode.S))
        {
            rb.AddForce(-dummyBall.forward * movementSpeed);
        }


        if (Input.GetKeyUp(KeyCode.B) && hasBoosted == false && isGrounded == false)
        {
            boost();
            hasBoosted = true;
        }

        //Jump Input
        if (isGrounded == true && Input.GetKeyUp(KeyCode.Space))
        {
            jump();
            hasBoosted = false;
        }


        dummyBall.position = transform.position;
    }
    public void boost()
    {
        rb.AddForce(rb.velocity.normalized * boostHorizontal, ForceMode.Impulse);
    }


    public void jump()
    {
        print("Jumping!!");
        rb.AddForce(jumpingVelocity);
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

    public MoveCharacterScript daddy()
    {
        return parentScript;
    }

    public void iAm(MoveCharacterScript parent)
    {
        parentScript = parent;
    }
}
