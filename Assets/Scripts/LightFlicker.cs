using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightFlicker : MonoBehaviour
{
    public new Light2D light;

    void Update()
    {
        light.pointLightInnerRadius = Mathf.Lerp(light.pointLightInnerRadius, Random.Range(1, 2.7f), Time.deltaTime * 2f);
        light.pointLightOuterRadius = Mathf.Lerp(light.pointLightOuterRadius, Random.Range(3, 3.7f), Time.deltaTime * 2f);
    }
}
