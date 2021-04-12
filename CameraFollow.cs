using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float mouseSensitivity = 150f;
    public Transform playerBody;
    float xRotation = 0f;

    float randomX= 0f;
    float randomY= 0f;
    public float anxiety = 0f;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        InvokeRepeating("randomizePosition", 0, 2f);
    }

    //setAnxiety and randomizePosition are used to move the cursor in random directions creating the symptom of trembling
    public void setAnxiety(float anx)
    {
        anxiety = anx;
    }

    void randomizePosition()
    {
        randomX = Random.Range(-anxiety, anxiety);
        randomY = Random.Range(-anxiety, anxiety);
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime + randomX;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime + randomY;


        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -80f, 55f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);
    }
}
