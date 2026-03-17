using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class musicManager : MonoBehaviour
{
    AudioSource musicAudiosource;
    public AudioClip[] musicClips;

    private void Start()
    {
        musicAudiosource = GetComponent<AudioSource>();
    }

    public void changeMusic(bool ending)
    {
        if(ending)
        {
            musicAudiosource.clip = musicClips[2];
        }
        else
        {
            if (musicAudiosource.clip == musicClips[0])
            {
                musicAudiosource.clip = musicClips[1];
            }
            else if (musicAudiosource.clip == musicClips[1])
            {
                musicAudiosource.clip = musicClips[0];
            }
        }
    }
}
