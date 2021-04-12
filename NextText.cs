using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NextText : MonoBehaviour
{
    public GameObject island;
    public Movement movement;
    public CameraFollow cameraFollow;
    public BreatheBar breatheBar;

    

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            island.SetActive(true);

            cameraFollow.enabled = true;
            movement.enabled = true;
            movement.lastPosition = new Vector3(7.3f, 4.06f, -89.2f);
            movement.moveToLastPosition();
            movement.gameObject.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
            breatheBar.gameObject.SetActive(true);

            gameObject.SetActive(false);
        }
    }
}
