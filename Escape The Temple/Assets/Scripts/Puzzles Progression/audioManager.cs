using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class audioManager : MonoBehaviour
{
    public AudioMixer audioMixer;
    public AudioSource[] musicSources;

    public Slider musicSlider;
    public Slider sfxSlider;
    public Slider sensitivitySlider;
    public Mov_Controller movController;
    void Start()
    {
        LoadAudioSettings();
        movController = FindObjectOfType<Mov_Controller>();
    }

    void LoadAudioSettings()
    {
        float music = PlayerPrefs.GetFloat("Music_Volume", 1f);
        float sfx = PlayerPrefs.GetFloat("SFX_Volume", 1f);
        float sensitivity = PlayerPrefs.GetFloat("Sensitivity", 100f);


        music = Mathf.Clamp(music, 0.0001f, 1f);
        sfx = Mathf.Clamp(sfx, 0.0001f, 1f);

        float musicCurved = music * music;
        float sfxCurved = sfx * sfx;

        audioMixer.SetFloat("MusicVolume", Mathf.Log10(musicCurved) * 20);
        audioMixer.SetFloat("SFXvolume", Mathf.Log10(sfxCurved) * 20);


        musicSlider.value = music;
        sfxSlider.value = sfx;
        sensitivitySlider.value = sensitivity;
    }
    public void changeMusic(bool ending)
    {
        if(ending)
        {
            musicSources[0].Stop();
            musicSources[1].Stop();
            musicSources[2].Play();
        }
        else
        {
            if (musicSources[0].isPlaying)
            {
                musicSources[0].Stop();
                musicSources[1].Play();
            }
            else if (musicSources[1].isPlaying)
            {
                musicSources[1].Stop();
                musicSources[0].Play();
            }
        }
    }

    public void SetSFXVolume(float volume)
    {
        float curved = volume * volume;

        float safeVolume = Mathf.Clamp(curved, 0.0001f, 1f);
        //float safeVolume = Mathf.Clamp(volume, 0.0001f, 1f);
        audioMixer.SetFloat("SFXvolume", Mathf.Log10(safeVolume) * 20);
        PlayerPrefs.SetFloat("SFX_Volume", volume);
    }

    public void SetSensitivity(float sensitivity)
    {
        PlayerPrefs.SetFloat("Sensitivity", sensitivity);
        movController.mouseSensitivity = sensitivity;
    }

    public void SetMusicVolume(float volume)
    {
        float curved = volume * volume;

        float safeVolume = Mathf.Clamp(curved, 0.0001f, 1f);
        audioMixer.SetFloat("MusicVolume", Mathf.Log10(safeVolume) * 20);
        PlayerPrefs.SetFloat("Music_Volume", volume);
    }
}
