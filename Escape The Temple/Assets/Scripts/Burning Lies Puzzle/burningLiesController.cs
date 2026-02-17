using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class burningLiesController : MonoBehaviour
{
    public phraseSet[] sets;
    private int currentSetIndex = 0;

    public phrasesController[] papers;

    void Start()
    {
        currentSetIndex = Random.Range(0, sets.Length);
        LoadSet(currentSetIndex);
    }

    void LoadSet(int newIndex)
    {
        currentSetIndex = newIndex;

        for (int i = 0; i < papers.Length; i++)
        {
            papers[i].GetComponentInChildren<TextMeshPro>().text = sets[newIndex].phrases[i];
        }
    }

    public void checkBurntPaper(int burnedIndex)
    {
        int correctIndex = sets[currentSetIndex].wrongPhraseIndex;

        if (burnedIndex == correctIndex)
        {
            PuzzleCompleted();
        }
        else
        {
            LoadNextSet();
        }
    }

    void LoadNextSet()
    {
        currentSetIndex++;

        if (currentSetIndex >= sets.Length)
            currentSetIndex = 0;

        LoadSet(currentSetIndex);
    }

    void PuzzleCompleted()
    {
        Debug.Log("Puzzle completado");
        // activar puerta, animación, etc.
    }
}
