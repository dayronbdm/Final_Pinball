/* using UnityEngine;

public class Circle : MonoBehaviour
{

    private float lightValue;
    private new Light light;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        light = GetComponent<Light>();
    }


    void Update()
    {
        if (lightValue > 0)
        {
            lightValue -= Time.deltaTime * 10000;
            if (lightValue>5000)
            {
                light.intensity = (6000-lightValue) * 1.25f;
            }
            else
            {
                light.intensity = lightValue * 0.5f;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Game.Instance.IncreaseScore(300);
        lightValue = 6000;
    }

    // Update is called once per frame
    /*void Update()
    {
        if (lightValue > 0)
        {
            lightValue -= Time.deltaTime * 10000; // Decreasing over time
            if (lightValue < 0) lightValue = 0;
            
            // Map lightValue (0 to 12000) to intensity (0 to peak)
            // Peak at 12000, fading down to 0
            light.intensity = (lightValue / 12000f) * 50f; // Adjusted scale
            light.enabled = lightValue > 0;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            lightValue = 12000f;
            if (light != null)
            {
                light.enabled = true;
                light.intensity = lightValue;
            }
        }
    }
}*/
 
 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Circle : MonoBehaviour
{
    private float lightValue;
    private new Light light;
    [SerializeField] AudioSource soundCircle;

    // Start is called before the first frame update
    void Start()
    {
        light = GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        if (lightValue > 0)
        {
            lightValue -= Time.deltaTime * 10000;
            if (lightValue>5000)
            {
                light.intensity = (6000-lightValue) * 1.25f;
            }
            else
            {
                light.intensity = lightValue * 0.5f;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        soundCircle.Play();
        Game.Instance.IncreaseScore(300);
        lightValue = 6000;
    }
}