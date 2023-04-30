using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BirdHintScript : MonoBehaviour
{
    public TextMeshProUGUI birdSpeechText;
    public TextMeshProUGUI birdTwoSpeechText;

    private int birdOnehitCount = 0;
    private int birdTwohitCount = 0;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            print("Bird Hit");

            // Get a reference to the player GameObject.
            GameObject player = other.gameObject;

            if (gameObject.name == "Bird (1)")
            {
                if (birdOnehitCount == 0)
                {
                    birdSpeechText.text = "OUCH!!! I am not giving you a hint, don't do that again!";
                }
                else if (birdOnehitCount == 1)
                {
                    birdSpeechText.text = "FINE!! Press C to change character, it will help you cross that beam!";
                }
                else if (birdOnehitCount == 2)
                {
                    birdSpeechText.text = "I gave you the hint, not like it will fix your lack of skill Ha!";
                }
                else if (birdOnehitCount == 3)
                {
                    birdSpeechText.text = "Go Away!, You can't win this game anyway Muhahahaha!!";
                }

                birdOnehitCount++;
            }
            else if (gameObject.name == "Bird (2)")
            {
                if (birdTwohitCount == 0)
                {
                    birdTwoSpeechText.text = "Why do you keep hitting me? Are you mean or just bored?";
                }
                else if (birdTwohitCount == 1)
                {
                    birdTwoSpeechText.text = "Fine! Try changing in middair, that should be obvious though....";
                }
                else if (birdTwohitCount == 2)
                {
                    birdTwoSpeechText.text = "Still stuck? or have you even tried it yet?.";
                }
                else if (birdTwohitCount == 3)
                {
                    birdTwoSpeechText.text = "Hold W when going through the pipe, but let go of movement controls while in air!";
                }
                else if (birdTwohitCount == 4)
                {
                    birdTwoSpeechText.text = "That's all the advice I have, if you're still stuck then I don't know what to say...";
                }

                birdTwohitCount++;
            }
        }
    }

}
