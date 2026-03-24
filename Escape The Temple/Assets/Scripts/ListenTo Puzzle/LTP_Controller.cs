using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LTP_Controller : MonoBehaviour
{
    //List<AudioClip> playersSequence = new List<AudioClip>();
    public List<LTP_soundPairs> soundsList;
    int soundIndex = 0;
    Dictionary<AudioClip, AudioClip> soundPairs = new Dictionary<AudioClip, AudioClip>();
    [HideInInspector] public bool LTPpuzzleSolved = false;
    public int puzzleIndex = 7;
    public GameObject room4_Door;


    public interiorLanternsController[] interiorLantern;
    public interiorLanternsController[] interiorLanternRoom4;
    public exteriorLanternsController[] lanternsRoomRoom4;
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
            }
            else
            {
                soundIndex = 0;
            }

            if (soundIndex >= FindObjectOfType<LTP_bellPattern>().bellsSequence.Count)
            {
                StopCoroutine(FindObjectOfType<LTP_bellPattern>().PlaySequences());
                StartCoroutine(FindObjectOfType<ESP_AudioClueController>().RestoreAudio());
                GameManager.instance.callForSunMovement(puzzleIndex);
                GameManager.instance.turnOn_ExteriorLanterns(lanternsRoomRoom4, false);
                GameManager.instance.turnOn_InteriorLanterns(interiorLantern);
                room4_Door.GetComponent<fadeRoomDoor>().StartFade();
                FindObjectOfType<LTP_BellSwing>().StopRinging();
                FindObjectOfType<LTP_bellPattern>().audioSource.enabled = false;
                foreach (LTP_DrumsController drum in FindObjectsOfType<LTP_DrumsController>())
                {
                    drum.gameObject.layer = LayerMask.NameToLayer("Default");
                }
                LTPpuzzleSolved = true;
            }
        }
    }
}
