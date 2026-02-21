using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class burningLiesController : MonoBehaviour
{
    public GameObject puzzle5_Piece;
    public phraseSet[] sets;
    private int currentSetIndex = 0;

    public phrasesController[] papers;


    public ParticleSystem[] fuegos;
    public float duracionApagadoFuego = 1.5f;
    float[] ratesOriginales;

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
        puzzle5_Piece.SetActive(true);
        puzzle5_Piece.GetComponent<fadeIn_PuzzlePieces>().StartFade();
        StartCoroutine(TransicionApagar());
    }


    private IEnumerator TransicionApagar()
    {
        //float volumenInicial = audioSource.volume;

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

            // Fade de audio
            //if (tiempo < duracionFadeAudio)
            //{
            //    audioSource.volume = Mathf.Lerp(volumenInicial, 0f, tiempo / duracionFadeAudio);
            //}

            // Apagado de todos los fuegos
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

        //audioSource.volume = 0f;

        for (int i = 0; i < fuegos.Length; i++)
        {
            if (fuegos[i] == null) continue;

            var emission = fuegos[i].emission;
            emission.rateOverTime = 0f;

            fuegos[i].Stop(true, ParticleSystemStopBehavior.StopEmitting);
        }
    }
}
