using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyboxExposureController : MonoBehaviour
{
    public float startExposure = 0f;
    public float endExposure = 1.2f;
    public float transitionDuration = 60f; // 초 단위, 조절 가능

    private Material skyboxMaterial;
    private float currentTime = 0f;
    private bool isTransitioning = false;

    private void Start()
    {
        skyboxMaterial = RenderSettings.skybox;
        skyboxMaterial.SetFloat("_Exposure", startExposure);
    }

    private void Update()
    {
        if (!isTransitioning)
        {
            currentTime += Time.deltaTime;

            if (currentTime >= transitionDuration)
            {
                currentTime = 0f;
                isTransitioning = true;
            }

            float t = currentTime / transitionDuration;
            float newExposure = Mathf.Lerp(startExposure, endExposure, t);
            skyboxMaterial.SetFloat("_Exposure", newExposure);
        }
        else
        {
            currentTime += Time.deltaTime;

            if (currentTime >= transitionDuration)
            {
                currentTime = 0f;
                isTransitioning = false;
            }

            float t = currentTime / transitionDuration;
            float newExposure = Mathf.Lerp(endExposure, startExposure, t);
            skyboxMaterial.SetFloat("_Exposure", newExposure);
        }
    }
}

