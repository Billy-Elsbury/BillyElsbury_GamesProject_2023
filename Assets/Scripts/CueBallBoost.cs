using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CueBallBoost : MonoBehaviour
{
    Rigidbody rb;
    float boostForce = 5f;
    Vector3 boostVelocity = new Vector3(0f, 0f, -400f);

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            rb.AddForce(boostVelocity * boostForce, ForceMode.Impulse);
        }
    }
}
