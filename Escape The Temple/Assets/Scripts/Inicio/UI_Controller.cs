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
    float[] ratesOriginales;

    public GameObject canvas;
    Transform pInicio;
    Transform pCreditos;
    Transform pOpciones;
    // Start is called before the first frame update
    void Start()
    {
        pInicio = canvas.transform.Find("Pantalla Inicio");
        pCreditos = canvas.transform.Find("Pantalla Creditos");
        pOpciones = canvas.transform.Find("Pantalla Opciones");

        ratesOriginales = new float[fuegos.Length];

        for (int i = 0; i < fuegos.Length; i++)
        {
            if (fuegos[i] != null)
            {
                ratesOriginales[i] = fuegos[i].emission.rateOverTime.constant;
            }
        }
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
        StartCoroutine(TransicionApagar("", 5));
        
    }

    public void irACreditos()
    {
        StartCoroutine(TransicionApagar("", 2));
    }

    public void irAOpciones()
    {
        StartCoroutine(TransicionApagar("", 1));
    }

    public void CargarEscenaConTransicion(string nombreEscena)
    {
        StartCoroutine(TransicionApagar(nombreEscena, 0));
    }

    private IEnumerator TransicionApagar(string nombreEscena, int btnIndex)
    {
        float volumenInicial = audioSource.volume;

        // Guardamos el rate inicial de cada fuego
        float[] ratesIniciales = new float[fuegos.Length];

        for (int i = 0; i < fuegos.Length; i++)
        {
            if (fuegos[i] != null)
            {
                ratesIniciales[i] = ratesOriginales[i];
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

        if(btnIndex == 0)
        {
            SceneManager.LoadScene(nombreEscena);
        } else if(btnIndex == 1)
        {
            pInicio.gameObject.SetActive(false);
            pOpciones.gameObject.SetActive(true);

            StartCoroutine(TransicionEncender());
        } else if(btnIndex == 2)
        {
            pInicio.gameObject.SetActive(false);
            pCreditos.gameObject.SetActive(true);

            StartCoroutine(TransicionEncender());
        }
        else if (btnIndex == 3)
        {
            pInicio.gameObject.SetActive(true);
            pCreditos.gameObject.SetActive(false);
            StartCoroutine(TransicionEncender());
        }
        else if (btnIndex == 4)
        {
            pInicio.gameObject.SetActive(true);
            pOpciones.gameObject.SetActive(false);
            StartCoroutine(TransicionEncender());
        }
        else
        {
            #if UNITY_EDITOR
                        UnityEditor.EditorApplication.isPlaying = false;
            #else
                            Application.Quit();
            #endif
        }
    }

    private IEnumerator TransicionEncender()
    {
        float volumenFinal = 1; // o audioSource.volume si querés guardar uno
        audioSource.volume = 0f;

        // Guardamos el rate objetivo de cada fuego
        float[] ratesObjetivo = new float[fuegos.Length];

        for (int i = 0; i < fuegos.Length; i++)
        {
            if (fuegos[i] != null)
            {
                ratesObjetivo[i] = ratesOriginales[i];

                // arrancan apagados
                var emission = fuegos[i].emission;
                emission.rateOverTime = 0f;

                fuegos[i].Play();
            }
        }

        float tiempo = 0f;

        while (tiempo < Mathf.Max(duracionFadeAudio, duracionApagadoFuego))
        {
            tiempo += Time.deltaTime;

            // Fade IN de audio
            if (tiempo < duracionFadeAudio)
            {
                audioSource.volume = Mathf.Lerp(0f, volumenFinal, tiempo / duracionFadeAudio);
            }

            // Encendido de fuegos
            for (int i = 0; i < fuegos.Length; i++)
            {
                if (fuegos[i] == null) continue;

                var emission = fuegos[i].emission;

                if (tiempo < duracionApagadoFuego)
                {
                    float nuevoRate = Mathf.Lerp(
                        0f,
                        ratesObjetivo[i],
                        tiempo / duracionApagadoFuego
                    );

                    emission.rateOverTime = nuevoRate;
                }
            }

            yield return null;
        }

        // Forzamos valores finales
        audioSource.volume = volumenFinal;

        for (int i = 0; i < fuegos.Length; i++)
        {
            if (fuegos[i] == null) continue;

            var emission = fuegos[i].emission;
            emission.rateOverTime = ratesObjetivo[i];
        }
    }

    public void ocultarCreditos()
    {
        StartCoroutine(TransicionApagar("", 3));
    }

    public void ocultarOpciones()
    {
        StartCoroutine(TransicionApagar("", 4));
    }
}
