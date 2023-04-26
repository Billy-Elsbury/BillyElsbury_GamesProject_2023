using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointScript : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Get a reference to the player GameObject.
            GameObject player = other.gameObject;

            // Get the MoveCharacterScript component from the player GameObject.
            MoveCharacterScript moveCharacterScript = player.GetComponent<MoveCharacterScript>();

            // If the MoveCharacterScript component exists, update its respawn point to this checkpoint's transform.
            if (moveCharacterScript != null)
            {
                moveCharacterScript.respawnPoint = transform;
            }
        }
    }
}
