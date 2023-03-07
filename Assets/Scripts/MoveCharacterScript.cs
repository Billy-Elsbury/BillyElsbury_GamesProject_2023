using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MoveCharacterScript : MonoBehaviour
{
    //Reference character game objects
    public GameObject characterBall, characterPawn;

    //variable to control which character is selected
    int characterSelected = 1;

    private float currentSpeed; //only for UI
    private float boostForce = 50f;

    public bool isGrounded;

    private Vector3 lastPosition;
    public Vector3 gravityModifier;

    internal void boost()
    {
        currentRB.AddForce(currentRB.velocity.normalized * boostForce, ForceMode.Impulse);
    }

    public Vector3 boostPadVelocity;

    private Rigidbody currentRB, ballRB, characterRB;

    public TextMeshProUGUI speedUI;

    bool OnBoostPad;

    // Start is called before the first frame update
    void Start()
    {
        characterPawn.gameObject.SetActive(true);
        characterBall.gameObject.SetActive(false);

        currentRB = characterPawn.GetComponent<Rigidbody>();
        ballRB = characterBall.GetComponent<Rigidbody>();
        characterRB = characterPawn.GetComponent<Rigidbody>();

        inform(ballRB);
        inform(characterRB);

        gravityModifier = new Vector3(0f, -1f, 0f);
        boostPadVelocity = new Vector3(50f, 0f, 0f);

        //lock cursor in camera view
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void inform(Rigidbody rb)
    {
        rb.GetComponent<ICharControl>();
        ICharControl character = rb.GetComponent<ICharControl>();
        character.iAm(this);
    }

    // Update is called once per frame
    void Update()
    {
        //call speed control method
        //SpeedControl();

        //code to increase gravity for better feeling physics
        currentRB.AddForce(gravityModifier * 2);


       

        //Input to change between characters
        if (Input.GetKeyUp(KeyCode.C))
        {
            if (characterSelected == 1)
            {
                SwitchCharacter();
            }

            else if (characterSelected == 2)
            {

                SwitchCharacter();
            }
        }

        transform.position = currentRB.position;


        //code to rotate character and camera should follow
        float rotateHorizontal = -Input.GetAxis("Mouse X");
        float rotateVertical = -Input.GetAxis("Mouse Y");
        float sensitivity = 1;

        transform.RotateAround(transform.position, -Vector3.up, rotateHorizontal * sensitivity);

        transform.RotateAround(Vector3.zero, transform.right, rotateVertical * sensitivity);


    }

    //method to Copy the velocity from one character to the other when switching between them
    //Based on code from: https://answers.unity.com/questions/1524258/transfer-velocity-from-one-object-to-another.html
    void CopyVelocity(Rigidbody from, Rigidbody to)
    {
        Vector3 vFrom = from.velocity;
        Vector3 vTo = to.velocity;

        // Move the values you want for each exis
        vTo.x = vFrom.x;
        // vTo.y = vFrom.y; // Leaving y-axis as is
        vTo.z = vFrom.z;

        to.velocity = vTo;
    }

    public void SwitchCharacter()
    {
        Vector3 velocity = currentRB.velocity;

        //processing characterSelected variable
        switch (characterSelected)
        {
            //Change from Pawn character to Ball character
            case 1:

                //change characterSelected state
                characterSelected = 2;

                
                characterBall.gameObject.SetActive(true);
                characterPawn.gameObject.SetActive(false);

                characterBall.transform.position = transform.position;

                currentRB = characterBall.GetComponent<Rigidbody>();

                
                break;

            //Change from Ball character to Pawn character
            case 2:

                //change characterSelected state
                characterSelected = 1;

                characterPawn.gameObject.SetActive(true);
                characterBall.gameObject.SetActive(false);
                characterPawn.transform.position = transform.position;

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
