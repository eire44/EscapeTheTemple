using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ESP_AudioClueController : MonoBehaviour
{
    public AudioSource audioClue;
    bool play = false;
    

    public void playAudioClue()
    {
        play = !play;
        if (play)
        {
            audioClue.Play();
        } else
        {
            audioClue.Stop();
        }
    }
}
