using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waterFog : MonoBehaviour
{
    public Color defaultColor;
    public bool lastLevel = false;
    Movement movement;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            movement = other.GetComponent<Movement>();
            if (!lastLevel) { Invoke("moveBack", 0.2f); }
            StartCoroutine(ChangeFog(0.20f, Color.blue));
        }
    }

    private void OnTriggerExit(Collider other)
    {
       
        if (other.CompareTag("Player"))
        {
            StopAllCoroutines();
            StartCoroutine(ChangeFog(0.01f, defaultColor));
        }
    }

    void moveBack()
    {
        movement.moveToLastPosition();
        StopAllCoroutines();
        StartCoroutine(ChangeFog(0.01f, defaultColor));
    }

    private IEnumerator ChangeFog(float targetDensity, Color color)
    {
        yield return new WaitForSeconds(0.1f);
        RenderSettings.fogColor = color;
        int increase = 1;

        if (targetDensity < RenderSettings.fogDensity)
        {
            increase = -1;
            while (RenderSettings.fogDensity > targetDensity)
            {
                RenderSettings.fogDensity += (0.001f * increase);
                yield return new WaitForSeconds(0.01f);
            }
        }

        else 
            while (RenderSettings.fogDensity < targetDensity)
        {
            RenderSettings.fogDensity += (0.001f * increase);
            yield return new WaitForSeconds(0.01f);
        }
    }
}
