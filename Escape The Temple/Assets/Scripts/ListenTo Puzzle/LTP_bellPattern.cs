using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class LTP_bellPattern : MonoBehaviour
{
    public AudioClip[] clips;

    public int minSounds = 6;
    public int maxSounds = 8;
    int amountOfSounds = 6;
    [HideInInspector] public List<AudioClip> bellsSequence = new List<AudioClip>();
    public float delayBetweenSounds = 0.5f;
    public float silenceBetweenSequences = 3f;

    AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        amountOfSounds = Random.Range(minSounds, maxSounds +1);
        for (int i = 0; i < amountOfSounds; i++)
        {
            AudioClip randomClip = clips[Random.Range(0, clips.Length)];
            bellsSequence.Add(randomClip);
            Debug.Log(randomClip.name);
        }
    }

    public void startBellSoundsPattern()
    {
        StartCoroutine(PlaySequences());
    }

    IEnumerator PlaySequences()
    {
        while (true)
        {
            foreach (AudioClip clip in bellsSequence)
            {
                audioSource.clip = clip;
                audioSource.Play();
                yield return new WaitForSeconds(clip.length + delayBetweenSounds);
            }

            yield return new WaitForSeconds(silenceBetweenSequences);
        }
    }
}
