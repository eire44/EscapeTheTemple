using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class optionsController : MonoBehaviour
{
    public AudioMixer audioMixer;
    void Start()
    {
        float volume = PlayerPrefs.GetFloat("Volume", 1f);
        audioMixer.SetFloat("MasterVolume", Mathf.Log10(volume) * 20);
    }

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("MasterVolume", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("Volume", volume);
    }

    public void SetSensitivity(float sensitivity)
    {
        PlayerPrefs.SetFloat("Sensitivity", sensitivity);
    }
}
