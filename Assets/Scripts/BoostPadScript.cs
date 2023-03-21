using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostPadScript : MonoBehaviour
{
    MoveCharacterScript character;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }


    private void OnTriggerEnter(Collider collision)
    {
        ICharControl character = collision.transform.GetComponent<ICharControl>();
        if (character != null)
        {
            print("BOOSTED!");
            character.daddy().boost();
        }
    print("Entered Boost!");

    }

}
