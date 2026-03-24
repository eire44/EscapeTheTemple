using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class pauseMenu : MonoBehaviour
{
    public GameObject menuDePausa;
    public GameObject cursor;
    public GameObject journal;
    bool pausaActiva = false;

    public AudioClip audioClip;
    AudioSource audiosource;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        audiosource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape))
        {
            pausaActiva = !pausaActiva;
            menuDePausa.SetActive(pausaActiva);
            if (pausaActiva)
            {
                if (FindObjectOfType<ESP_AudioClueController>().audioClue.isPlaying)
                {
                    FindObjectOfType<ESP_AudioClueController>().audioClue.Pause();
                }
                cleanUI(false);
                journal.SetActive(false);
                Time.timeScale = 0.0f;
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            } else
            {
                FindObjectOfType<ESP_AudioClueController>().audioClue.UnPause();
                
                cleanUI(true);
                Time.timeScale = 1.0f;
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
    }

    public void reiniciarJuego()
    {
        audiosource.PlayOneShot(audioClip);
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("SeonamsaTemple");
    }

    public void backToMenu()
    {
        audiosource.PlayOneShot(audioClip);
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("Inicio");
    }

    public void Salir()
    {
        audiosource.PlayOneShot(audioClip);
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
                        Application.Quit();
        #endif
    }

    public void cleanUI(bool show)
    {
        if(show)
        {
            cursor.SetActive(true);
        } 
        else
        {
            cursor.SetActive(false);
            foreach (paperClue item in FindObjectsOfType<paperClue>())
            {
                if (item.showPaper)
                {
                    item.showPaper = false;
                    item.imgPaper.SetActive(false);
                }
            }
        }
        
    }
}
