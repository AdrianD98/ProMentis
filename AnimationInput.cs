using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationInput : MonoBehaviour
{

    public Animator animator;
    public float InputX;
    public float InputY;

    // Start is called before the first frame update
    void Start()
    {
        animator = this.gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        InputY = Input.GetAxis("Vertical");
        InputX = Input.GetAxis("Horizontal");

        animator.SetFloat("InputX", InputX);
        animator.SetFloat("InputY", InputY);

        if (InputX != 0 || InputY != 0)
            animator.SetBool("Walking", true);
        else animator.SetBool("Walking", false);

        
    }
}
