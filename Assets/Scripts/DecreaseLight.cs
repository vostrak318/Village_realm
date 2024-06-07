using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class DecreaseLight : MonoBehaviour
{
    public Volume dayNightVolume;
    public Light2D pointLight;

    public float maxIntensity = 18f;
    public float minIntensity = 1f;
    private void FixedUpdate()
    {
        if (dayNightVolume.weight > 0)
        {
            pointLight.intensity = Mathf.Lerp(minIntensity, maxIntensity, dayNightVolume.weight);
        }
        else
        {
            pointLight.intensity = minIntensity;
        }
    }
}
