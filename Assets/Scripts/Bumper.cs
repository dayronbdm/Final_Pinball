/* using UnityEngine;

public class Bumper : MonoBehaviour
{

    [SerializeField] private new Light light;
    private float time_left_LighShine;
    AudioSource soundBumper;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        soundBumper = GameObject.Find("Sound/bumper").GetComponent<AudioSource>();

        if (light == null)
        {
            light = GetComponentInChildren<Light>();
        }
        if (light != null)
        {
            light.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (time_left_LighShine > 0)
        {
            time_left_LighShine -= Time.deltaTime;
            if (time_left_LighShine <= 0 && light != null)
            {
                light.enabled = false;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        soundBumper.Play();
        Game.Instance.IncreaseScore(1500);
        if (collision.collider.CompareTag("Ball"))
        {
            Rigidbody rb = collision.collider.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(7000f, transform.position, 8);
            }
            
            if (light != null)
            {
                light.enabled = true;
                time_left_LighShine = 0.2f;
            }
        }
    }
} */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bumper : MonoBehaviour
{
    AudioSource soundBumper;
    [SerializeField] private new Light light;
    private float timeLeftLightShine;

    // Start is called before the first frame update
    void Start()
    {
        soundBumper = GameObject.Find("Sound/bumper").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if( timeLeftLightShine > 0)
        {
            timeLeftLightShine -= Time.deltaTime;
            if( timeLeftLightShine < 0)
            {
                light.enabled = false;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        soundBumper.Play();
        collision.collider.GetComponent<Rigidbody>().AddExplosionForce(7000f, transform.position, 8);
        Game.Instance.IncreaseScore(10);
        light.enabled = true;
        timeLeftLightShine = 0.2f;
    }
}