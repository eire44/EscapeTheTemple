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
        //float volume = PlayerPrefs.GetFloat("Volume", 1f);
        //audioMixer.SetFloat("MasterVolume", Mathf.Log10(volume) * 20);
        float music = PlayerPrefs.GetFloat("Music_Volume", 1f);
        float sfx = PlayerPrefs.GetFloat("SFX_Volume", 1f);
        float sensitivity = PlayerPrefs.GetFloat("Sensitivity", 100f);

        audioMixer.SetFloat("MusicVolume", Mathf.Log10(music) * 20);
        audioMixer.SetFloat("SFXvolume", Mathf.Log10(sfx) * 20);
        //SetMusicVolume(music);
        //SetSFXVolume(sfx);
        //SetSensitivity(sensitivity);

        musicSlider.value = music;
        sfxSlider.value = sfx;
        sensitivitySlider.value = sensitivity;

        Debug.Log("musica: " + music);
        Debug.Log("sfx: " + sfx);
        Debug.Log("sensitivity: " + sensitivity);
    }

    public void SetSFXVolume(float volume)
    {
        Debug.Log("Raw slider value: " + volume);
        float safeVolume = Mathf.Clamp(volume, 0.0001f, 1f);
        Debug.Log("new value: " + safeVolume);
        audioMixer.SetFloat("SFXvolume", Mathf.Log10(safeVolume) * 20);
        PlayerPrefs.SetFloat("SFX_Volume", volume);
    }

    public void SetSensitivity(float sensitivity)
    {
        Debug.Log("new value: " + sensitivity);
        PlayerPrefs.SetFloat("Sensitivity", sensitivity);
    }

    public void SetMusicVolume(float volume)
    {
        Debug.Log("Raw slider value: " + volume);
        float safeVolume = Mathf.Clamp(volume, 0.0001f, 1f);
        Debug.Log("new value: " + safeVolume);
        audioMixer.SetFloat("MusicVolume", Mathf.Log10(safeVolume) * 20);
        PlayerPrefs.SetFloat("Music_Volume", volume);
    }
}
