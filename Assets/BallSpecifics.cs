using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpecifics : MonoBehaviour,ICharControl
{
    bool isGrounded = false;
    bool hasBoosted = false;
    float boostForce = 20f;
    public Vector3 jumping_velocity;
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        jumping_velocity = new Vector3(0f, 1000f, 0f);
        
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
        rb.AddForce(rb.velocity.normalized * boostForce, ForceMode.Impulse);
    }


    public void jump()
    {
        print("Jumping!!");
        rb.AddForce(jumping_velocity);
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
}
