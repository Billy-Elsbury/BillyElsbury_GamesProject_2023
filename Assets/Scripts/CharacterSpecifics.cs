using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSpecifics : MonoBehaviour,ICharControl
{
    bool isGrounded = false;
    bool hasBoosted = false;

    float boostForce = 20f;
    float jumpForce = 15f;

    public Vector3 boostVelocity;
    public Vector3 jumpingVelocity;
    Rigidbody rb;

    MoveCharacterScript parentScript;

    public Animator playerAnimator;
    public float walkSpeed, walkBackSpeed, slowRunSpeed, runSpeed, rotateSpeed;
    public bool walking;
    public Transform playerTrans;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        jumpingVelocity = new Vector3(0f, jumpForce, 0f);
        boostVelocity = new Vector3(0f, boostForce, 0f);

    }
    void Update()
    {
        //basic code to move character
        if (Input.GetKey(KeyCode.W) && isGrounded)
        {
            rb.velocity = transform.forward * walkSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S) && isGrounded)
        {
            rb.velocity = -transform.forward * walkBackSpeed * Time.deltaTime;
        }

        if (Input.GetKeyUp(KeyCode.B) && hasBoosted == false && isGrounded == false)
        {
            boost();
            hasBoosted = true;
        }

        //Jump Input
        if ((isGrounded == true) && Input.GetKeyUp(KeyCode.Space))
        {
            jump();
            hasBoosted = false;
        }


        if (Input.GetKeyDown(KeyCode.W) && (isGrounded))
        {
            playerAnimator.SetTrigger("Slow_Run");
            playerAnimator.ResetTrigger("Idle");
            walking = true;
        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            playerAnimator.ResetTrigger("Slow_Run");
            playerAnimator.SetTrigger("Idle");
            walking = false;
        }
        if (Input.GetKeyDown(KeyCode.S) && (isGrounded))
        {
            playerAnimator.SetTrigger("Walk_Back");
            playerAnimator.ResetTrigger("Idle");
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            playerAnimator.ResetTrigger("Walk_Back");
            playerAnimator.SetTrigger("Idle");
        }
        if (Input.GetKey(KeyCode.A))
        {
            playerTrans.Rotate(0, -rotateSpeed * Time.deltaTime, 0);
        }
        if (Input.GetKey(KeyCode.D))
        {
            playerTrans.Rotate(0, rotateSpeed * Time.deltaTime, 0);
        }
        if (walking == true)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                walkSpeed = walkSpeed + runSpeed;
                playerAnimator.SetTrigger("Fast_Run");
                playerAnimator.ResetTrigger("Slow_Run");
            }
            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                walkSpeed = slowRunSpeed;
                playerAnimator.ResetTrigger("Fast_Run");
                playerAnimator.SetTrigger("Slow_Run");
            }
        }
    }

    public void boost()
    {
        rb.AddForce(boostVelocity, ForceMode.Impulse);
    }


    public void jump()
    {
        print("Jumping!!");
        rb.AddForce(jumpingVelocity, ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            print("Character is Grounded");
            isGrounded = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            print("Character is Airborne!");
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
