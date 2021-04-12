using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TipsMovement : MonoBehaviour
{
    Vector3 initialPos;
    public bool Vertical;
    float timer;

    // Start is called before the first frame update
    void Start()
    {
        initialPos = transform.position;

        StartCoroutine(move());
    }


    private IEnumerator move()
    {
        while (true)
        {
            if (Vertical)
            transform.Translate(Vector3.down *  1f);
            else transform.Translate(Vector3.right * Time.deltaTime * 250);

            if (Time.time > timer)
            {
                if (Vertical)
                {
                    timer = Time.time + 1.1f; yield return new WaitForSeconds(1f);
                }

                else 
                {
                    timer = Time.time + 2f; yield return new WaitForSeconds(0.5f); 
                }
                    transform.position = initialPos;
            }

            yield return new WaitForSeconds(0.01f);
        }
    }

   

}
