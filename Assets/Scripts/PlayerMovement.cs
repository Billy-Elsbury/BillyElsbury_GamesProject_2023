using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float movementSpeed;

    public float groundDrag;

    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    bool readyToJump;

    [Header("KeyBinds")]
    public KeyCode jumpKey = KeyCode.Space;

    //Ground Check Variables
    public float PlayerHeight;
    public LayerMask groundLayer;
    bool grounded;


    public Transform orientation;

    float horizontalInput;
    float verticalInput;

    Vector3 movementDirection;

    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        readyToJump = true;
        print("Ready to Jump!");
    }

    // Update is called once per frame
    void Update()
    {
        //call method for keyboard inputs
        KeyBoardInputs();

        //call speed controller method
        SpeedControl();

        //ground check on update
        //0.5f is half player height
        //0.2f a bit extra for usability
        grounded = Physics.Raycast(transform.position, Vector3.down, PlayerHeight * 0.5f + 0.2f, groundLayer);
    }

    private void FixedUpdate()
    {
        CharacterMovement();
    }

    //Method for keyboard inputs
    private void KeyBoardInputs()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        //Should Jump check
        if(Input.GetKey(jumpKey) && readyToJump && grounded)
        {
            readyToJump = false;
            print("Jump Cooldown resetting!");

            //call method to jump
            Jump();

            //reset jump cooldown to allow holding of jump input
            Invoke(nameof(resetJump), jumpCooldown);
            
        }
    }

    private void CharacterMovement()
    {
        //calculating movement direction
        movementDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        // grounded movement
        if(grounded)
            rb.AddForce(movementDirection.normalized * movementSpeed * 10f, ForceMode.Force);

        //air control movement
        else if(!grounded)
            rb.AddForce(movementDirection.normalized * movementSpeed * 10f * airMultiplier, ForceMode.Force);

    }

    private void SpeedControl()
    {
        Vector3 flatVelocity = new Vector3(rb.velocity.x, rb.velocity.z);

        //limit velocity if above baseline
        if(flatVelocity.magnitude > movementSpeed)
        {
            Vector3 limitedVelocity = flatVelocity.normalized * movementSpeed;
            rb.velocity = new Vector3(limitedVelocity.x, rb.velocity.y, limitedVelocity.z);
        }
    }

    private void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
        print("Jumping!");
    }

    private void resetJump()
    {
        readyToJump = true;
        print("Ready to Jump!");
    }
}
