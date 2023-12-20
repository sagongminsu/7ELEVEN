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
    public AudioClip[] moveAudioClip;
    public Coroutine MoveClipCoroutine;


    [Header ("Jump")]
    public AudioSource jumpAudioSource;
    public AudioClip jumpSound;

    [Header("Swing")]
    public AudioSource swingAudioSource;
    public AudioClip[] swingSound;
    public int swingNum = 0;

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
                PlaySFX(craftAudioSource, openSound, volume);
                break;
            case "Crafting":
                PlaySFX(craftAudioSource, craftingSound, volume);
                break;
            case "Failed":
                PlaySFX(craftAudioSource, failedSound, volume);
                break;
            case "ButtonClickSound":
                PlaySFX(craftAudioSource, buttonClickSound, volume);
                break;
            case "ButtonClickSoundFaile":
                PlaySFX(craftAudioSource, buttonClickSoundFaile, volume);
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
                PlaySFX(interactableAudioSource, getItemSound, volume);
                break;
            default:
                Debug.LogWarning("해당하는 오디오 클립을 찾을 수 없습니다.");
                return;
        }
    }

    public void JumpSound(float volume = 1.0f)
    {
            PlaySFX(swingAudioSource, jumpSound, volume);
    }

    public void SwingSound(float volume = 1.0f)
    {
        PlaySFX(swingAudioSource, swingSound[swingNum], volume);
        swingNum += 1;
            if(swingNum >= swingSound.Length)
            {
                swingNum = 0;
            }
    }
    public void PlaySFX(AudioSource audioSource,AudioClip audioClip ,float voluem)
    {
        audioSource.clip = audioClip;
        audioSource.volume = voluem;
        audioSource.Play();
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
