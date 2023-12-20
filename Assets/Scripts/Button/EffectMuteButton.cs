using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EffectMuteButton : MonoBehaviour
{
    public AudioSource effect1;
    public AudioSource effect2;
    public AudioSource effect3;

    public GameObject effect_on;
    public GameObject effect_off;

    // Update is called once per frame
    public void click()
    {
        effect1.enabled = true;
        effect2.enabled = true;
        effect3.enabled = true;
        effect_on.SetActive(true);
        effect_off.SetActive(false);
    }
}
