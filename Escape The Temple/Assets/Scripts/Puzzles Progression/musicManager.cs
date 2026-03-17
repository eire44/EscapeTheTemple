using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class musicManager : MonoBehaviour
{
    public AudioSource[] musicSources;

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
