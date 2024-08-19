using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderBehaviour : MonoBehaviour
{

    public Slider sfxSlider;
    AudioSource sfxSource;

    void Start()
    {
        sfxSource = GameObject.FindGameObjectWithTag("SFX").GetComponent<AudioSource>();
        sfxSlider = GetComponent<Slider>();
        sfxSlider.value = sfxSource.volume;
    }

    private void Update()
    {
        OnValueChanged();
    }

    public void OnValueChanged()
    {
        SFXManager.SetVolume(sfxSlider.value);
    }
}
