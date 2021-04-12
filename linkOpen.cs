using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class linkOpen : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F)) Application.OpenURL("https://forms.office.com/r/ACiVj0TSRb");
        if (Input.GetKeyDown(KeyCode.G)) Application.OpenURL("https://www.anxietyuk.org.uk/");
    }
}
