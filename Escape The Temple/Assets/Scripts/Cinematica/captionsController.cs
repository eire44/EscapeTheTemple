using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class captionsController : MonoBehaviour
{
    public AudioSource audioSource;
    public TextMeshProUGUI subtitleText;

    public List<ESP_CaptionsList> subtitles;
    int currentIndex = 0;

    bool wasPlaying = false;

    void Update()
    {
        if (audioSource.isPlaying && !wasPlaying)
        {
            subtitleText.gameObject.SetActive(true);
            currentIndex = 0;
        }

        if (!audioSource.isPlaying && wasPlaying)
        {
            subtitleText.text = "";
            subtitleText.gameObject.SetActive(false);
        }

        wasPlaying = audioSource.isPlaying;

        if (!audioSource.isPlaying || currentIndex >= subtitles.Count)
            return;

        float time = audioSource.time;
        
        if (time >= subtitles[currentIndex].startTime)
        {
            subtitleText.text = subtitles[currentIndex].text;

            if (time >= subtitles[currentIndex].endTime)
            {
                subtitleText.text = "";
                currentIndex++;
            }
        }
    }
}
