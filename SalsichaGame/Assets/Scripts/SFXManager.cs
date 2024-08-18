using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    private static SFXManager Instance;

    private static AudioSource audioSource;
    private static SFXLibraryManager sfxLibraryManager;

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

    public static void PlaySFX(string soundName, float volume)
    {
        AudioClip audioClip = sfxLibraryManager.GetRandomClip(soundName);
        if (audioClip != null)
        {
            audioSource.volume = volume;
            audioSource.PlayOneShot(audioClip);
        }
    }
}
