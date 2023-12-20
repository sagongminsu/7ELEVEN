using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    [Range(0.0f, 60f)]
    public float time;
    public float fullDayLength;
    public float startTime = 0.2f;
    private float timeRate;
    public Vector3 noon;

    



    [Header("Sun")]
    public Light sun;
    public Gradient sunColor;
    public AnimationCurve sunIntensity;

    [Header("Moon")]
    public Light moon;
    public Gradient moonColor;
    public AnimationCurve moonIntensity;




    [Header("Other Lighting")]
    public AnimationCurve lightingIntensityMultiplier;
    public AnimationCurve reflectionIntensityMultiplier;

    private void Start()
    {
        timeRate = 0.3f / fullDayLength;
        time = startTime;
    }

    private void Update()
    {
        time = (time + timeRate * Time.deltaTime) % 60f;

        UpdateLighting(sun, sunColor, sunIntensity, 30f);
        UpdateLighting(moon, moonColor, moonIntensity, 30f);

        RenderSettings.ambientIntensity = lightingIntensityMultiplier.Evaluate(time);
        RenderSettings.reflectionIntensity = reflectionIntensityMultiplier.Evaluate(time);
    }

    void UpdateLighting(Light lightSource, Gradient colorGradiant, AnimationCurve intensityCurve, float duration)
    {
        float intensity = intensityCurve.Evaluate(time);

        lightSource.transform.eulerAngles = (time - (lightSource == sun ? 30f : 30f)) * noon * 4.0f;
        lightSource.color = colorGradiant.Evaluate(time);
        lightSource.intensity = intensity;

        GameObject go = lightSource.gameObject;

        // Check if it's time to activate or deactivate the light
        if (time < duration && lightSource.intensity == 0 && go.activeInHierarchy)
            go.SetActive(false);
        else if (time >= duration && lightSource.intensity > 0 && !go.activeInHierarchy)
            go.SetActive(true);
    }


}
