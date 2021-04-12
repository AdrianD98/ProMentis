using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    public Image ScreenDarken;
    bool starting = false;
    bool advanced = false;
    public GameObject text1;
    public GameObject text2;
    public GameObject breathingBar;
    
    // Start is called before the first frame update
    void Awake()
    {
        Time.timeScale = 0;
    }
   
    private IEnumerator DarkenScreen()
    {
        Time.timeScale = 1;
        float timer = 2f;
        Color color = ScreenDarken.color;

        while (timer > 0 && color.a > 0)
        {
            color.a -= 0.02f;
            timer -= 0.01f;
            ScreenDarken.color = color;
            yield return new WaitForSeconds(0.01f);
        }

        gameObject.SetActive(false);
    }

    private void Update()
    {
        if (!starting)
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (advanced) 
                { 
                StartCoroutine(DarkenScreen());
                starting = true;
                }
                else
                {
                    text1.SetActive(false);
                    if (text2 != null)
                    text2.SetActive(true);
                    if (breathingBar != null)
                    breathingBar.SetActive(true);
                    advanced = true;
                }
            }
    }
}
