using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PuzzleColliding : MonoBehaviour
{

    Rigidbody parent;
    float force = 0.1f;
    Material defaultMaterial;
    Color defaultColor = new Color(5,55,191,0);
    bool isPushed = false;
    

    private void Start()
    {
        parent = gameObject.GetComponentInParent<Rigidbody>();
        defaultMaterial = gameObject.GetComponent<Renderer>().material;
    }

    void setDefaultColor()
    {
        defaultMaterial.SetFloat("Speed", 0.1f);
        defaultMaterial.SetColor("Colorx", defaultColor * 0.2f);
    }
    

    void setDefaultColor(string trigger)
    {
        if (trigger == "goal" && !isPushed)
        {
            defaultMaterial.SetFloat("Speed", 0.05f);
            defaultMaterial.SetColor("Colorx", Color.green * 8f);
        }

        else if (trigger == "Forcefield")
        {
            defaultMaterial.SetFloat("Speed", 0.2f);
            defaultMaterial.SetColor("Colorx", Color.red * 8f);
        }
    }

    private void OnTriggerStay(Collider other)
    {

        if (other.gameObject.tag == "goal")
        {
            if (parent.velocity == parent.transform.position * 0f)
                parent.gameObject.tag = "solved";
            else parent.gameObject.tag = "unsolved";
        }
        else if (other.gameObject.tag == "Forcefield")
        {
            isPushed = true;
            Vector3 distance = (parent.position - other.transform.parent.position).normalized;
            
            if (parent.useGravity == true)
            {
                distance.y = 2;
            }

            parent.velocity = (distance) * force;
        }   

        setDefaultColor(other.gameObject.tag);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Forcefield")
        {
            parent.velocity = parent.transform.position * 0f;
            isPushed = false;
        }

        if (other.gameObject.tag == "goal")
        {
            parent.gameObject.tag = "unsolved";
        }
        setDefaultColor();
    }
}
