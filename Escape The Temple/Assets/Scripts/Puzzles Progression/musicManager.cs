using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class musicManager : MonoBehaviour
{
    public AudioMixer audioMixer;
    public AudioSource[] musicSources;
    void Start()
    {
        LoadAudioSettings();
    }

    void LoadAudioSettings()
    {
        float music = PlayerPrefs.GetFloat("Music_Volume", 0.75f);
        float sfx = PlayerPrefs.GetFloat("SFX_Volume", 1f);
        float sensitivity = PlayerPrefs.GetFloat("Sensitivity", 100f);

        //float dbVolume = Mathf.Log10(music) * 20;
        audioMixer.SetFloat("MusicVolume", Mathf.Log10(Mathf.Max(0.0001f, music)) * 40);
        //float dbVolumeSFX = Mathf.Log10(sfx) * 20;
        audioMixer.SetFloat("SFXvolume", Mathf.Log10(Mathf.Max(0.0001f, sfx)) * 40);
        //audioMixer.SetFloat("MusicVolume", Mathf.Log10(Mathf.Max(0.0001f, music)) * 20);
        //audioMixer.SetFloat("SFXvolume", Mathf.Log10(Mathf.Max(0.0001f, sfx)) * 20);
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
}
