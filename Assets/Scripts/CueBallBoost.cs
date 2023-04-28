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
        Invoke("Count", 6);
        rb = GetComponent<Rigidbody>();
        
    }


    void Count()
    {
        rb.AddForce(boostVelocity * boostForce, ForceMode.Impulse);
    }
}
