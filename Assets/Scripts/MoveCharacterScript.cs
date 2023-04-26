using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MoveCharacterScript : MonoBehaviour
{
    //Reference character game objects
    public GameObject characterBall, characterPawn;
    BallSpecifics theBall;

    //variable to control which character is selected
    int characterSelected = 2;

    private float currentSpeed; //only for UI
    public float boostForce = 50f;
    public float maxSpeed = 200f;

    public bool isGrounded;

    CameraFollow theCam;

    private Vector3 lastPosition;
    public Vector3 gravityModifier = new Vector3(0f, -7f, 0f);
    public Vector3 boostPadVelocity;

    internal void boost()
    {
        currentRB.AddForce(currentRB.velocity.normalized * boostForce, ForceMode.Impulse);
    }

    private Rigidbody currentRB, ballRB, characterRB;

    public TextMeshProUGUI speedUI;

    bool OnBoostPad;

    

    // Start is called before the first frame update
    void Start()
    {
       
        boostPadVelocity = new Vector3(100f, 0f, 0f);

        theCam = FindObjectOfType<CameraFollow>();
        //theCam.follow(characterPawn.transform);
        theBall = characterBall.GetComponent<BallSpecifics>();
        characterPawn.gameObject.SetActive(false);
        characterBall.gameObject.SetActive(true);

        currentRB = characterBall.GetComponent<Rigidbody>();
        ballRB = characterBall.GetComponent<Rigidbody>();
        characterRB = characterPawn.GetComponent<Rigidbody>();

        inform(ballRB);
        inform(characterRB);

        //lock cursor in camera view
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void inform(Rigidbody rb)
    {
        rb.GetComponent<ICharControl>();
        ICharControl character = rb.GetComponent<ICharControl>();
        character.IAm(this);
    }

    // Update is called once per frame
    void Update()
    {
        //call speed control method
        SpeedControl();

        //code to increase gravity for better feeling physics
        currentRB.AddForce(gravityModifier * 2);


        //Input to change between characters
        if (Input.GetKeyUp(KeyCode.C))
        {
            SwitchCharacter();
        }

        transform.position = currentRB.position;
       
    }

    private void SpeedControl()
    {
        if (currentRB.velocity.magnitude > maxSpeed)
        {
            currentRB.velocity = currentRB.velocity.normalized * maxSpeed;
        }
    }

    public void SwitchCharacter()
    {
        // store the current velocity
        Vector3 velocity = currentRB.velocity;
        // store the current rotation
        Quaternion rotation = transform.rotation; 


        //processing characterSelected variable
        switch (characterSelected)
        {
            //Change from Pawn character to Ball character
            case 1:

                //change characterSelected state
                characterSelected = 2;

                
                characterBall.gameObject.SetActive(true);
                characterPawn.gameObject.SetActive(false);

                // set new character's position to the previous character's position
                characterBall.transform.position = transform.position;
                // set new character's rotation to the previous character's rotation
                characterBall.transform.rotation = rotation;

                currentRB = characterBall.GetComponent<Rigidbody>();

                
                break;

            //Change from Ball character to Pawn character
            case 2:

                //change characterSelected state
                characterSelected = 1;

                characterPawn.gameObject.SetActive(true);
                characterBall.gameObject.SetActive(false);


                characterPawn.transform.position = transform.position;
                // set new character's position to the previous character's position
                characterBall.transform.rotation = rotation; 

                currentRB = characterPawn.GetComponent<Rigidbody>();

                
                break;
        }
        currentRB.velocity = velocity;
    }

    //Code to display ball's current speed to UI
    void FixedUpdate()
    {
        currentSpeed = Vector3.Distance(lastPosition, transform.position) * 100f;
        lastPosition = transform.position;
        speedUI.text = currentSpeed.ToString("F0");
    }

    public void boostPad()
    {
        currentRB.AddForce(boostPadVelocity, ForceMode.Impulse);
    } 

}
