using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExampleScript : MonoBehaviour
{
    private AnimationCurve curve;

    private void Start()
    {

        curve = new AnimationCurve();


        curve.AddKey(0f, 0f);
        curve.AddKey(1f, 1f);
    }

    private void Update()
    {
        // 시간에 따라 값을 보간하여 출력
        float time = Time.time;
        float value = curve.Evaluate(time);
        Debug.Log("Time: " + time + ", Value: " + value);
    }
}
