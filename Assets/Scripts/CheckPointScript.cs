using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CheckPointScript : MonoBehaviour
{
    public TextMeshProUGUI informationUI;

    private ObserverScript observerScript;

    private void Start()
    {
        observerScript = FindObjectOfType<ObserverScript>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            print("Checkpoint");

            // Get a reference to the player GameObject.
            GameObject player = other.gameObject;

            // Get the IRespawnable component from the player GameObject.
            IRespawnable respawnable = player.GetComponent<IRespawnable>();

            // If the respawnable component exists, update its respawn point to this checkpoint's transform.
            if (respawnable != null)
            {
                respawnable.RespawnPoint = transform;

                // Notify the checkpoint observers that the player has hit a checkpoint
                if (observerScript != null)
                {
                    observerScript.NotifyObservers();
                }
            }
        }

        if (gameObject.name == "CheckPoint (2)")
        {
            informationUI.text = "CheckPoint!\nNow for something a bit more challenging!";
        }
        if (gameObject.name == "CheckPoint (3)")
        {
            informationUI.text = "You have finished the Game!! \nThank you for playing!";
        }

    }
}
