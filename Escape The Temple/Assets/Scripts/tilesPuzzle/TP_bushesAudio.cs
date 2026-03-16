using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TP_bushesAudio : MonoBehaviour
{
    public AudioClip[] audioClips;
    AudioSource audiosource;

    // Start is called before the first frame update
    void Start()
    {
        audiosource = GetComponent<AudioSource>();
    }

    public void bushSound()
    {
        audiosource.PlayOneShot(audioClips[Random.Range(0, audioClips.Length)]);
    }
}
