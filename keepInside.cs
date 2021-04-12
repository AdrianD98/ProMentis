using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class keepInside : MonoBehaviour
{
    Rigidbody rb;
    Vector3 startPos;
    bool exit = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        startPos = transform.position;
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "BoardBoundry")
        {
            exit = true;
        }
    }

    private void FixedUpdate()
    {

        if (exit)
        {
            transform.position = Vector3.MoveTowards(transform.position, startPos, 0.15f);
            if (transform.position == startPos) exit = false;
        }
    }
}
