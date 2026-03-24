using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    bool cached = false;

    void Update()
    {
        if (play && !audioClue.isPlaying)
        {
            play = false;
            StartCoroutine(RestoreAudio());
        }
    }

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
        if (cached) return;

        mixer.GetFloat("SFXbolume", out originalSFX);
        mixer.GetFloat("MusicVolume", out originalMusic);

        cached = true;
    }


    public IEnumerator DuckAudio(float decreaseSFX = 10f, float decreaseMusic = 25f)
    {
        CacheVolumes();
        yield return StartCoroutine(FadeMixer("SFXvolume", originalSFX - decreaseSFX));
        yield return StartCoroutine(FadeMixer("MusicVolume", originalMusic - decreaseMusic));
    }

    public IEnumerator RestoreAudio()
    {
        yield return StartCoroutine(FadeMixer("SFXvolume", originalSFX));
        yield return StartCoroutine(FadeMixer("MusicVolume", originalMusic));
        cached = false;
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
