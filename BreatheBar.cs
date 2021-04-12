using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;
using UnityEngine.Experimental.Rendering.LWRP;
using UnityEngine.Rendering.Universal;
using MidiPlayerTK;

public class BreatheBar : MonoBehaviour
{
    //UI elements
    public Slider slider;
    RectTransform cursorTransform;
    RectTransform anxietyTransform;
    public Image Aura;

    [SerializeField] Image cursor = default;
    [SerializeField] Image anxiety = default;

    //breathing variables
    int increasing = 1;
    float breathIncreaseSpeed = 0.025f;
    public float maximumBreath = 2.5f;

    //anxiety variables
    float anxietyVal = 0f;
    float currentAnxietyVal = 0f;

    //post processing elements
    Vignette vignette;
    DepthOfField dof;

    //post processing variables
    public float dofOffset = 10;
    int heatIncreasing;
    float AuraValue = 0f;
    bool ending = false;

    [SerializeField] int level;
    [SerializeField] int levelMax = 1;

    //outside components
    public CameraFollow cameraFollow;
    public MidiFilePlayer MidiFilePlayer;

    private void Start()
    {
        //get the ui components and set the values to their defaults
        slider.value = 0;

        cursorTransform = cursor.GetComponent<RectTransform>();
        anxietyTransform = anxiety.GetComponent<RectTransform>();

        Aura.material.SetFloat("Intensity", 0);

        //get the post processing components
        Volume volume = GameObject.Find("Post-process Volume").GetComponent<Volume>();
        volume.profile.TryGet<Vignette>(out vignette);
        volume.profile.TryGet<DepthOfField>(out dof);

        //run the scripts which set the values for each of the symptom in parallel 
        //timing is different on each script for performance optimization purposes
        InvokeRepeating("stabilizeBreath", 0.0f, 0.1f);
        InvokeRepeating("changeDoF", 0.0f, 0.3f);


        levelMax = FindObjectOfType<EventManager>().levelMax;
        InvokeRepeating("checkLevel", 1f, 1f);

        InvokeRepeating("UpdateAnxietyLevel", 1.2f, 0.1f);
        InvokeRepeating("changeAura", 0, 2f);

    }

    public void checkLevel()
    {
        level = FindObjectOfType<EventManager>().level;
    }

    //used on the learning island to trigger each of the effects
    public void setEffect(string effect, float power)
    {
        CancelInvoke();

        if (effect == "Aura")
        {
            AuraValue = power;
            Aura.material.SetFloat("Intensity", AuraValue);
        }

        if (effect == "Vignette")
        {
            vignette.intensity.value = power;
            dofOffset = power;
        }

        if (effect == "Trembling")
        {
            cameraFollow.setAnxiety(power / 200);
        }

        if (effect == "Anxiety")
        {
            maximumBreath = 0.8f;
            if (power == 0) maximumBreath = 2.5f;
        }
    }

        void changeAura()
        {
            if (currentAnxietyVal / 100 != AuraValue)
            {
                AuraValue = currentAnxietyVal / 30;
                Aura.material.SetFloat("Intensity", AuraValue);
            }
        }

        //check if breathing in or out
        void changeBreath()
        {
            if (slider.value > maximumBreath) { increasing = -1; MidiFilePlayer.MPTK_Previous(); }
            if (slider.value == 0) { increasing = 1; MidiFilePlayer.MPTK_Previous(); }
            slider.value += breathIncreaseSpeed * increasing;
        }
        
        //move the slider based on the breathLevel and anxiety value
        void stabilizeBreath()
        {

            anxietyVal = anxietyTransform.rect.xMin / 30 * -1;
            if (maximumBreath >= 3.25f && maximumBreath <= 4.2f)
            { if (currentAnxietyVal > anxietyVal / 2) currentAnxietyVal -= 0.01f; }
            else if (currentAnxietyVal < anxietyVal) currentAnxietyVal += 0.01f;

            if (maximumBreath > 5 - anxietyVal) maximumBreath -= 0.02f;
            else if (maximumBreath > 2.5f) maximumBreath -= 0.0025f;

            vignette.intensity.value = currentAnxietyVal / 4;

        }

    public void setVignetter()
    {
        ending = true;
        anxietyVal = 0f;
        currentAnxietyVal = 0f;
        anxietyTransform.offsetMin = new Vector2(301, 0);
        dof.focalLength.value = 1;
        gameObject.SetActive(false);
    }

    void changeDoF()
    {
        if (dofOffset >= 30 + currentAnxietyVal * 60) heatIncreasing = -1;
        if (dofOffset <= 15 + currentAnxietyVal * 20) heatIncreasing = 1;
        dofOffset += 10 * heatIncreasing;

        dof.focalLength.value = dofOffset;
    }

    private void FixedUpdate()
    {
        changeBreath();
        cursorTransform.anchoredPosition = new Vector3(maximumBreath * 60, -30f, 0);

        //move the cursor with the spacebar
        if (Input.GetButton("Jump") && maximumBreath <5) maximumBreath += 0.01f;
    }



    void UpdateAnxietyLevel()
    {
        //only running during the normal gameplay and not during the educational part
        if (!ending)
        {
            if (299f + anxietyTransform.rect.xMin * 2 > 300 - 250 / (levelMax - 2) * (level - 1))
            {
                anxietyTransform.offsetMin = new Vector2(anxietyTransform.offsetMin.x - 1f, anxietyTransform.offsetMin.y);
            }
            cameraFollow.setAnxiety(currentAnxietyVal / 200);

            Midi();
        }
    }

    void Midi()
    {
        MidiFilePlayer.MPTK_Speed = 1 / (maximumBreath);
    }

}
