using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class burningLiesController : MonoBehaviour
{
    public GameObject puzzle5_Piece;
    public phraseSet[] sets;
    [HideInInspector] public int currentSetIndex = 0;

    public phrasesController[] papers;
    public AudioSource audioSource;
    public float duracionFadeAudio = 1.5f;

    public ParticleSystem[] fuegos;
    public float duracionApagadoFuego = 1.5f;
    float[] ratesOriginales;

    bool puzzleAlreadySolved = false;

    void Start()
    {
        currentSetIndex = Random.Range(0, sets.Length);
        LoadSet(currentSetIndex);

        ratesOriginales = new float[fuegos.Length];

        for (int i = 0; i < fuegos.Length; i++)
        {
            if (fuegos[i] != null)
            {
                ratesOriginales[i] = fuegos[i].emission.rateOverTime.constant;
            }
        }
    }

    void LoadSet(int newIndex)
    {
        currentSetIndex = newIndex;
        Shuffle(papers);

        for (int i = 0; i < papers.Length; i++)
        {
            papers[i].GetComponentInChildren<TextMeshPro>().text = sets[newIndex].phrases[i];
        }
    }

    public void Shuffle(phrasesController[] array)
    {
        for (int i = array.Length - 1; i > 0; i--)
        {
            int randomIndex = Random.Range(0, i + 1);

            phrasesController temp = array[i];
            array[i] = array[randomIndex];
            array[randomIndex] = temp;
        }

        for (int i = 0; i < array.Length; i++)
        {
            array[i].index = i;
        }
    }

    public void checkBurntPaper(int burnedIndex, phrasesController currentPaper, bool burning)
    {
        if (puzzleAlreadySolved) return;

        int correctIndex = sets[currentSetIndex].wrongPhraseIndex;
        int papersBurnt = 0;
        phrasesController singleBurningPaper = currentPaper;
        foreach (phrasesController paper in papers)
        {
            if(paper.alreadyBurned)
            {
                papersBurnt++;
                singleBurningPaper = paper;
            }   
        }
        if(papersBurnt == 1)
        {
            if(singleBurningPaper.index == correctIndex)
            {
                puzzleAlreadySolved = true;
                PuzzleCompleted();
            }
            else
            {
                currentPaper.gameObject.GetComponent<BLP_resetPhrasesPosition>().resetPosition();
                LoadNextSet();
            }
        }
        else
        {
            if (burning)
            {
                if(burnedIndex != correctIndex)
                {
                    currentPaper.gameObject.GetComponent<BLP_resetPhrasesPosition>().resetPosition();
                    LoadNextSet();
                }
            }
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
        puzzle5_Piece.SetActive(true);
        puzzle5_Piece.GetComponent<fadeIn_PuzzlePieces>().StartFade();
        StartCoroutine(TransicionApagar());
        foreach (phrasesController paper in papers)
        {
            paper.gameObject.layer = LayerMask.NameToLayer("Default");
        }
        FindObjectOfType<SunMovement>().sunProgression();
    }


    private IEnumerator TransicionApagar()
    {
        float volumenInicial = audioSource.volume;

        float[] ratesIniciales = new float[fuegos.Length];

        for (int i = 0; i < fuegos.Length; i++)
        {
            if (fuegos[i] != null)
            {
                ratesIniciales[i] = ratesOriginales[i];
            }
        }

        float tiempo = 0f;

        while (tiempo < Mathf.Max(duracionApagadoFuego))
        {
            tiempo += Time.deltaTime;

            if (tiempo < duracionFadeAudio)
            {
                audioSource.volume = Mathf.Lerp(volumenInicial, 0f, tiempo / duracionFadeAudio);
            }

            for (int i = 0; i < fuegos.Length; i++)
            {
                if (fuegos[i] == null) continue;

                var emission = fuegos[i].emission;

                if (tiempo < duracionApagadoFuego)
                {
                    float nuevoRate = Mathf.Lerp(
                        ratesIniciales[i],
                        0f,
                        tiempo / duracionApagadoFuego
                    );

                    emission.rateOverTime = nuevoRate;
                }
            }

            yield return null;
        }

        audioSource.volume = 0f;

        for (int i = 0; i < fuegos.Length; i++)
        {
            if (fuegos[i] == null) continue;

            var emission = fuegos[i].emission;
            emission.rateOverTime = 0f;

            fuegos[i].Stop(true, ParticleSystemStopBehavior.StopEmitting);
        }
    }
}
