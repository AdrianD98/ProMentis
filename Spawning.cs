using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawning : MonoBehaviour
{
    Material _mat;
    float dissolve = -0.2f;

  
    // Start is called before the first frame update
    void OnEnable()
    {
        dissolve = 1.1f;
        execution(true);
    }


    public void execution(bool ShouldSpawn)
    {
        _mat = GetComponent<Renderer>().material;

        if (ShouldSpawn)
        {
            dissolve = 1.1f;
            InvokeRepeating("Spawn", 0.0f, 0.015f);
        }
        else
        {
            InvokeRepeating("DeSpawn", 0.0f, 0.015f);
        }
    }

    void Spawn()
    {
        if (dissolve > -0.2f)
        {
            _mat.SetFloat("Dissolve", dissolve);
            dissolve -= 0.01f;
        }
        else
        {
            if (tag == "Puzzle")
            {
                for (int i = 0; i < transform.childCount; i++)
                {
                    transform.GetChild(i).gameObject.SetActive(true);
                }
            }
            CancelInvoke();
        }
    }

    void DeSpawn()
    {
        if (dissolve < 1.1f)
        {
            _mat.SetFloat("Dissolve", dissolve);
            
            dissolve = dissolve + 0.01f;
        }
        else this.gameObject.SetActive(false);
    }
}
