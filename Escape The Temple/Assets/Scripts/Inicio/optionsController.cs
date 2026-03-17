using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class optionsController : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Slider musicSlider;
    public Slider sfxSlider;
    public Slider sensitivitySlider;
    void Start()
    {
        float music = PlayerPrefs.GetFloat("Music_Volume", 1f);
        float sfx = PlayerPrefs.GetFloat("SFX_Volume", 1f);
        float sensitivity = PlayerPrefs.GetFloat("Sensitivity", 100f);

        audioMixer.SetFloat("MusicVolume", Mathf.Log10(music) * 20);
        audioMixer.SetFloat("SFXvolume", Mathf.Log10(sfx) * 20);

        music = Mathf.Clamp(music, 0.0001f, 1f);
        sfx = Mathf.Clamp(sfx, 0.0001f, 1f);
        //musicSlider.value = music;
        //sfxSlider.value = sfx;
        sensitivitySlider.value = sensitivity;
    }

    public void SetSFXVolume(float volume)
    {
        float safeVolume = Mathf.Clamp(volume, 0.0001f, 1f);
        audioMixer.SetFloat("SFXvolume", Mathf.Log10(safeVolume) * 20);
        PlayerPrefs.SetFloat("SFX_Volume", volume);
    }

    public void SetSensitivity(float sensitivity)
    {
        PlayerPrefs.SetFloat("Sensitivity", sensitivity);
    }

    public void SetMusicVolume(float volume)
    {
        float safeVolume = Mathf.Clamp(volume, 0.0001f, 1f);
        audioMixer.SetFloat("MusicVolume", Mathf.Log10(safeVolume) * 20);
        PlayerPrefs.SetFloat("Music_Volume", volume);
    }
}
