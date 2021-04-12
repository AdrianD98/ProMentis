using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 playerVelocity;
    Animator animator;

    private bool groundedPlayer;
    private float playerSpeed = 4.0f;
    private float gravityValue = -9.81f;

    public Vector3 lastPosition = new Vector3 (12.96f,4.06f,13.35f);


    private void Start()
    {
        //set the position of the mouse to the center of the screen
        var mousePos = Input.mousePosition;
        mousePos.x -= Screen.width / 2;
        mousePos.y -= Screen.height / 2;
        
        controller = gameObject.GetComponent<CharacterController>();
        animator = gameObject.GetComponent<Animator>();
    }

    public void moveToLastPosition()
    {
        controller.enabled = false;
        transform.position = lastPosition;
        controller.enabled = true;
    }

    void Update()
    {
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y <= 0)
        {
            animator.SetBool("InAir", false);
            playerVelocity.y -= gravityValue *Time.deltaTime;
        }

        if(playerVelocity.y > 0)
        {
            animator.SetBool("InAir", true);
        }

        float horizontalInput = Input.GetAxis("Horizontal")/2;
        float verticalInput = Input.GetAxis("Vertical")/2;

        // Calculate the Direction to Move based on the tranform of the Player
        Vector3 moveDirectionForward = transform.forward * verticalInput;
        Vector3 moveDirectionSide = transform.right * horizontalInput;

        //find the direction
        Vector3 direction = (moveDirectionForward + moveDirectionSide).normalized;
        //find the distance
        Vector3 distance = direction * playerSpeed * Time.deltaTime;

        // Apply Movement to Player
        controller.Move(distance);


        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }
}

