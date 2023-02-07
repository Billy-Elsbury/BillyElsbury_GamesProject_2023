using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CharacterSwitchScript : MonoBehaviour
{
    //Reference character game objects
    public GameObject characterBall, characterPawn;

    //variable to control which character is selected
    int characterSelected = 1;

    // Start is called before the first frame update
    void Start()
    {

        //enable Pawn character and disable Ball character on Start
        characterPawn.gameObject.SetActive(true);
        characterBall.gameObject.SetActive(false);
    }

    // Public method to switch avatars by pressing key
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
                break;

            //Change from Ball character to Pawn character
            case 2:

                //change characterSelected state
                characterSelected = 1;

                characterPawn.gameObject.SetActive(true);
                characterBall.gameObject.SetActive(false);
                break;
        }
    }
}
