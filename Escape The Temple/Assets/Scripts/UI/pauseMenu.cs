using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class pauseMenu : MonoBehaviour
{
    public GameObject menuDePausa;
    public GameObject optionsMenu;
    public GameObject cursor;
    public GameObject journal;
    bool pausaActiva = false;
    bool showOptionsMenu = false;

    public AudioClip audioClip;
    AudioSource audiosource;
    Volume blurVolume;

    cinematicaController cinematicaController;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        audiosource = GetComponent<AudioSource>();
        blurVolume = GameObject.Find("Global Volume").GetComponent<Volume>();
        cinematicaController = FindObjectOfType<cinematicaController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && optionsMenu.activeInHierarchy)
        {
            openOptionsMenu();
        }

        if ((Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape)) && !optionsMenu.activeInHierarchy && !cinematicaController.cinematicaPlaying)
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
                blurVolume.weight = 1f;
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            } else
            {
                FindObjectOfType<ESP_AudioClueController>().audioClue.UnPause();
                
                cleanUI(true);
                Time.timeScale = 1.0f;
                blurVolume.weight = 0f;
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
    }

    public void openOptionsMenu()
    {
        showOptionsMenu = !showOptionsMenu;
        audiosource.PlayOneShot(audioClip);
        if(showOptionsMenu)
        {
            optionsMenu.SetActive(true);
        }
        else
        {
            optionsMenu.SetActive(false);
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
