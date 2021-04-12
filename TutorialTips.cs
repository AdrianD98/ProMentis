using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialTips : MonoBehaviour
{
    public GameObject Tips;
    public string effect;
    public BreatheBar breatheBar;

    public float value;

    private void Start()
    {
        if (effect == "") effect = null;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Tips.SetActive(true);
            if (effect != null)
                breatheBar.setEffect(effect, value);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Tips.SetActive(false);
            if (effect != null)
                breatheBar.setEffect(effect, 0);
        }
    }

    private void OnDisable()
    {
        Tips.SetActive(false);
    }
}
