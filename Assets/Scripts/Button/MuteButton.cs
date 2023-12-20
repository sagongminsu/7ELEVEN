using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MuteButton : MonoBehaviour
{
    public AudioSource bgm;
    public GameObject bgm_on;
    public GameObject bgm_off;
    public void click()
    {
        bgm.enabled = true;
        bgm_on.SetActive(true);
        bgm_off.SetActive(false);
    }
}
