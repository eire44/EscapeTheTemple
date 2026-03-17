using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class ESP_AudioClueController : MonoBehaviour
{
    public AudioSource audioClue;
    bool play = false;


    public AudioMixer mixer;

    public float duckedVolume = -20f; // volumen mientras suena la clue
    public float normalVolume = 0f;
    public float fadeTime = 1f;
    float originalSFX;
    float originalMusic;

    public void playAudioClue()
    {
        play = !play;
        if (play)
        {
            StartCoroutine(DuckAudio());
            audioClue.Play();
        }
        else
        {
            audioClue.Stop();
            StartCoroutine(RestoreAudio());
        }
    }

    public void CacheVolumes()
    {
        mixer.GetFloat("SFXvolume", out originalSFX);
        mixer.GetFloat("MusicVolume", out originalMusic);
    }


    public IEnumerator DuckAudio()
    {
        CacheVolumes();
        yield return StartCoroutine(FadeMixer("SFXvolume", originalSFX - 20f));
        yield return StartCoroutine(FadeMixer("MusicVolume", originalMusic - 20f));
    }

    public IEnumerator RestoreAudio()
    {
        yield return StartCoroutine(FadeMixer("SFXvolume", originalSFX));
        yield return StartCoroutine(FadeMixer("MusicVolume", originalMusic));
    }

    IEnumerator FadeMixer(string parameter, float target)
    {
        mixer.GetFloat(parameter, out float current);

        float time = 0f;

        while (time < fadeTime)
        {
            time += Time.deltaTime;
            float value = Mathf.Lerp(current, target, time / fadeTime);
            mixer.SetFloat(parameter, value);
            yield return null;
        }

        mixer.SetFloat(parameter, target);
    }
}
