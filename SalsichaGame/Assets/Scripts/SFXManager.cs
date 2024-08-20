using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SFXManager : MonoBehaviour
{
    private static SFXManager Instance;

    private static AudioSource audioSource;
    private static SFXLibraryManager sfxLibraryManager;

    public bool endgame = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            audioSource = GetComponent<AudioSource>();
            sfxLibraryManager = GetComponent<SFXLibraryManager>();
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public static void PlaySFX(string soundName)
    {
        AudioClip audioClip = sfxLibraryManager.GetRandomClip(soundName);
        if (audioClip != null)
        {
            audioSource.PlayOneShot(audioClip);
        }
    }

    public static void SetVolume(float volume)
    {
        audioSource.volume = volume;
    }
}
