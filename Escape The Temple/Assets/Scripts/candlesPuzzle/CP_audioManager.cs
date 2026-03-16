using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CP_audioManager : MonoBehaviour
{
    public AudioClip[] audioClips;
    AudioSource audiosource;
    // Start is called before the first frame update
    void Start()
    {
        audiosource = GetComponent<AudioSource>();
    }

    public void turnCandle(bool on)
    {
        if(on)
        {
            audiosource.PlayOneShot(audioClips[0]);
        }
        else
        {
            audiosource.PlayOneShot(audioClips[1]);
        }
    }
}
