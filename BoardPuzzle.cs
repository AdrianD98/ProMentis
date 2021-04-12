using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardPuzzle : MonoBehaviour
{

    [SerializeField] int total_spheres;
    [SerializeField] int current_spheres;
    bool ok = true;


    private void Start()
    {
        foreach (Transform child in transform)
        {
            if (child.gameObject.tag == "unsolved")
            {
                total_spheres++;
            }
        }

    }

    public int numberOfSpheres()
    {
        return total_spheres;
    }

    void Update()
    {
        current_spheres = 0;
        foreach (Transform child in transform)
        {
            if (child.gameObject.tag == "solved") 
            {
                current_spheres++;
            }
        }

        if (total_spheres == current_spheres && ok)
        {
            gameObject.BroadcastMessage("execution", false);
            ok = false;
        }
    }
}
