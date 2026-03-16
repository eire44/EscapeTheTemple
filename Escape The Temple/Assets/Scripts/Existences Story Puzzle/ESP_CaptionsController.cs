using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ESP_CaptionsController : MonoBehaviour
{
    public AudioSource audioSource;
    public TextMeshProUGUI subtitleText;

    public List<ESP_CaptionsList> subtitles;
    int currentIndex = 0;

    void Update()
    {
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
