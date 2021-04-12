using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fall : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Forcefield"))
        other.GetComponentInParent<Rigidbody>().useGravity = true;
    }
}
