using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CheckpointObserver : Observer
{
    private ObserverScript observerScript;

    public TextMeshProUGUI informationUI;


    private void Start()
    {
        // Find the observer script in the scene
        observerScript = FindObjectOfType<ObserverScript>();

        // Add this observer to the list of observers
        observerScript.AddObserver(this);
    }

    public override void OnNotify()
    {
        print("Observer knows player has hit a checkpoint!");
    }
}

