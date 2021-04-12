using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FfSpawn : MonoBehaviour
{
    float size = 0;
    Vector3 vector = new Vector3();
    Vector3 oldsize;
    Material mat;

    // Start is called before the first frame update

    private void Awake()
    {
        mat = gameObject.GetComponent<Renderer>().material;
        oldsize = transform.localScale;
        vector.Set(oldsize.x * size, oldsize.y*size, oldsize.z*size);
        transform.localScale = vector;
    }

    void OnEnable()
    {
        size = 0;
        execution(true);
    }

    public void execution(bool ShouldSpawn)
    {
        
        if (ShouldSpawn)
        {
            InvokeRepeating("Spawn", 0.0f, 0.015f);
        }
        else
        {
            InvokeRepeating("DeSpawn", 0.0f, 0.015f);
        }
    }

    void Spawn()
    {
        if (size < 1)
        {
            size = size + 0.01f;
            vector.Set(oldsize.x * size, oldsize.y * size, oldsize.z * size);
            transform.localScale = vector;
        }
        else  CancelInvoke();
    }

    void DeSpawn()
    {
            size = size - 0.01f;
            vector.Set(oldsize.x * size, oldsize.y * size, oldsize.z * size);
            transform.localScale = vector;
            mat.SetColor("Colorx", Color.green * 8f);
        if (size <= 0) gameObject.SetActive(false);  
    }
}
