using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_Controller : MonoBehaviour
{
    public AudioSource audioSource;
    public float duracionFadeAudio = 1.5f;

    public ParticleSystem[] fuegos;
    public float duracionApagadoFuego = 1.5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Salir();
        }
    }

    public void Salir()
    {
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #else
                Application.Quit();
        #endif
    }

    public void CargarEscenaConTransicion(string nombreEscena)
    {
        StartCoroutine(Transicion(nombreEscena));
    }

    private IEnumerator Transicion(string nombreEscena)
    {
        float volumenInicial = audioSource.volume;

        // Guardamos el rate inicial de cada fuego
        float[] ratesIniciales = new float[fuegos.Length];

        for (int i = 0; i < fuegos.Length; i++)
        {
            if (fuegos[i] != null)
            {
                ratesIniciales[i] = fuegos[i].emission.rateOverTime.constant;
            }
        }

        float tiempo = 0f;

        while (tiempo < Mathf.Max(duracionFadeAudio, duracionApagadoFuego))
        {
            tiempo += Time.deltaTime;

            // Fade de audio
            if (tiempo < duracionFadeAudio)
            {
                audioSource.volume = Mathf.Lerp(volumenInicial, 0f, tiempo / duracionFadeAudio);
            }

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

        // Forzamos valores finales
        audioSource.volume = 0f;

        for (int i = 0; i < fuegos.Length; i++)
        {
            if (fuegos[i] == null) continue;

            var emission = fuegos[i].emission;
            emission.rateOverTime = 0f;

            // Opcional: detener emisión definitivamente
            fuegos[i].Stop(true, ParticleSystemStopBehavior.StopEmitting);
        }

        SceneManager.LoadScene(nombreEscena);
    }

    public void CargarEscena()
    {
        SceneManager.LoadScene("SeonamsaTemple");
    }
}
