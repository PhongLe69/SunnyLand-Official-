using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class ControlLight2D : MonoBehaviour
{
    private Light2D myLight;
    private float spawnDelay;


    void Start()
    {
        myLight = GetComponent<Light2D>();
        StartCoroutine(Twinkling());
    }

    void Update()
    {
        spawnDelay = Random.Range(0, 0.1F);
    }

    IEnumerator Twinkling()
    {
        yield return new WaitForSeconds(spawnDelay);
        myLight.intensity = Random.Range(2.0F, 3F);
        myLight.pointLightInnerRadius = Random.Range(0, 0.1F);
        myLight.pointLightOuterRadius = Random.Range(2.5F, 2.7F);
        StartCoroutine(Twinkling());
    }
}
