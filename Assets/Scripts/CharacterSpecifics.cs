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

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        jumpingVelocity = new Vector3(0f, jumpForce, 0f);
        boostVelocity = new Vector3(0f, boostForce, 0f);

    }

    // Update is called once per frame
    void Update()
    {
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
