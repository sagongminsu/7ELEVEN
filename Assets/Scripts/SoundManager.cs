using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    int currentClipIndex = 0;
    private static SoundManager instance;
    private Coroutine playNextClipCoroutine;

    [Header("Craft")]
    public AudioSource craftAudioSource;
    public AudioClip openSound;
    public AudioClip craftingSound;
    public AudioClip failedSound;
    public AudioClip buttonClickSound;
    public AudioClip buttonClickSoundFaile;

    [Header("InteractableSound")]
    public AudioSource interactableAudioSource;
    public AudioClip getItemSound;

    [Header("MoveSound")]
    public AudioSource moveAudioSource;
    public AudioClip[] mopveAudioClip;
    public Coroutine MoveClipCoroutine;


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
            case "ButtonClickSound":
                craftAudioSource.clip = buttonClickSound;
                craftAudioSource.volume = volume;
                craftAudioSource.Play();
                break;
            case "ButtonClickSoundFaile":
                craftAudioSource.clip = buttonClickSoundFaile;
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


    private IEnumerator PlayNextClipCoroutine(AudioSource audioSource, AudioClip[] soundClips)
    {
        while (true)
        {
            if (currentClipIndex >= soundClips.Length)
            {
                currentClipIndex = 0;
            }

            audioSource.clip = soundClips[currentClipIndex];
            audioSource.Play();

            currentClipIndex++;

            yield return new WaitForSeconds(0.5f);
        }
    }

    public void StartPlayNextClip(AudioSource audioSource, AudioClip[] soundClips, bool isCheck)
    {
        if (isCheck)
        {
            if (playNextClipCoroutine == null)
            {
                playNextClipCoroutine = StartCoroutine(PlayNextClipCoroutine(audioSource, soundClips));
            }
        }
        else
        {
            if (playNextClipCoroutine != null)
            {
                StopCoroutine(playNextClipCoroutine);
                playNextClipCoroutine = null;
            }
        }
    }
}
