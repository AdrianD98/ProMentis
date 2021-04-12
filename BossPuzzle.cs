using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossPuzzle : MonoBehaviour
{
    public Transform parentTransform;
    public Image ScreenDarken;
    public BreatheBar breatheBar;
    public bool win = false;
    public Transform player;
    public Transform water;
    public bool inside;
    public GameObject indications;
    public GameObject endingText;
    public GameObject crosshair;
    Image endingImage;

    private void Start()
    {
        endingImage = endingText.GetComponent<Image>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            water.GetComponent<waterFog>().lastLevel = true;

            if (breatheBar.maximumBreath > 2.5f)  breatheBar.maximumBreath = 2.5f; 

            inside = true; 
            other.GetComponent<Movement>().enabled = false;
            other.GetComponent<AnimationInput>().enabled = false;
            other.GetComponent<Animator>().SetBool("Walking", false);

            player = other.transform;
            StartCoroutine(MoveUp(other));
            StartCoroutine(DarkenScreen(false));
            //show space bar
        }
        
    }

    private IEnumerator MoveUp(Collider other)
    {
        float timer = 4f;
        while (timer > 0)
        {
            
            water.Translate(Vector3.up * Time.deltaTime * 12, Space.World);

            if (timer < 2f)
            {
                parentTransform.Translate(Vector3.up * Time.deltaTime * 0.8f, Space.World);
                other.transform.Translate(Vector3.up * Time.deltaTime * 2f, Space.World);
                other.transform.Rotate(new Vector3(1, 0, 1), 0.2f);
                other.transform.position = Vector3.MoveTowards(other.transform.position, transform.position, 2f * Time.deltaTime);
            }

            timer -= 0.01f;
            yield return new WaitForSeconds(0.016f);
        }
    }

    private IEnumerator DarkenScreen(bool win)
    {
        float timer = 12f;
        if (win) timer = 2f;
        Color color = ScreenDarken.color;

        if (win) color = Color.white; 
        color.a = 0;
        
        
        while (timer > 0)
        {
            color.a += 0.001f;
            timer -= 0.01f;
            if (win) color.a += 0.01f;
            ScreenDarken.color = color;
            yield return new WaitForSeconds(0.016f);
        }
        if (!win) indications.SetActive(true);

        if (win)
        {
            timer = 3f;
            player.GetComponentInChildren<CameraFollow>().enabled = false;
            player.transform.Rotate(new Vector3(1, 0, 1), -1f);
            player.position = new Vector3(14.47f, 3.48f, 12.64f);
           
            player.localEulerAngles = new Vector3(-37.5f, -232.7f, 0f);
            water.position = new Vector3(63.6f, 1.98f, 37.3f);
            RenderSettings.fogDensity = 0.01f;
           
            crosshair.SetActive(false);

            breatheBar.setVignetter();


            while (timer > 0)
            {
                color.a -= 0.01f;
                timer -= 0.01f;
                ScreenDarken.color = color;
                yield return new WaitForSeconds(0.016f);
            }

            timer = 2f;

            endingText.SetActive(true);


            while (timer > 0)
            {
                color.a += 0.1f;
                timer -= 0.01f;
                endingImage.color = color;
                yield return new WaitForSeconds(0.016f);
            }

            
            ScreenDarken.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        if (breatheBar.maximumBreath >=4.5f && win == false && inside)
        {
            win = true;
            StopAllCoroutines();
            StartCoroutine(DarkenScreen(true));
        }
    }
}
