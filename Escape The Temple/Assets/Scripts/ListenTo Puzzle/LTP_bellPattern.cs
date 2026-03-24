using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class LTP_bellPattern : MonoBehaviour
{
    public AudioClip[] clips;

    //public int minSounds = 6;
    //public int maxSounds = 8;
    int amountOfSounds = 6;
    [HideInInspector] public List<AudioClip> bellsSequence = new List<AudioClip>();
    public float delayBetweenSounds = 0.5f;
    public float silenceBetweenSequences = 3f;
    public bool playBellSounds = true;
    [HideInInspector] public AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        //amountOfSounds = Random.Range(minSounds, maxSounds +1);
        for (int i = 0; i < amountOfSounds; i++)
        {
            AudioClip randomClip = clips[Random.Range(0, clips.Length)];
            bellsSequence.Add(randomClip);
            Debug.Log(randomClip.name);
        }
    }

    public void startBellSoundsPattern()
    {
        StartCoroutine(FindObjectOfType<ESP_AudioClueController>().DuckAudio(7f, 19f));
        StartCoroutine(PlaySequences());
    }

    public IEnumerator PlaySequences()
    {
        //if (!FindObjectOfType<LTP_Controller>().LTPpuzzleSolved)
        //{
            while (playBellSounds)
            {
                foreach (AudioClip clip in bellsSequence)
                {
                    if (!playBellSounds || audioSource == null || !audioSource.enabled)
                        yield break;

                    audioSource.PlayOneShot(clip);
                    //audioSource.clip = clip;
                    //audioSource.Play();
                    yield return new WaitForSeconds(clip.length + delayBetweenSounds);
                }

                yield return new WaitForSeconds(silenceBetweenSequences);
            }
        //}
    }
}
