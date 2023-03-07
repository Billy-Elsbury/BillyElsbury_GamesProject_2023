using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpecifics : MonoBehaviour,ICharControl
{
    bool isGrounded = false;
    bool hasBoosted = false;

    float boostForce = 50f;
    float boostVertical = 25f;

    float jumpForce = 1000f;
    public float movementSpeed = 15f;

    public Vector3 boostVelocity;
    public Vector3 jumpingVelocity;
    Rigidbody rb;

    MoveCharacterScript parentScript;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        jumpingVelocity = new Vector3(0f, jumpForce, 0f);
        boostVelocity = new Vector3(boostForce, boostVertical, 0f);

    }

    // Update is called once per frame
    void Update()
    {
        //Horizontal inputs
        if (Input.GetAxis("Horizontal") > 0)
        {
            rb.AddForce(transform.right * movementSpeed);
        }
        else if (Input.GetAxis("Horizontal") < 0)
        {
            rb.AddForce(-transform.right * movementSpeed);
        }

        //Vertical inputs
        if (Input.GetAxis("Vertical") > 0)
        {
            rb.AddForce(transform.forward * movementSpeed);
        }
        else if (Input.GetAxis("Vertical") < 0)
        {
            rb.AddForce(-transform.forward * movementSpeed);
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
    }
    public void boost()
    {
        rb.AddForce(rb.velocity.normalized * boostForce, ForceMode.Impulse);
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
