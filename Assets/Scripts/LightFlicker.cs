using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightFlicker : MonoBehaviour
{
    public new Light2D light;
    public Vector2 innerRadiusRange = new(1, 2.7f);
    public Vector2 outerRadiusRange = new(3, 3.7f);

    void Update()
    {
        light.pointLightInnerRadius = Mathf.Lerp(light.pointLightInnerRadius, Random.Range(innerRadiusRange.x, innerRadiusRange.y), Time.deltaTime * 2f);
        light.pointLightOuterRadius = Mathf.Lerp(light.pointLightOuterRadius, Random.Range(outerRadiusRange.x, outerRadiusRange.y), Time.deltaTime * 2f);
    }
}
