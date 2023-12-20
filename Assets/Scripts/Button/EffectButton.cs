using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EffectButton : MonoBehaviour
{
    public AudioSource effect1;
    public AudioSource effect2;
    public AudioSource effect3;

    public GameObject effect_on;
    public GameObject effect_off;

    // Update is called once per frame
    public void click()
    {
        effect1.enabled = false;
        effect2.enabled = false;
        effect3.enabled = false;
        effect_on.SetActive(false);
        effect_off.SetActive(true);
    }
}
