using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public musicManager musicController;
    public SunMovement sunMovement;
    public journalWriting_Controller journalController;

    [Header("Nature Elements")]
    public ParticleSystem stars;
    public ParticleSystem fireflies1;
    public ParticleSystem fireflies2;
    public AudioSource[] dayNatureSounds;
    public AudioSource[] nightNatureSounds;
    public GameObject[] butterflies;
    bool soundsChanged = false;

    [Header("Puzzles Progression")]
    public int totalPuzzles = 10;
    [HideInInspector] public int progressionIndex = 0;

    void Awake()
    {
        instance = this;
    }

    public void callForSunMovement(int puzzleIndex)
    {
        if (progressionIndex >= totalPuzzles)
            return;

        Debug.Log("SUN MOVED");
        journalController = FindObjectOfType<journalWriting_Controller>();
        sunMovement = FindObjectOfType<SunMovement>();

        journalController.sumLessonsText(puzzleIndex, (progressionIndex + 1).ToString());

        if (progressionIndex == 7)
        {
            fireflies1.gameObject.SetActive(true);
            foreach (GameObject item in butterflies)
            {
                item.SetActive(false);
            }
        }

        if (progressionIndex == 8)
            fireflies2.gameObject.SetActive(true);

        if(progressionIndex > sunMovement.sunsetIndex)
        {
            stars.gameObject.SetActive(true);
            if (!soundsChanged)
            {
                soundsChanged = true;
                foreach (AudioSource item in dayNatureSounds)
                {
                    StartCoroutine(sunMovement.FadeOut(item, 2f));
                }
                foreach (AudioSource item in nightNatureSounds)
                {
                    StartCoroutine(sunMovement.FadeIn(item, 2f));
                }
            }
        }

        sunMovement.sunProgression(progressionIndex, totalPuzzles);

        progressionIndex++;
    }

    public void turnOn_ExteriorLanterns(exteriorLanternsController[] extLanterns, bool ending)
    {
        bool flag = true;
        foreach (exteriorLanternsController item in extLanterns)
        {
            item.TurnOn();
            if(flag)
            {
                flag = false;
                musicController.changeMusic(ending);
            }
            
        }
    }
    public void turnOn_InteriorLanterns(interiorLanternsController[] intLanterns)
    {
        foreach (interiorLanternsController item in intLanterns)
        {
            item.TurnOn();
        }
    }
    //public void flicker_InteriorLanterns(interiorLanternsController[] intLanterns)
    //{
    //    foreach (interiorLanternsController item in intLanterns)
    //    {
    //        Debug.Log(item.name);
    //        item.StartFlicker();
    //    }
    //}

    public void checkIfRoom2Completed()
    {
        if(FindObjectOfType<burningLiesController>().puzzleAlreadySolved && FindObjectOfType<LC_PuzzleController>().puzzleAlreadySolved && FindObjectOfType<stagesController>().puzzleAlreadySolved)
        {
            turnOn_ExteriorLanterns(FindObjectOfType<burningLiesController>().lanternsRoomABL, false);
            //flicker_InteriorLanterns(FindObjectOfType<burningLiesController>().interiorLanternABL);
        }
    }
    public IEnumerator turnOn_TilesLanterns(exteriorLanternsController[] extLanterns, bool ending)
    {
        bool flag = true;

        for (int i = 0; i < extLanterns.Length; i++)
        {
            extLanterns[i].TurnOn();

            if (flag)
            {
                flag = false;
                musicController.changeMusic(ending);
            }

            if (i % 2 != 0)
            {
                yield return new WaitForSeconds(0.5f);
            }
        }
    }

}
