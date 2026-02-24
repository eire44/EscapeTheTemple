using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LTP_Controller : MonoBehaviour
{
    //List<AudioClip> playersSequence = new List<AudioClip>();
    public List<LTP_soundPairs> soundsList;
    int soundIndex = 0;
    Dictionary<AudioClip, AudioClip> soundPairs = new Dictionary<AudioClip, AudioClip>();
    bool LTPpuzzleSolved = false;
    public GameObject room4_Door;

    private void Start()
    {
        foreach (var soundPair in soundsList)
        {
            soundPairs[soundPair.drumSound] = soundPair.bellSound;
        }
    }

    public void savePlayedSound(AudioClip clip)
    {
        if (!LTPpuzzleSolved)
        {
            if (soundPairs[clip] == FindObjectOfType<LTP_bellPattern>().bellsSequence[soundIndex])
            {
                soundIndex++;
                Debug.Log("BIEN");
            }
            else
            {
                soundIndex = 0;
                Debug.Log("MAL");
            }
            Debug.Log("INDICE: " + soundIndex);

            if (soundIndex >= FindObjectOfType<LTP_bellPattern>().bellsSequence.Count)
            {
                Debug.Log("SECUENCIA PERFECTA");
                room4_Door.GetComponent<fadeRoomDoor>().StartFade();
                LTPpuzzleSolved = true;
            }
        }
    }
}
