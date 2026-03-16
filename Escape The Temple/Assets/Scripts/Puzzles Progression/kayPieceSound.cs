using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kayPieceSound : MonoBehaviour
{
    public AudioClip[] audioClips;
    AudioSource audiosource;
    // Start is called before the first frame update
    void Start()
    {
        audiosource = GetComponent<AudioSource>();
    }

    public void handleKeypiece(bool on)
    {
        if (on)
        {
            audiosource.PlayOneShot(audioClips[0]);
        }
        else
        {
            audiosource.PlayOneShot(audioClips[1]);
        }
    }
}
