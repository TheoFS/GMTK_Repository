using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicLibraryManager : MonoBehaviour
{
    [SerializeField] private SoundEffectGroup[] soundEffectGroups;
    //Criação de um dicionário para armazenar audios
    private Dictionary<string, List<AudioClip>> soundDictionary;

    private void Awake()
    {
        InitializeDictionary();
    }

    private void InitializeDictionary()
    {
        soundDictionary = new Dictionary<string, List<AudioClip>>();
        foreach (SoundEffectGroup soundEffectGroup in soundEffectGroups)
        {
            soundDictionary[soundEffectGroup.name] = soundEffectGroup.audioClips;
        }
    }

    public AudioClip GetRandomClip(string name)
    {
        if (soundDictionary.ContainsKey(name))
        {
            List<AudioClip> audioClips = soundDictionary[name];
            if (audioClips.Count > 0)
            {
                return audioClips[UnityEngine.Random.Range(0, audioClips.Count)];
            }
        }
        return null;
    }
}




//Classe que pode ser vista no inspetor e utilizada para adicionar os sons na biblioteca
[System.Serializable]
public struct SoundEffectGroup2
{
    public string name;
    public List<AudioClip> audioClips;
}
