using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class DayNightCycle : MonoBehaviour
{
    public Volume dayNightVolume;
    public float dayDuration = 240f;

    private float cycleTime = 0f;
    void FixedUpdate()
    {
        cycleTime += Time.deltaTime;
        float normalizedTime = (cycleTime % dayDuration) / dayDuration;
        // Use a different function to slow down near weight = 0
        float weight = Mathf.Pow(Mathf.Sin(normalizedTime * Mathf.PI), 4);
        dayNightVolume.weight = weight;
    }
}
