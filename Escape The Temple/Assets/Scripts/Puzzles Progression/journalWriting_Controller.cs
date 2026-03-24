using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class journalWriting_Controller : MonoBehaviour
{
    public TextMeshProUGUI textMesh;
    public string[] lessons;
    public float speed = 50f;
    public GameObject journalCanvas;
    public GameObject pauseMenu;
    bool journalActivo = false;
    string lessonsText = "";
    public AudioSource writingLesson_Audio;
    public AudioSource showingLesson_Audio;
    int previousCharCount = 0;
    Coroutine fadeCoroutine;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            if(!pauseMenu.activeInHierarchy)
            {
                openJournal();
            }
        }
    }

    public void sumLessonsText(int lessonIndex) //que el indice corresponda al indice del puzzle, realizar en GameManager
    {
        writingLesson_Audio.Play();
        lessonsText += lessons[lessonIndex] + "\n";
    }

    void openJournal()
    {
        journalActivo = !journalActivo;
        journalCanvas.SetActive(journalActivo);
        if (journalActivo)
        {
            if (FindObjectOfType<ESP_AudioClueController>().audioClue.isPlaying)
            {
                FindObjectOfType<ESP_AudioClueController>().audioClue.Pause();
            }
            FindObjectOfType<pauseMenu>().cleanUI(false);
            Time.timeScale = 0.0f;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;

            writeLesson();
        }
        else
        {
            StartFadeOut();
            FindObjectOfType<ESP_AudioClueController>().audioClue.UnPause();

            FindObjectOfType<pauseMenu>().cleanUI(true);
            Time.timeScale = 1.0f;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    void writeLesson()
    {
        StartCoroutine(ShowText(lessonsText));
    }

    IEnumerator ShowText(string text)
    {
        bool playAudio = false;
        textMesh.text = text;

        int totalChars = text.Length;

        float t = previousCharCount;

        textMesh.maxVisibleCharacters = previousCharCount;

        if(previousCharCount < totalChars)
        {
            playAudio = true;
            StartFadeIn();
        }

        while (textMesh.maxVisibleCharacters < totalChars)
        {
            t += Time.unscaledDeltaTime * speed;
            textMesh.maxVisibleCharacters = (int)t;

            yield return null;
        }

        previousCharCount = totalChars;

        if(playAudio)
        {
            StartFadeOut();
        }
        playAudio = false;
    }

    void StartFadeIn()
    {
        if (fadeCoroutine != null)
            StopCoroutine(fadeCoroutine);

        fadeCoroutine = StartCoroutine(FindObjectOfType<SunMovement>().FadeIn(showingLesson_Audio, 0.3f));
    }

    void StartFadeOut()
    {
        if (fadeCoroutine != null)
            StopCoroutine(fadeCoroutine);

        fadeCoroutine = StartCoroutine(FindObjectOfType<SunMovement>().FadeOut(showingLesson_Audio, 0.3f));
    }
}
