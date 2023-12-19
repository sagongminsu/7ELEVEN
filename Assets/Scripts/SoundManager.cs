using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private static SoundManager instance;

    [Header("Craft")]
    public AudioSource craftAudioSource;
    public AudioClip openSound;
    public AudioClip craftingSound;
    public AudioClip failedSound;

    [Header("interactableSound")]
    public AudioSource interactableAudioSource;
    public AudioClip getItemSound;

    public static SoundManager Instance
    {
        get { return instance; }
    }

    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void CraftAudio(string name, float volume = 1.0f)
    {
        switch (name)
        {
            case "Open":
                craftAudioSource.clip = openSound;
                craftAudioSource.volume = volume;
                craftAudioSource.Play();
                break;
            case "Crafting":
                craftAudioSource.clip = craftingSound;
                craftAudioSource.volume = volume;
                craftAudioSource.Play();
                break;
            case "Failed":
                craftAudioSource.clip = failedSound;
                craftAudioSource.volume = volume;
                craftAudioSource.Play();
                break;
            default:
                Debug.LogWarning("해당하는 오디오 클립을 찾을 수 없습니다.");
                return;
        }
    }

    public void GetItemSound(string name, float volume = 1.0f)
    {
        switch (name)
        {
            case "GetItem":
                interactableAudioSource.clip = getItemSound;
                interactableAudioSource.volume = volume;
                interactableAudioSource.Play();
                break;
            default:
                Debug.LogWarning("해당하는 오디오 클립을 찾을 수 없습니다.");
                return;
        }
    }
}
