using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody rb;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        InvokeRepeating("SetVelocity", 0.0f, 1.0f);
        // rb.velocity = new Vector3(10, 0, 10);
    }

    void SetVelocity() {
        float x = Random.Range(-10.0f, 10.0f);
        float z = Random.Range(-10.0f, 10.0f);
        rb.velocity = new Vector3(x, 0, z);

    }
}
