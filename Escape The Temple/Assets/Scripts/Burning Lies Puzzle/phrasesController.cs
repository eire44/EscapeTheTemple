using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class phrasesController : MonoBehaviour
{
    public int index;
    [HideInInspector] public bool alreadyBurned = false;
    burningLiesController controller;

    public AudioClip[] audioClips;
    AudioSource audiosource;
    void Start()
    {
        controller = FindObjectOfType<burningLiesController>();
        audiosource = GetComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (alreadyBurned) return;

        if (other.CompareTag("Fire"))
        {
            alreadyBurned = true;
            controller.checkBurntPaper(index, this, true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (!alreadyBurned) return;

        if (other.CompareTag("Fire"))
        {
            alreadyBurned = false;
            controller.checkBurntPaper(index, this, false);
        }
    }
    public void grabPaper()
    {
        audiosource.PlayOneShot(audioClips[Random.Range(0, audioClips.Length)]);
    }
}
