using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSettings : MonoBehaviour
{
    [Header("References")]
    public Transform orientation;
    public Transform player;
    public Transform playerCharacter;
    public Rigidbody playerRigidBody;

    public float rotationSpeed;


    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
       //Find out the player's forward
       Vector3 viewDirection = player.position - new Vector3(transform.position.x, transform.position.y, transform.position.z);
       orientation.position = viewDirection.normalized;

        //Rotate player to view
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 inputDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        //Correcting camera
        if (inputDirection != Vector3.zero)
            playerCharacter.forward = Vector3.Slerp(playerCharacter.forward, inputDirection.normalized, Time.deltaTime * rotationSpeed);

    }
}
