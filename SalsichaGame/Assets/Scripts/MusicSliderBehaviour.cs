using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicSliderBehaviour : MonoBehaviour
{

    public Slider musicSlider;
    AudioSource musicSource;

    void Start()
    {
        musicSource = GameObject.FindGameObjectWithTag("Music").GetComponent<AudioSource>();
        musicSlider = GetComponent<Slider>();
        musicSlider.value = musicSource.volume;
    }

    private void Update()
    {
        OnValueChanged();
    }

    public void OnValueChanged()
    {
        MusicManager.SetVolume(musicSlider.value);
    }
}
