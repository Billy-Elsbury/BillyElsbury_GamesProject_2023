using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CheckPointScript;

public class CharacterSpecifics : MonoBehaviour, ICharControl, IRespawnable
{
    private Transform respawnPoint; // the respawn point to teleport the player to

    public Transform RespawnPoint
    {
        get { return respawnPoint; }
        set { respawnPoint = value; }
    }

    bool isGrounded = false;
    bool hasBoosted = false;

    public float boostForce;
    public float jumpForce;

    Vector3 boostVelocity;
    Vector3 jumpingVelocity;
    public Rigidbody rb;

    MoveCharacterScript parentScript;

    public Animator playerAnimator;
    public float walkSpeed, walkBackSpeed, slowRunSpeed, runSpeed, rotateSpeed;
    public bool walking;
    public Transform playerTrans;

    public float respawnHeight; // the height below which the player will respawn

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        jumpingVelocity = new Vector3(0f, jumpForce, 0f);
        boostVelocity = new Vector3(0f, boostForce, 0f);

        respawnHeight = -100f;
        respawnPoint = new GameObject("Respawn Point").transform;
        respawnPoint.position = new Vector3(-0.7f, -2.6f, -70f);

    }
    void Update()
    {
       
        //Code for Input Management

        if (Input.GetKey(KeyCode.W) && isGrounded)
        {

            rb.velocity = transform.forward * walkSpeed * Time.deltaTime;

        }
        if (Input.GetKey(KeyCode.S) && isGrounded)
        {
            rb.velocity = -transform.forward * walkBackSpeed * Time.deltaTime;
        }

        //Boost Input
        if (Input.GetKeyUp(KeyCode.LeftShift) && !hasBoosted && !isGrounded)
        {
            Boost();
            hasBoosted = true;
        }

        //Jump Input
        if ((isGrounded) && Input.GetKeyUp(KeyCode.Space))
        {
            Jump();
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
            if (Input.GetKeyUp(KeyCode.LeftShift) || Input.GetKeyUp(KeyCode.W))
            {
                walkSpeed = slowRunSpeed;
                playerAnimator.ResetTrigger("Fast_Run");
                playerAnimator.SetTrigger("Slow_Run");
            }
        }

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

    public Boolean check_Ground()
    {
        Boolean Grounded = false;

        if (Physics.Raycast(rb.position, rb.transform.up * -1, 1.1f))
        {
            Grounded = true;
        }
        
        return Grounded;
    }


    public void Boost()
    {
        rb.AddForce(boostVelocity, ForceMode.Impulse);
    }


    public void Jump()
    {
        //print("Jumping!!");
        rb.AddForce(jumpingVelocity, ForceMode.Impulse);
    }


public MoveCharacterScript Father()
{
    return parentScript;
}

public void IAm(MoveCharacterScript parent)
{
    parentScript = parent;

}
}
