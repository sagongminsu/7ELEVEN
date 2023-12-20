using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundAnimationEvent : MonoBehaviour
{
    public void Attack()
    {
        SoundManager.Instance.SwingSound();
    }

    void Start()
    {
        RemoveCloneFromObjectName();
    }

    void RemoveCloneFromObjectName()
    {
        if (gameObject.name.Contains("(Clone)"))
        {
            gameObject.name = gameObject.name.Replace("(Clone)", "");
        }
    }
}
