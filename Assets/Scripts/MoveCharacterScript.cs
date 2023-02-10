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

    public float movementSpeed = 5f;
    private float currentSpeed;
    public bool isGrounded;

    private Vector3 lastPosition;
    public Vector3 gravityModifier;

    private Rigidbody currentRB, ballRB, characterRB;

    public TextMeshProUGUI speedUI;

    // Start is called before the first frame update
    void Start()
    {
        characterPawn.gameObject.SetActive(true);
        characterBall.gameObject.SetActive(false);

        currentRB = characterPawn.GetComponent<Rigidbody>();
        ballRB = characterBall.GetComponent<Rigidbody>();
        characterRB = characterPawn.GetComponent<Rigidbody>();

        gravityModifier = new Vector3(0f, -1f, 0f);
        
        //lock cursor in camera view
        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        //call speed control method
        //SpeedControl();

        //code to increase gravity for better feeling physics
        currentRB.AddForce(gravityModifier * 2);
        

        //Horizontal inputs
        if (Input.GetAxis("Horizontal") > 0)
        {
            currentRB.AddForce(Vector3.right * movementSpeed);
        }
        else if (Input.GetAxis("Horizontal") < 0)
        {
            currentRB.AddForce(-Vector3.right * movementSpeed);
        }

        //Vertical inputs
        if (Input.GetAxis("Vertical") > 0)
        {
            currentRB.AddForce(Vector3.forward * movementSpeed);
        }
        else if (Input.GetAxis("Vertical") < 0)
        {
            currentRB.AddForce(-Vector3.forward * movementSpeed);
        }

        //Input to change between characters
        if(Input.GetKeyUp(KeyCode.C) && currentRB == characterRB)
        {
            characterSelected = 1;

            CopyVelocity(currentRB, ballRB);

            SwitchCharacter();  
        }

        else if (Input.GetKeyUp(KeyCode.C) && currentRB == ballRB)
        {
            characterSelected = 2;

            CopyVelocity(currentRB, characterRB);

            SwitchCharacter();
        }

        transform.position = currentRB.position;

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

        //processing characterSelected variable
        switch (characterSelected)
        {
            //Change from Pawn character to Ball character
            case 1:

                //change characterSelected state
                characterSelected = 2;

                characterPawn.gameObject.SetActive(false);
                characterBall.gameObject.SetActive(true);
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
    }

    //Code to display ball's current speed to UI
    void FixedUpdate()
    {
        currentSpeed = Vector3.Distance(lastPosition, transform.position) * 100f;
        lastPosition = transform.position;
        speedUI.text = currentSpeed.ToString("F0");
    }

}
