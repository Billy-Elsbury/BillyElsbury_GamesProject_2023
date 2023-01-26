using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallCharacterController : MonoBehaviour
{
    public float speed = 2f;
    private Rigidbody rigidBodyBall;
    private Vector3 jumping_velocity;
    float start_jump_velocity = 10;
    private float jump_boost = 2;
    private float gravity = 10;

    enum CharacterStates { Grounded, Jumping, Falling }

    CharacterStates ball_state = CharacterStates.Grounded;

    // Start is called before the first frame update
    void Start()
    {
        rigidBodyBall = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //Horizontal inputs
        if (Input.GetAxis("Horizontal") > 0)
        {
            rigidBodyBall.AddForce(Vector3.right * speed);
        }
        else if (Input.GetAxis("Horizontal") < 0)
        {
            rigidBodyBall.AddForce(-Vector3.right * speed);
        }

        //Vertical inputs
        if (Input.GetAxis("Vertical") > 0)
        {
            rigidBodyBall.AddForce(Vector3.forward * speed);
        }
        else if (Input.GetAxis("Vertical") < 0)
        {
            rigidBodyBall.AddForce(-Vector3.forward * speed);
        }

        //Jumping input

        switch (ball_state)
        {
            //Grounded
            case CharacterStates.Grounded:

                print("Ball is on the ground!");
                if (shouldJump()) jump();
                transform.position += speed * transform.forward * Time.deltaTime;
                break;
            
            //Jumping
            case CharacterStates.Jumping:
                transform.position += jumping_velocity * Time.deltaTime;
                jumping_velocity -= gravity * Vector3.up * Time.deltaTime;

                if (jumping_velocity.y < 0f)
                {
                    ball_state = CharacterStates.Falling;
                }
                break;

            //Falling
            case CharacterStates.Falling:


                Collider[] colliding_with = Physics.OverlapSphere(transform.position, 0.2f);
                foreach (Collider c in colliding_with)
                {
                    print(c.tag);

                    if (c.tag == "Ground")
                    {
                        ball_state = CharacterStates.Grounded;
                        print("Found Ground!");
                    }

                }

                break;
        }
    }

                private void jump()
                {
                    ball_state = CharacterStates.Jumping;
                    print("Ball is Jumping!");

                    jumping_velocity = jump_boost * speed * transform.forward + start_jump_velocity * Vector3.up;
                }

                private bool shouldJump()
                {
                    return Input.GetKey(KeyCode.Space);
                }
        }
 
