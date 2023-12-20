using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class VolumeButton : MonoBehaviour
{
    public AudioSource bgm;
    public GameObject bgm_on;
    public GameObject bgm_off;

    // Update is called once per frame
    public void click()
    {
        bgm.enabled = false;
        bgm_on.SetActive(false);
        bgm_off.SetActive(true);
    }
}
